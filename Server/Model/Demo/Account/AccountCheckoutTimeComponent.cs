﻿namespace ET
{
    public class AccountCheckoutTimeComponent :Entity,IAwake<long>,IDestroy
    {
        public long Timer = 0;
        
        public long AccountId = 0;
    }
}