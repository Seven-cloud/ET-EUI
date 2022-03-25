using System;
using MongoDB.Driver.Core.Events;

namespace ET
{
    [Timer(TimerType.AccountSessionCheckoutTime)]
    public class AccountSessiomCheckoutTimer :ATimer<AccountCheckoutTimeComponent>
    {
        public override void Run(AccountCheckoutTimeComponent self)
        {
            try
            {
                self.DeleteSession();   
            }
            catch (Exception e)
            {
              Log.Error(e.ToString());
            }
        }
    }

    public class AccountCheckoutTimeComponentAwakeSystem :AwakeSystem<AccountCheckoutTimeComponent,long>
    {
        public override void Awake(AccountCheckoutTimeComponent self, long accountId)
        {
            self.AccountId = accountId;
            TimerComponent.Instance.Remove(ref self.Timer);
            self.Timer = TimerComponent.Instance.NewOnceTimer(TimeHelper.ServerNow() + 600000, TimerType.AccountSessionCheckoutTime, self);
        }
    }

    public class AccoutCheckoutTimeComponentDestroySystem: DestroySystem<AccountCheckoutTimeComponent>
    {
        public override void Destroy(AccountCheckoutTimeComponent self)
        {
            self.AccountId = 0;
            TimerComponent.Instance.Remove(ref self.Timer);
        }
    }

    public  static  class AccountCheckoutTimeComponentSystem 
    {
        public static void DeleteSession(this  AccountCheckoutTimeComponent self)
        {
            Session session = self.GetParent<Session>();

            long sessionInstanceId = session.DomainScene().GetComponent<AccountSessionComponent>().Get(self.AccountId);
            if (session.InstanceId == sessionInstanceId)
            {
                session.DomainScene().GetComponent<AccountSessionComponent>().Remove(self.AccountId);
            }
            session?.Send(new  A2C_Disconnect(){Error =  1});
            session?.Disconnect();
        }
    }
}