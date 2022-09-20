namespace ET.Server
{
    
    public class AccountSessionKeyComponentDestroySystem:DestroySystem<AccountSessionKeyComponent>
    {
        protected override void Destroy(AccountSessionKeyComponent self)
        {
            self.accountSessionKeyDcit.Clear();
        }
    }

    [FriendOf(typeof(AccountSessionKeyComponent))]
    public static class AccountSessionKeyComponentSystem
    {
        public static string Get(this AccountSessionKeyComponent self, long key)
        {
            string AccountName = null;
            self.accountSessionKeyDcit.TryGetValue(key, out AccountName);
            return AccountName;
        }

        public static void Add(this AccountSessionKeyComponent self, long key, string AccountName)
        {
            self.accountSessionKeyDcit.Add(key,AccountName);
            self.TimeOutRemoveKey(key,AccountName).Coroutine();
        }


        public static void Remove(this AccountSessionKeyComponent self, long key)
        {
            if (self.accountSessionKeyDcit.ContainsKey(key))
            {
                self.accountSessionKeyDcit.Remove(key);
            }
        }

        private static async ETTask TimeOutRemoveKey(this AccountSessionKeyComponent self, long key, string AccountName)
        {
            await TimerComponent.Instance.WaitAsync(6000);

            string onlineAccountName = self.Get(key);

            if (!string.IsNullOrEmpty(onlineAccountName) && onlineAccountName == AccountName)
            {
                self.Remove(key);
            }

        }
    }
}