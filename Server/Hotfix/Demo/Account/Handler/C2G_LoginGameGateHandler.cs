using System;

namespace ET
{
    public class C2G_LoginGameGateHandler :AMRpcHandler<C2G_LoginGameGate,G2C_LoginGameGate>
    {
        protected override async ETTask Run(Session session, C2G_LoginGameGate request, G2C_LoginGameGate response, Action reply)
        {
            if (session.DomainScene().SceneType != SceneType.Gate)
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
            string token = session.DomainScene().GetComponent<GateSessionKeyComponent>().Get(request.Account);
            if (token == null || token != request.Key)
            {
                response.Error = ErrorCode.ERR_ConenctGateKeyError;
                response.Message = "Gate Key 验证失败！";
                reply();
                session?.Disconnect().Coroutine();
                return;
            }
            session.DomainScene().GetComponent<GateSessionKeyComponent>().Remove(request.Account);

            long instanceId = session.InstanceId;

            using (session.AddComponent<SessionLockingComponent>())
            {
                using (await  CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginGate,request.Account.GetHashCode()))
                {
                    if (instanceId != session.InstanceId)
                    {
                        return;
                    }
                    // 通知登录中心服  记录本次登录服务器的zone
                    StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.LocationConfig;
                    L2G_AddLoginRecord l2GAddLoginRecord = (L2G_AddLoginRecord) await MessageHelper.CallActor(startSceneConfig.InstanceId,
                        new G2L_AddLoginRecord() { AccountId = request.Account, ServerId = session.DomainScene().Zone });
                    if (l2GAddLoginRecord.Error != ErrorCode.ERR_Success)
                    {
                        response.Error = l2GAddLoginRecord.Error;
                        reply();
                        session?.Disconnect().Coroutine();
                        return;
                    }

                    Player player = session.DomainScene().GetComponent<PlayerComponent>().Get(request.Account);

                    if (player == null)
                    {
                        //添加一个新的GateUnit
                        player = session.DomainScene().GetComponent<PlayerComponent>()
                                .AddChildWithId<Player,long, long>(request.RoleId, request.Account, request.RoleId);
                        player.PlayerState = PlayerState.Gate;
                        session.DomainScene().GetComponent<PlayerComponent>().Add(player);
                        session.AddComponent<MailBoxComponent, MailboxType>(MailboxType.GateSession);
                    }
                    else
                    {
                        player.RemoveComponent<PlayerOffLineOutTimeComponent>();
                    }

                    session.AddComponent<SessionPlayerComponent>().PlayerId = player.Id;
                    session.GetComponent<SessionPlayerComponent>().PlayerInstanceId = player.InstanceId;
                    session.GetComponent<SessionPlayerComponent>().AccountId = player.Account;
                    player.SessionInstanceId = session.InstanceId;
                }
            }

            await ETTask.CompletedTask;
        }
    }
}