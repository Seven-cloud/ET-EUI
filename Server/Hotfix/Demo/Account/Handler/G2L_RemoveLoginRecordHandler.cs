using System;

namespace ET
{
    public class G2L_RemoveLoginRecordHandler :AMActorRpcHandler<Scene,G2L_RemoveLoginRecord,L2G_RemoveLoginRecord>
    {
        protected override  async ETTask Run(Scene scene, G2L_RemoveLoginRecord request, L2G_RemoveLoginRecord response, Action reply)
        {

            long account = request.AccountId;
            using ( await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginCenterLock,account.GetHashCode()))
            {
                int zone = scene.GetComponent<LoginInfoRecordComponent>().Get(account);
                if (request.ServerId == zone)
                {
                    scene.GetComponent<LoginInfoRecordComponent>().Remove(account);
                }
            }

            reply();
            await ETTask.CompletedTask;
        }
    }
}