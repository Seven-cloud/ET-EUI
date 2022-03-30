﻿using System.Collections.Generic;

namespace ET
{
    public class RoleInfoComponent :Entity,IAwake,IDestroy
    {
        public List<RoleInfo> RoleInfos = new List<RoleInfo>();
        public int CurrentRoleId = 0;
    }
}