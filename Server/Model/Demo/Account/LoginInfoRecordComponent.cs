﻿using System.Collections.Generic;

namespace ET
{
    public class LoginInfoRecordComponent :Entity ,IAwake,IDestroy
    {
        public Dictionary<long, int> LoginInfoRecordDictionary = new Dictionary<long, int>();
    }
}