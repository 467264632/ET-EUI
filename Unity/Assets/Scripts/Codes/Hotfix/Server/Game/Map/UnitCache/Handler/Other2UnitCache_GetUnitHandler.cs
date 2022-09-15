using System;
using System.Collections.Generic;

namespace ET.Server
{
    [ActorMessageHandler(SceneType.UnitCache)]
    [FriendOf(typeof(UnitCacheComponent))]
    public class Other2UnitCache_GetUnitHandler : AMActorRpcHandler<Scene,Other2UnitCache_GetUnit,UnitCache2Other_GetUnit>
    {
        protected override async ETTask Run(Scene scene, Other2UnitCache_GetUnit request, UnitCache2Other_GetUnit response, Action reply)
        {
            UnitCacheComponent unitCacheComponent = scene.GetComponent<UnitCacheComponent>();
            // Dictionary<string,Entity> dictionary =  MonoPool.Instance.Fetch(typeof (Dictionary<string, Entity>)) as Dictionary<string, Entity>;
            // var dictionary = unitCacheComponent.UnitCacheComponentDict;
            // dictionary.Clear();
            try
            {
                if (request.ComponentNameList.Count == 0)
                {
                    // dictionary.Add(nameof (Unit), null);
                    response.ComponentNameList.Add(nameof (Unit));
                    // response.EntityList.Add(null);
                    foreach (string s in unitCacheComponent.UnitCacheKeyList)
                    {
                        response.ComponentNameList.Add(s);
                        // response.EntityList.Add(null);
                    }
                }
                else
                {
                    foreach (string s in request.ComponentNameList)
                    {
                        response.ComponentNameList.Add(nameof (Unit));
                        // response.EntityList.Add(null);
                    }
                }

                foreach (var key in response.ComponentNameList)
                {
                    Entity entity = await unitCacheComponent.Get(request.UnitId, key);
                    response.EntityList.Add(entity);
                }

            }
            catch (Exception e)
            {
                Log.Error($"Other2UnitCache_GetUnitHandler:{e.ToString()}");
            }
            
            reply();
            await ETTask.CompletedTask;
        }
    }
}