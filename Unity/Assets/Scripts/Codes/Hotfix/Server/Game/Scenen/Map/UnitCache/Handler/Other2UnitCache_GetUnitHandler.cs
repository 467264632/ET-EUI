using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace ET.Server
{
    [ActorMessageHandler(SceneType.UnitCache)]
    [FriendOf(typeof(UnitCacheComponent))]
    public class Other2UnitCache_GetUnitHandler : AMActorRpcHandler<Scene,Other2UnitCache_GetUnit,UnitCache2Other_GetUnit>
    {
        protected override async ETTask Run(Scene scene, Other2UnitCache_GetUnit request, UnitCache2Other_GetUnit response, Action reply)
        {
            // UnitCacheComponent unitCacheComponent = scene.GetComponent<UnitCacheComponent>();
            // // Dictionary<string,Entity> dictionary =  MonoPool.Instance.Fetch(typeof (Dictionary<string, Entity>)) as Dictionary<string, Entity>;
            // // var dictionary = unitCacheComponent.UnitCacheComponentDict;
            // // dictionary.Clear();
            // try
            // {
            //     if (request.ComponentNameList.Count == 0)
            //     {
            //         // dictionary.Add(nameof (Unit), null);
            //         response.ComponentNameList.Add(nameof (Unit));
            //         // response.EntityList.Add(null);
            //         foreach (string s in unitCacheComponent.UnitCacheKeyList)
            //         {
            //             response.ComponentNameList.Add(s);
            //             // response.EntityList.Add(null);
            //         }
            //     }
            //     else
            //     {
            //         foreach (string s in request.ComponentNameList)
            //         {
            //             response.ComponentNameList.Add(nameof (Unit));
            //             // response.EntityList.Add(null);
            //         }
            //     }
            //
            //     foreach (var key in response.ComponentNameList)
            //     {
            //         Entity entity = await unitCacheComponent.Get(request.UnitId, key);
            //         response.EntityList.Add(entity);
            //     }
            //
            // }
            // catch (Exception e)
            // {
            //     Log.Error($"Other2UnitCache_GetUnitHandler:{e.ToString()}");
            // }
            
            // UnitCacheComponent unitCacheComponent = scene.GetComponent<UnitCacheComponent>();
            // Dictionary<string,Entity> dictionary =  ObjectPool.Instance.Fetch(typeof (Dictionary<string, Entity>)) as Dictionary<string, Entity>;
            // Dictionary<string,Entity> dictionaryValues =  ObjectPool.Instance.Fetch(typeof (Dictionary<string, Entity>)) as Dictionary<string, Entity>;
            // try
            // {
            //     if (request.ComponentNameList.Count == 0)
            //     {
            //         dictionary.Add(nameof (Unit), null);
            //         dictionaryValues.Add(nameof (Unit), null);
            //         foreach (string s in unitCacheComponent.UnitCacheKeyList)
            //         {
            //             dictionary.Add(s, null);
            //             dictionaryValues.Add(s, null);
            //         }
            //     }
            //     else
            //     {
            //         foreach (string s in request.ComponentNameList)
            //         {
            //             dictionary.Add(s, null);
            //             dictionaryValues.Add(s, null);
            //         }
            //     }
            //     
            //     foreach (var key in dictionary.Keys)
            //     {
            //         Entity entity = await unitCacheComponent.Get(request.UnitId,key);
            //         dictionaryValues[key] = entity;
            //     }
            //     
            //     response.ComponentNameList.AddRange(dictionary.Keys);
            //     response.EntityList.AddRange(dictionaryValues.Values);
            // }
            // catch (Exception e)
            // {
            //     Log.Error($"Other2UnitCache_GetUnitHandler get Error:\n{e.ToString()}");
            // }
            //
            // finally
            // {
            //     dictionary.Clear();
            //     ObjectPool.Instance.Recycle(dictionary);
            //     dictionaryValues.Clear();
            //     ObjectPool.Instance.Recycle(dictionaryValues);
            // }
            // reply();
            // await ETTask.CompletedTask;
            
            UnitCacheComponent unitCacheComponent = scene.GetComponent<UnitCacheComponent>();
            List<string> ListKey = ObjectPool.Instance.Fetch(typeof (List<string>)) as List<string>;
            List<Entity> ListValue = ObjectPool.Instance.Fetch(typeof (List<Entity>)) as List<Entity>;
            try
            {
                if (request.ComponentNameList.Count == 0)
                {
                    ListKey.Add(nameof (Unit));
                    foreach (string s in unitCacheComponent.UnitCacheKeyList)
                    {
                        ListKey.Add(s);
                    }
                }
                else
                {
                    foreach (string s in request.ComponentNameList)
                    {
                        ListKey.Add(s);
                    }
                }
                
                foreach (var key in ListKey)
                {
                    Entity entity = await unitCacheComponent.Get(request.UnitId,key);
                    // if(entity is )
                    if (entity != null)
                    {
                        response.EntityList.Add(entity.ToBson());
                    }
                }
                response.ComponentNameList.AddRange(ListKey);
            }
            catch (Exception e)
            {
                Log.Error($"Other2UnitCache_GetUnitHandler get Error:\n{e.ToString()}");
            }
            
            finally
            {
                ListKey.Clear();
                ListValue.Clear();
                ObjectPool.Instance.Recycle(ListKey);
                ObjectPool.Instance.Recycle(ListValue);
            }
            reply();
            await ETTask.CompletedTask;
            
        }
    }
}