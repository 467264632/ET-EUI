namespace ET.Server
{
    [FriendOf(typeof(SessionPlayerComponent))]
    public static class SessionPlayerComponentSystem
    {
        public class SessionPlayerComponentDestroySystem: DestroySystem<SessionPlayerComponent>
        {
            protected override void Destroy(SessionPlayerComponent self)
            {
                // 发送断线消息
                if (!self.isLoginAgain && self.PlayerInstanceId != 0)
                {
                    Player player = EventSystem.Instance.Get(self.PlayerInstanceId) as Player;
                    DisconnectHelper.KickPlayer(player).Coroutine();
                }

                self.AccountId = 0;
                self.PlayerId = 0;
                self.PlayerInstanceId = 0;
                self.isLoginAgain = false;
            }
        }

        public static Player GetMyPlayer(this SessionPlayerComponent self)
        {
            return self.DomainScene().GetComponent<PlayerComponent>().Get(self.AccountId);
        }
    }
}