using UnityEngine;

namespace ET
{
    public  static class TokeComponentSystem
    {
        public static void Add(this TokenComponent self ,long key,string token)
        {
            self.TokenDictionary.Add(key,token);
        }
    
        public static void Remove(this TokenComponent self,long key)
        {
            if (self.TokenDictionary.ContainsKey(key))
            {
                self.TokenDictionary.Remove(key);
            }
        }

        public static string Get(this TokenComponent self,long key)
        {
            string value = null;
            self.TokenDictionary.TryGetValue(key, out value);
            return value;
        }

        public static async void TimeoutRemoveKey(this TokenComponent self, long key ,string token)
        {
            await TimerComponent.Instance.WaitAsync(60000);
            string onlineToken = self.Get(key);
            if (!string.IsNullOrEmpty(onlineToken) && onlineToken == token)
            {
                self.TokenDictionary.Remove(key);
            }
        }
    }
}