namespace ET
{

    public class AccountSessionComponentSystemAwake:AwakeSystem<AccountSessionComponent>
    {
        public override void Awake(AccountSessionComponent self)
        {
            
        }
    }
    public  class  AccountSessionComponentSystemIDestroy :DestroySystem<AccountSessionComponent>
    {
        public override void Destroy(AccountSessionComponent self)
        {
            self.AccontSessionDictionary.Clear();
        }
    }

    public  static  class AccountSessionComponentSystem
    {
        public static long Get(this AccountSessionComponent self ,long accountId)
        {
            if (!self.AccontSessionDictionary.TryGetValue(accountId ,out long value))
            {
                return -1;
            }

            return value;
        }

        public static void Add( this AccountSessionComponent self,long AccountId ,long sessionInstanceId)
        {
            if (self.AccontSessionDictionary.ContainsKey(AccountId))
            {
                self.AccontSessionDictionary[AccountId] = sessionInstanceId;
                return;
            }
            self.AccontSessionDictionary.Add(AccountId,sessionInstanceId);
        }

        public static void Remove(this AccountSessionComponent self ,long AccountId)
        {
            if (self.AccontSessionDictionary.ContainsKey(AccountId))
            {
                self.AccontSessionDictionary.Remove(AccountId);
            }
        }

    }
}