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
            if (!self.accountSessionKeyDcit.TryGetValue(key,out string AccountName))
            {
                return null;
            }

            return AccountName;
        }

        public static void Add(this AccountSessionKeyComponent self, long key, string AccountName)
        {
            if (self.accountSessionKeyDcit.ContainsKey(key))
            {
                self.accountSessionKeyDcit[key] = AccountName;
                return;
            }
            self.accountSessionKeyDcit.Add(key,AccountName);
        }


        public static void Remove(this AccountSessionKeyComponent self, long key)
        {
            if (self.accountSessionKeyDcit.ContainsKey(key))
            {
                self.accountSessionKeyDcit.Remove(key);
            }
        }

    }
}