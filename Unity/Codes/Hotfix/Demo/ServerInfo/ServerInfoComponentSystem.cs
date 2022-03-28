namespace ET
{
    public  class  ServerInfoComponentDestroySystem :DestroySystem<ServerInfoComponent>
    {
        public override void Destroy(ServerInfoComponent self)
        {
            foreach (var info in self.ServerInfoList)
            {
                info?.Dispose();
            }
            self.ServerInfoList.Clear();
            self.CurrentServerId = 0;
        }
    }

    public static  class SeverInfoComponentSystem 
    {
        public static void Add(this ServerInfoComponent self,ServerInfo serverInfo)
        {
            self.ServerInfoList.Add(serverInfo);
        }
    }
}