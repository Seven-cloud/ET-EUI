namespace ET
{

    public class ServerInfoManagerComponentAwakeSystem :AwakeSystem<ServerInfoManagerComponent>
    {
        public override void Awake(ServerInfoManagerComponent self)
        {
            self.Awake().Coroutine();
        }
    }
    public class ServerInfoManagerComponentDestroySystem :DestroySystem<ServerInfoManagerComponent>
    {
        public override void Destroy(ServerInfoManagerComponent self)
        {
            foreach (var info in self.ServerInfos)
            {
                info?.Dispose();
            }
            self.ServerInfos.Clear();
        }
    }

    public class ServerInfoManagerComponentLoadSystem: LoadSystem<ServerInfoManagerComponent>
    {
        public override void Load(ServerInfoManagerComponent self)
        {
            self.Awake().Coroutine();
        }
    }

    public  static class ServerInfoManagerComponentSystem 
    {
        public static async   ETTask Awake(this  ServerInfoManagerComponent self)
        {

            var serverInfoList = await DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Query<ServerInfo>(d => true);
            if (serverInfoList == null || serverInfoList.Count <=0)
            {
                Log.Error("serverInfo count zero!");
                self.ServerInfos.Clear();
                var ServerInfoConfig = ServerInfoConfigCategory.Instance.GetAll();
                foreach (var infoConfig in ServerInfoConfig.Values)
                {
                    ServerInfo serverInfo = self.AddChildWithId<ServerInfo>(infoConfig.Id);
                    serverInfo.Status = (int)ServerStatus.Normal;
                    serverInfo.ServerName = infoConfig.ServerName;
                    self.ServerInfos.Add(serverInfo);
                   await DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Save(serverInfo);
                }




                return;
            }
            self.ServerInfos.Clear();
            foreach (var info in serverInfoList )
            {
                self.AddChild(info);
                self.ServerInfos.Add(info);
            }
            
            await ETTask.CompletedTask;
        }
    }
}