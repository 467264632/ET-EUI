using System;
using System.Collections.Generic;
using System.Text;

namespace ET.Server
{
    [Callback(TimerCallbackId.PlayerOfflineOutTime)]
    public class PlayerOfflineOutTime: ATimer<PlayerOfflineOutTimeComponent>
    {
        protected override void Run(PlayerOfflineOutTimeComponent self)
        {
            try
            {
                self.KickPlayer();
            }
            catch (Exception e)
            {
                Log.Error($"playerOffline timer error: {self.Id}\n{e}");
            }
        }
    }
    
    
    [ObjectSystem]
    public class GateUnitDeleteComponentDestroySystem : DestroySystem<PlayerOfflineOutTimeComponent>
    {
        protected override void Destroy(PlayerOfflineOutTimeComponent self)
        {
            TimerComponent.Instance.Remove(ref self.Timer);
        }
    }

    [ObjectSystem]
    public class PlayerOfflineOutTimeComponentAwakeSystem : AwakeSystem<PlayerOfflineOutTimeComponent>
    {
        protected override void Awake(PlayerOfflineOutTimeComponent self)
        {
            self.Timer = TimerComponent.Instance.NewOnceTimer(TimeHelper.ServerNow() + 10000,TimerCallbackId.PlayerOfflineOutTime,self);
        }
    }

    public static class GateUnitDeleteComponentSystem
    {
        public static void KickPlayer(this PlayerOfflineOutTimeComponent self)
        {
            DisconnectHelper.KickPlayer(self.GetParent<Player>()).Coroutine();
        }
    }


}