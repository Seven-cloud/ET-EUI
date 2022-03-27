using System;

namespace ET
{
    public class L2G_DisconentGateUnitHandler :AMActorRpcHandler<Scene,L2G_DisconentGateUnit,G2L_DisconentGateUnit>
    {
        protected override async ETTask Run(Scene scene, L2G_DisconentGateUnit request, G2L_DisconentGateUnit response, Action reply)
        {
            long accountId = request.AccountId;
            using (await  CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginGateLock,accountId.GetHashCode()))
            {
                PlayerComponent playerComponent = scene.AddComponent<PlayerComponent>();
               Player player = playerComponent.Get(accountId);
               if (player ==null)
               {
                   reply();
                   return;
               }  
               playerComponent.Remove(accountId);
               player.Dispose();
            }
        }
    }
}