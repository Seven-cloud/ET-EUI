using System;
using System.Text.RegularExpressions;

namespace ET
{
    public class C2A_LoginAccountHandler :AMRpcHandler<C2A_LoginAccount,A2C_LoginAccount>
    {
        protected override  async   ETTask Run(Session session, C2A_LoginAccount request, A2C_LoginAccount response, Action reply)
        {
            if (session.DomainScene().SceneType != SceneType.Account)
            {
                Log.Debug($"请求的Scene错误，当前Scene为：{session.DomainScene().SceneType}");
                session.Dispose();
                return;
            }
            session.RemoveComponent<SessionAcceptTimeoutComponent>();

            if (session.GetComponent<SessionLockingComponent>()!= null)
            {
                response.Error = ErrorCode.ERR_RequestRepeatError;
                reply();
                session.Disconnect().Coroutine();
                return;
                
            }
            if (string.IsNullOrEmpty(request.Account) || string.IsNullOrEmpty(request.Password))
            {
                response.Error = ErrorCode.ERR_LoginInfoError;
                reply();
                session.Disconnect().Coroutine();
                return;
            }

            if (!Regex.IsMatch(request.Account.Trim(),@"^(?=.*[0-9].*)(?=.*[A-Z].*)(?=.*[a-z].*).{6,20}$"))
            {
                response.Error = ErrorCode.ERR_AccountNameFormatError;
                reply();
                session.Disconnect().Coroutine();
                return;
            }
            if (!Regex.IsMatch(request.Password.Trim(),@"^[A-Za-z0-9]+$"))
            {
                response.Error = ErrorCode.ERR_PasswordFormatError;
                reply();
                session.Disconnect().Coroutine();
                return;
            }
            using (session.AddComponent<SessionLockingComponent>())
            {

                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginAccount,request.Account.GetHashCode()) )
                {
                    var accountInfoList =  await DBManagerComponent.Instance.GetZoneDB(session.DomainZone())
                        .Query<Account>(d => d.AccountName.Equals(request.Account.Trim()));
                    
                    Account account = null;
                    if (accountInfoList !=null && accountInfoList.Count > 0)
                    {
                        account = accountInfoList[0];
                        session.AddChild(account);
                        if (account.AccountType  == (int)AccountType.blockList)
                        {
                            response.Error = ErrorCode.ERR_AccountInBlackListError;
                            reply();
                            session.Disconnect().Coroutine();
                            account.Dispose();
                            return;
                        }

                        if (!account.Password.Equals(request.Password))
                        {
                            response.Error = ErrorCode.ERR_LoginPasswordError;
                            reply();
                            session.Disconnect().Coroutine();
                            account.Dispose();
                            return;
                        }
                    }
                    else
                    {
                        account = session.AddChild<Account>();
                        account.AccountName = request.Account;
                        account.Password = request.Password;
                        account.CreateTime = TimeHelper.ServerNow();
                        account.AccountType = (int) AccountType.Normal;

                        await DBManagerComponent.Instance.GetZoneDB(session.DomainZone()).Save<Account>(account);

                    }

                    StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetBySceneName(session.DomainZone(), "LoginCenter");
                    long loginCenterInstanceId = startSceneConfig.InstanceId;
                    var LoginAccountResponse =
                           (L2A_LoginAccoutResponse) await ActorMessageSenderComponent.Instance.Call(loginCenterInstanceId,
                                new A2L_LoginAccoutRequest() { AccountId = account.Id });
                    if (LoginAccountResponse.Error != ErrorCode.ERR_Success)
                    {
                        response.Error = LoginAccountResponse.Error;
                    
                        reply();
                        session?.Disconnect();
                        account?.Dispose();
                    }
                    long sessionInstanceId = session.DomainScene().GetComponent<AccountSessionComponent>().Get(account.Id);
                    Session otherSession = Game.EventSystem.Get(sessionInstanceId) as Session;
                    otherSession?.Send(new A2C_Disconnect(){ Error =  0});
                    otherSession?.Disconnect();
                    session.DomainScene().GetComponent<AccountSessionComponent>().Add(account.Id,account.InstanceId);
                    session.AddComponent<AccountCheckoutTimeComponent, long>(account.Id);
                    string Token = TimeHelper.ServerNow().ToString() + RandomHelper.RandomNumber(int.MinValue, int.MaxValue).ToString();
                    
                    session.DomainScene().GetComponent<TokenComponent>().Remove(account.Id);
                    session.DomainScene().GetComponent<TokenComponent>().Add(account.Id,Token);


                    response.AccountId = account.Id;
                    
                    response.Token = Token;
                    account.Dispose();
                    reply();
        
                }
            }
        }
    }
}