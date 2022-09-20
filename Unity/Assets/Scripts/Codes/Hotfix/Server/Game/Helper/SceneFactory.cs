using System.Net;

namespace ET.Server
{
    public static class SceneFactory
    {
        public static async ETTask<Scene> CreateServerScene(Entity parent, long id, long instanceId, int zone, string name, SceneType sceneType, StartSceneConfig startSceneConfig = null)
        {
            await ETTask.CompletedTask;
            Scene scene = EntitySceneFactory.CreateScene(id, instanceId, zone, sceneType, name, parent);

            scene.AddComponent<MailBoxComponent, MailboxType>(MailboxType.UnOrderMessageDispatcher);

            switch (scene.SceneType)
            {
                case SceneType.Router:
                    scene.AddComponent<RouterComponent, IPEndPoint, string>(startSceneConfig.OuterIPPort,
                        startSceneConfig.StartProcessConfig.InnerIP
                    );
                    break;
                case SceneType.RouterManager:
                    scene.AddComponent<HttpComponent, string>($"http://{startSceneConfig.OuterIPPort}/");
                    break;
                case SceneType.Realm:
                    scene.AddComponent<NetServerComponent, IPEndPoint>(startSceneConfig.InnerIPOutPort);
                    // scene.AddComponent<TokenComponent>();
                    break;
                case SceneType.Gate:
                    scene.AddComponent<NetServerComponent, IPEndPoint>(startSceneConfig.InnerIPOutPort);
                    scene.AddComponent<PlayerComponent>();
                    scene.AddComponent<GateSessionKeyComponent>();
                    break;
                case SceneType.Map:
                    scene.AddComponent<UnitComponent>();
                    scene.AddComponent<AOIManagerComponent>();
                    break;
                case SceneType.Location:
                    scene.AddComponent<LocationComponent>();
                    break;
                case SceneType.Robot:
                    scene.AddComponent<RobotManagerComponent>();
                    break;
                case SceneType.BenchmarkServer:
                    scene.AddComponent<BenchmarkServerComponent>();
                    scene.AddComponent<NetServerComponent, IPEndPoint>(startSceneConfig.OuterIPPort);
                    break;
                case SceneType.BenchmarkClient:
                    scene.AddComponent<BenchmarkClientComponent>();
                    break;
                case SceneType.Account:
                    scene.AddComponent<NetServerComponent, IPEndPoint>(startSceneConfig.InnerIPOutPort);
                    scene.AddComponent<TokenComponent>();//记录accountId与Token
                    scene.AddComponent<AccountSessionsComponent>();//记录accountId与SessionInstanceId
                    scene.AddComponent<AccountSessionKeyComponent>();//C2R_GetAccountKey 记录账号被分配到哪个Account
                    scene.AddComponent<ServerInfoManagerComponent>();
                    break;
                case SceneType.LoginCenter:
                    scene.AddComponent<LoginInfoRecordComponent>();//记录登录成功进入gate的accountId与zone
                    break;
                case SceneType.UnitCache:
                    scene.AddComponent<UnitCacheComponent>();
                    break;
                // case SceneType.Rank:
                //     await scene.AddComponent<RankInfosComponent>().LoadRankInfo();
                //     break;
                // case SceneType.ChatInfo:
                //     scene.AddComponent<ChatInfoUnitsComponent>();
                //     break;
            }

            return scene;
        }
    }
}