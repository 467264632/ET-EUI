namespace ET.Server
{
    // session add本组件，session只持续60秒，必须通过验证，否则断开
    [ComponentOf(typeof(Session))]
    public class AccountCheckOutTimeComponent : Entity,IAwake<long>,IDestroy
    {
        public long Timer = 0;

        public long AccountId = 0;
    }
}