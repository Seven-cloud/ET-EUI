using System;

namespace ET
{
    [Timer(TimerType.PlayerOffOutTime)]
    public class  PlayerOfflineOutTiime :ATimer<PlayerOffLineOutTimeComponent>
    {
        public override void Run(PlayerOffLineOutTimeComponent Self)
        {
            try
            {
                Self.KickPlayer();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }

    public class PlayerOffLineOutTimeComponentDetroySystem :DestroySystem<PlayerOffLineOutTimeComponent>
    {
        public override void Destroy(PlayerOffLineOutTimeComponent self)
        {
            TimerComponent.Instance.Remove(ref self.Timer);
        }
    }

    public class  PlayerOffLineOutTimeComponentAwakeSystem :AwakeSystem<PlayerOffLineOutTimeComponent>
    {
        public override void Awake(PlayerOffLineOutTimeComponent self)
        {
            self.Timer = TimerComponent.Instance.NewOnceTimer(TimeHelper.ServerNow() + 10000, TimerType.PlayerOffOutTime, self);
        }
    }

    public  static  class PlayerOffLineOutTimeComponentSystem 
    {
        public static void KickPlayer( this PlayerOffLineOutTimeComponent self)
        {
            DisconnectHelper.KickPlayer(self.GetParent<Player>()).Coroutine();
        }
    }
}