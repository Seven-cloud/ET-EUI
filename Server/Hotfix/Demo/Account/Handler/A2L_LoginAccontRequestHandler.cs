using System;

namespace ET
{
    
    [ActorMessageHandler]
    public class A2L_LoginAccontRequestHandler :AMActorRpcHandler<Scene,A2L_LoginAccoutRequest,L2A_LoginAccoutResponse>
    {
        protected override  async ETTask Run(Scene scene, A2L_LoginAccoutRequest request, L2A_LoginAccoutResponse response, Action reply)
        {

            long accountId = request.AccountId;
            using (await  CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginCenterLock,accountId.GetHashCode()))
            {
                if (!scene.GetComponent<LoginInfoRecordComponent>().IsExit(accountId))
                {
                    reply();
                    return;
                }

                int zone = scene.GetComponent<LoginInfoRecordComponent>().Get(accountId);
                Log.Debug("A====0",zone);
                StartSceneConfig startSceneConfig = RealmGateAddressHelper.GetGate(zone,accountId);
                Log.Debug("A====1");
               var   G2L_DisconentGateUnit =       (G2L_DisconentGateUnit)await  MessageHelper.CallActor(startSceneConfig.InstanceId, new L2G_DisconentGateUnit() { AccountId = accountId });
               response.Error = G2L_DisconentGateUnit.Error;
               Log.Debug("A====2" + response.Error);
               reply();
            }
        }
    }
}