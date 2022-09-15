

namespace ET.Server
{
    public class UnitSystem: AwakeSystem<Unit, int>
    {
        protected override void Awake(Unit self, int configId)
        {
            self.ConfigId = configId;
        }
    }
}