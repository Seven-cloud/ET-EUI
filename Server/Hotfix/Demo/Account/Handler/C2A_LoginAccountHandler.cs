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
            if (string.IsNullOrEmpty(request.Account) || string.IsNullOrEmpty(request.Password))
            {
                response.Error = ErrorCode.ERR_LoginInfoError;
                reply();
                session.Dispose();
                return;
            }

            if (!Regex.IsMatch(request.Account.Trim(),@"^(?=.*[0-9].*)(?=.*[A-Z].*)(?=.*[a-z].*).{6,20}$"))
            {
                response.Error = ErrorCode.ERR_LoginInfoError;
                reply();
                session.Dispose();
                return;
            }
            if (!Regex.IsMatch(request.Password.Trim(),@"^(?=.*[0-9].*)(?=.*[A-Z].*)(?=.*[a-z].*).{6,20}$"))
            {
                response.Error = ErrorCode.ERR_LoginInfoError;
                reply();
                session.Dispose();
                return;
            }

            var accountInfoList =  await DBManagerComponent.Instance.GetZoneDB(session.DomainZone())
                    .Query<Account>(d => d.AccountName.Equals(request.Account.Trim()));

            Account account = null;
            if (accountInfoList.Count > 0)
            {
                account = accountInfoList[0];
                session.AddChild(account);
                if (account.AccountType  == (int)AccountType.blockList)
                {
                    response.Error = ErrorCode.ERR_LoginInfoError;
                    reply();
                    session.Dispose();
                    return;
                }

                if (!account.Password.Equals(request.Password))
                {
                    response.Error = ErrorCode.ERR_LoginInfoError;
                    reply();
                    session.Dispose();
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

            string Token = TimeHelper.ServerNow().ToString() + RandomHelper.RandomNumber(int.MinValue, int.MaxValue).ToString();
            
            session.DomainScene().GetComponent<TokenComponent>().Remove(account.Id);
            session.DomainScene().GetComponent<TokenComponent>().Add(account.Id,Token);


            response.AccountId = account.Id;
            response.Token = Token;

            reply();


        }
    }
}