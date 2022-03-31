using System;

namespace ET
{
    public class A2R_GetRealmKeyHandler :AMActorRpcHandler   <Scene,A2R_GetRealmKey,R2A_GetRealmKey>
    {
        protected override async ETTask Run(Scene scene, A2R_GetRealmKey request, R2A_GetRealmKey response, Action reply)
        {
            
            if (scene.DomainScene().SceneType != SceneType.Realm)
            {
                Log.Debug($"请求的Scene错误，当前Scene为:{ scene.DomainScene().SceneType}");
                response.Error = ErrorCode.ERR_RequestSceneTypeError;
                reply();
                scene.Dispose();
                return;
            }

            string key = TimeHelper.ServerNow().ToString() + RandomHelper.RandInt64().ToString();
            scene.GetComponent<TokenComponent>().Remove(request.AccountId);
            scene.GetComponent<TokenComponent>().Add(request.AccountId,key);
            response.RealmKey = key;
            reply();
                    
            await ETTask.CompletedTask;
        }
    }
}