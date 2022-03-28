namespace ET
{
    public static  class RoleInfoSystem
    {
        public static void FromMessage(this  RoleInfo self , RoleInfoProto roleInfoProto)
        {
            self.Id = roleInfoProto.Id;
            self.ServerId = roleInfoProto.ServerId;
            self.State = roleInfoProto.State;
            self.Account = roleInfoProto.AccountId;
            self.LastLoginTime = roleInfoProto.LastLoginTime;
            self.CreateTime = roleInfoProto.CreateTime;
        }
        public  static  RoleInfoProto ToMessage(this  RoleInfo self)
        {

            return new RoleInfoProto()
            {
                Id = self.Id,
                ServerId = self.ServerId,
                State = self.State,
                AccountId = self.Account,
                LastLoginTime = self.LastLoginTime,
                CreateTime = self.CreateTime,
            };
        }
    }
}