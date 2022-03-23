namespace ET
{
    public enum AccountType
    {
        Normal = 0,
        blockList = 1
    }

    public class Account :Entity,IAwake
    {
        public string AccountName;  // 账号
        public string Password;      //密码
        public long CreateTime;      //创建时间
        public int AccountType;      // 账号类型

    }
}