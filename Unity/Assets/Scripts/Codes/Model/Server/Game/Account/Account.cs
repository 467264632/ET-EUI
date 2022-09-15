namespace ET.Server

{
    public enum AccountType
    {
        General = 0,    //正常
        BlackList = 1,  //黑名单
        
    }
    
    [ComponentOf(typeof(Scene))]
    [ChildOf(typeof(AccountsZone))]
    public class Account: Entity, IAwake
    {
        public string AccountName;      //账号
        public string Password;         //密码
        public long CreateTime;         //创建时间
        public int AccountType;         //账号类型
    }
}

