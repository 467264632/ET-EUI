using System;

namespace ET.Server
{
    
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class FriendClassAttribute : Attribute
    {
        public Type Type;
        public FriendClassAttribute(Type type)
        {
            this.Type = type;
        }
    }
}