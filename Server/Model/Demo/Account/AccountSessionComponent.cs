using System.Collections.Generic;

namespace ET
{
    public class AccountSessionComponent :Entity,IAwake,IDestroy
    {
        public Dictionary<long, long> AccontSessionDictionary = new Dictionary<long, long>();
    }
}