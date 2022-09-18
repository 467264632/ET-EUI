using System;

namespace ET.Server
{
    [MessageHandler(SceneType.Account)]
    public class C2A_GetGateKeyHandler : AMRpcHandler<C2A_GetGateKey,A2C_GetGateKey>
    {
        protected override async ETTask Run(Session session, C2A_GetGateKey request, A2C_GetGateKey response, Action reply)
        {
            if (session.DomainScene().SceneType != SceneType.Account)
            {
                Log.Error($"请求的Scene错误，当前Scene为：{session.DomainScene().SceneType}");
                session.Dispose();
                return;
            }
            
            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                response.Error = ErrorCode.ERR_RequestRepeatedly;
                reply();
                session.Disconnect().Coroutine();
                return;
            }
            
            string token = session.DomainScene().GetComponent<TokenComponent>().Get(request.AccountId);

            if (token == null || token != request.Token)
            {
                response.Error = ErrorCode.ERR_TokenError;
                reply();
                session?.Disconnect().Coroutine();
                return;
            }

            using (session.AddComponent<SessionLockingComponent>())
            {
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginAccount, request.AccountId))
                {
                    StartSceneConfig gateStartSceneConfig = GetSceneHelper.GetGate(request.ServerId,request.AccountId);

                    G2A_GetLoginGateKey r2AGetRealmKey =  (G2A_GetLoginGateKey) await MessageHelper.CallActor(gateStartSceneConfig.InstanceId, new A2G_GetLoginGateKey() { AccountId = request.AccountId });

                    if (r2AGetRealmKey.Error != ErrorCode.ERR_Success)
                    {
                        response.Error = r2AGetRealmKey.Error;
                        reply();
                        session?.Disconnect().Coroutine();
                        return;
                    }

                    response.GateKey = r2AGetRealmKey.GateKey;
                    response.GateAddress = gateStartSceneConfig.OuterIPPort.ToString();
                    response.Error = ErrorCode.ERR_Success;
                    reply();
                    session?.Disconnect().Coroutine();
                }
            }


            await ETTask.CompletedTask;
        }
    }
}