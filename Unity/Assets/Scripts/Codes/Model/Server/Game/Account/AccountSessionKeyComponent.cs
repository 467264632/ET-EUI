using System.Collections.Generic;

namespace ET.Server
{
    [ComponentOf(typeof(Scene))]
    public class AccountSessionKeyComponent : Entity, IAwake,IDestroy
    {
        public readonly Dictionary<long, string> accountSessionKeyDcit = new Dictionary<long, string>();
    }
}