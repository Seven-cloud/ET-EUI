using System;

namespace ET
{
    public class C2G_LoginGameGateHandler :AMRpcHandler<C2G_LoginGameGate,G2C_LoginGameGate>
    {
        protected override async ETTask Run(Session session, C2G_LoginGameGate request, G2C_LoginGameGate response, Action reply)
        {
            
            
            await ETTask.CompletedTask;
        }
    }
}