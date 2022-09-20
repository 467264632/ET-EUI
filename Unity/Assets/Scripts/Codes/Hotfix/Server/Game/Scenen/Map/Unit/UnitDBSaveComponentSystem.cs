using System;

namespace ET.Server
{
    
    public class UnitDBSaveComponentAwakeSystem: AwakeSystem<UnitDBSaveComponent>
    {
        protected override void Awake(UnitDBSaveComponent self)
        {
            self.Timer = TimerComponent.Instance.NewRepeatedTimer(10000, TimerCallbackId.SaveChangeDBData, self);
        }
    }
    
    public class UnitDBSaveComponentDestroySystem: DestroySystem<UnitDBSaveComponent>
    {
        protected override void Destroy(UnitDBSaveComponent self)
        {
            TimerComponent.Instance.Remove(ref self.Timer);
        }
    }
    
    public class UnitAddComponentSystem : AddComponentSystem<Unit>
    {
        protected override void AddComponent(Unit unit, Entity component)
        {
            Type type = component.GetType();
            if (!(typeof (IUnitCache).IsAssignableFrom(type)) )
            {
                return;
            }
            unit.GetComponent<UnitDBSaveComponent>()?.AddChange(type);
        }
    }
    
    public class UnitGetComponentSystem : GetComponentSystem<Unit>
    {
        protected override void GetComponent(Unit unit, Entity component)
        {
            Type type = component.GetType();
            if (!(typeof (IUnitCache).IsAssignableFrom(type)) )
            {
                return;
            }
            unit.GetComponent<UnitDBSaveComponent>()?.AddChange(type);
        }
    }

    [Callback(TimerCallbackId.SaveChangeDBData)]
    public class  UnitDBSaveComponentTimer : ATimer<UnitDBSaveComponent>
    {
        protected override void Run(UnitDBSaveComponent self)
        {
            try
            {
                if ( self.IsDisposed || self.Parent == null )
                {
                    return;
                }
                
                if ( self.DomainScene() == null )
                {
                    return;
                }

                self?.SaveChange();
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
        }
    }
    
    
    public static class UnitDBSaveComponentSystem
    {
        public static void AddChange(this UnitDBSaveComponent self,Type t)
        {
            self.EntityChangeTypeSet.Add(t);
        }
        
        public static void  SaveChange(this UnitDBSaveComponent self)
        {
            if (self.EntityChangeTypeSet.Count <= 0)
            {
                return;
            }
            Unit unit = self.GetParent<Unit>();
            if (unit == null)
            {
                Log.Error($"UnitDBSaveComponent SaveChange:unit = null");
            }
            Other2UnitCache_AddOrUpdateUnit message = new Other2UnitCache_AddOrUpdateUnit() { UnitId = unit.Id, };
            message.EntityTypes.Add(unit.GetType().FullName);
            message.EntityBytes.Add(MongoHelper.ToBson(unit));
            foreach (Type type in self.EntityChangeTypeSet)
            {
                Entity entity = unit.GetComponent(type);
                if ( entity == null || entity.IsDisposed )
                {
                    continue;
                }
                Log.Debug("开始保存变化部分的Entity数据 : " + type.FullName);
                message.EntityTypes.Add(type.FullName);
                message.EntityBytes.Add(MongoHelper.ToBson(entity));
            }
            self.EntityChangeTypeSet.Clear();
            MessageHelper.CallActor(GetSceneHelper.GetUnitCache(unit.Id).InstanceId, message).Coroutine();
        }
    }
}