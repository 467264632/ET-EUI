namespace ET
{
    public enum RoleInfoState
    {
        Normal = 0,
        Freeze,
    }
    
    [ChildOf]
    [ComponentOf(typeof(Unit))]
    public class RoleInfo : Entity,IAwake,ITransfer,IUnitCache
    {
        public string Name;

        public int ServerId;

        public int State;

        public long AccountId;

        public long LastLoginTime;

        public long CreateTime;
    }
}