using System;

namespace ET.Server
{
    [ActorMessageHandler(SceneType.Realm)]
    public class A2R_GetRealmKeyHandler: AMActorRpcHandler<Scene, A2R_GetRealmKey, R2A_GetRealmKey>
    {
        protected override async ETTask Run(Scene scene, A2R_GetRealmKey request, R2A_GetRealmKey response, Action reply)
        {
            if (scene.SceneType != SceneType.Realm)
            {
                Log.Error($"请求的Scene错误，当前Scene为：{scene.SceneType}");
                response.Error = ErrorCode.ERR_RequestSceneTypeError;
                reply();
                return;
            }
            
            string key = TimeHelper.ServerNow().ToString() +  RandomHelper.RandInt64().ToString();
            Log.Info(scene.GetComponent<TokenComponent>().ToString());
            Log.Info(request.AccountId.ToString());
            scene.GetComponent<TokenComponent>()?.Remove(request.AccountId);
            scene.GetComponent<TokenComponent>().Add(request.AccountId,key);
            response.RealmKey = key.ToString();
            reply();
            await ETTask.CompletedTask;
        }
    }
}