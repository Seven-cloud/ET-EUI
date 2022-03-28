namespace ET
{
    public  static  class ServerInfoSystem
    {
        public static void FromMessage(this  ServerInfo self, ServerInfoProto serverInfoProto)
        {
            self.Status = serverInfoProto.Status;
            self.Id = serverInfoProto.Id;
            self.ServerName = serverInfoProto.SeverName;
        }

        public static ServerInfoProto ToMessage(this  ServerInfo self)
        {
            return new ServerInfoProto() { Id = (int) self.Id, Status = self.Status, SeverName = self.ServerName, };
        }
    }
}