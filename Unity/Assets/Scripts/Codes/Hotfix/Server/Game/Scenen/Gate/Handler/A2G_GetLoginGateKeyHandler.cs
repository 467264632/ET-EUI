using System;

namespace ET.Server
{
    [ActorMessageHandler(SceneType.Gate)]
    public class A2G_GetLoginGateKeyHandler : AMActorRpcHandler<Scene, A2G_GetLoginGateKey, G2A_GetLoginGateKey>
    {
        protected override async ETTask Run(Scene scene, A2G_GetLoginGateKey request, G2A_GetLoginGateKey response, Action reply)
        {
            if (scene.SceneType != SceneType.Gate)
            {
                Log.Error($"请求的Scene错误，当前Scene为：{scene.SceneType}");
                response.Error = ErrorCode.ERR_RequestSceneTypeError;
                reply();
                return;
            }
            
            string key     =  RandomHelper.RandInt64().ToString() + TimeHelper.ServerNow().ToString();
            scene.GetComponent<GateSessionKeyComponent>().Remove(request.AccountId);
            scene.GetComponent<GateSessionKeyComponent>().Add(request.AccountId, key);
            response.GateKey = key;
            reply();
            await ETTask.CompletedTask;
        }
    }
}