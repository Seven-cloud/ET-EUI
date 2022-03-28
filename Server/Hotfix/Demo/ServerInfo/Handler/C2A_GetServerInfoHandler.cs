using System;

namespace ET.Handler
{
    public class C2A_GetServerInfoHandler :AMRpcHandler<C2A_GetServerInfo,A2C_GetServerInfo>
    {
        protected override async  ETTask Run(Session session, C2A_GetServerInfo request, A2C_GetServerInfo response, Action reply)
        {
            if (session.DomainScene().SceneType != SceneType.Account)
            {
                Log.Debug($"请求的Scene错误，当前Scene为:{ session.DomainScene().SceneType}");
                session.Dispose();
                return;
            }

            string token = session.DomainScene().GetComponent<TokenComponent>().Get(request.Account);

            if (token == null || token != request.Token)
            {
                response.Error = ErrorCode.ERR_TokenError;
                reply();
                return;
            }

            foreach (var info in session.DomainScene().GetComponent<ServerInfoManagerComponent>().ServerInfos)
            {
                response.ServerInfoList.Add(info.ToMessage());
            }

            reply();
            await ETTask.CompletedTask;
        }
    }
}