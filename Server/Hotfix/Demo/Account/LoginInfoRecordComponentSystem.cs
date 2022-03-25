namespace ET
{

    public class LoginInfoRecordComponentDestroySystem: DestroySystem<LoginInfoRecordComponent>
    {
        public override void Destroy(LoginInfoRecordComponent self)
        {
            self.LoginInfoRecordDictionary.Clear();
            
        }
    }

    public static class LoginInfoRecordComponentSystem
    {
        public static void Add(this LoginInfoRecordComponent self, long key, int value)
        {
            if (self.LoginInfoRecordDictionary.ContainsKey(key))
            {
                self.LoginInfoRecordDictionary[key] = value;
                return;
            }

            self.LoginInfoRecordDictionary.Add(key, value);
        }

        public static void Remove(this  LoginInfoRecordComponent self,long key)
        {
            if (self.LoginInfoRecordDictionary.ContainsKey(key))
            {
                self.LoginInfoRecordDictionary.Remove(key);
            }
        }

        public static int Get(this  LoginInfoRecordComponent self ,long key)
        {
            if (!self.LoginInfoRecordDictionary.TryGetValue(key,out int value))
            {
                return -1;
            }
            return value;
        }

        public static bool IsExit(this  LoginInfoRecordComponent self,long key)
        {
            return self.LoginInfoRecordDictionary.ContainsKey(key);
        }
    }
}