using System.IO.IsolatedStorage;

namespace ET
{
    public static class DisconnectHelper
    {
        public static async ETTask Disconnect(this Session self)
        {
            if (self == null|| self.IsDisposed)
            {
                return;
            }

            long instanceId = self.InstanceId;
            await TimerComponent.Instance.WaitAsync(1000);
            if (self.InstanceId != instanceId)
            {
                return;
            }
            self.Dispose();
        }
        public static async ETTask KickPlayer( Player  player, bool isException = false)
        {
            if (player == null|| player.IsDisposed)
            {
                return;
            }

            long instanceId = player.InstanceId;
            using (await  CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginGate,player.Account.GetHashCode()))
            {
                if (player.IsDisposed || instanceId != player.InstanceId)
                {
                    return;
                }

                if (!isException)
                {
                    switch (player.PlayerState)
                    {
                        case  PlayerState.Disconnect:
                            break;
                        case  PlayerState.Game :
                            
                            
                            break;
                        case PlayerState.Gate:
                            // TODO 通知游戏逻辑服下线Unit角色逻辑，并将数据存入数据库

                            var m2GRequestExitGame =    (M2G_RequestExitGame) await MessageHelper.CallLocationActor(player.UnitId, new G2M_RequestExitGame());
                            
                            //移除账号登陆信息
                            var loginCenterInstaneId = StartSceneConfigCategory.Instance.LoginCenterConfig.InstanceId;
                            var l2G_RemoveLoginRecord = await MessageHelper.CallActor(loginCenterInstaneId,
                                new G2L_RemoveLoginRecord() { AccountId = player.Account, ServerId = player.DomainZone() });
                            break;
                    }
                }
                player.PlayerState = PlayerState.Disconnect;
                player.DomainScene().GetComponent<PlayerComponent>()?.Remove(player.Account);
                player?.Dispose();
                await TimerComponent.Instance.WaitAsync(300);
            }
          
        }
    }
}