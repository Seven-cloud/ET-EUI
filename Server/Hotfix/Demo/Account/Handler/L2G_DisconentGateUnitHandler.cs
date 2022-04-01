using System;

namespace ET
{
    public class L2G_DisconentGateUnitHandler :AMActorRpcHandler<Scene,L2G_DisconentGateUnit,G2L_DisconentGateUnit>
    {
        protected override async ETTask Run(Scene scene, L2G_DisconentGateUnit request, G2L_DisconentGateUnit response, Action reply)
        {
            long accountId = request.AccountId;
            using (await  CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginGate,accountId.GetHashCode()))
            {
                PlayerComponent playerComponent = scene.AddComponent<PlayerComponent>();
               Player player = playerComponent.Get(accountId);
               if (player ==null)
               {
                   reply();
                   return;
               }  
               scene.GetComponent<GateSessionKeyComponent>().Remove(accountId);
               
               Session gateSession = Game.EventSystem.Get(player.SessionInstanceId) as Session;
               if (gateSession != null && !gateSession.IsDisposed)
               {
                   gateSession.Send(new  A2C_Disconnect(){Error =  ErrorCode.ERR_OtherAccountLogin});
                   gateSession?.Disconnect().Coroutine();
               }

               player.SessionInstanceId = 0;
               player.AddComponent<PlayerOffLineOutTimeComponent>();
            }
        }
    }
}