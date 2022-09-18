namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    public class AccountInfoComponent : Entity,IAwake,IDestroy
    {
        public string Token;
        public long AccountId;
        public string GateKey;
        public string GateAddress;
    }
}

