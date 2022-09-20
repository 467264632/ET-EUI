namespace ET.Server
{
    //10s后离Player线
    [ComponentOf(typeof(Player))]
    public class PlayerOfflineOutTimeComponent : Entity,IAwake,IDestroy
    {
        public long Timer;
    }
}