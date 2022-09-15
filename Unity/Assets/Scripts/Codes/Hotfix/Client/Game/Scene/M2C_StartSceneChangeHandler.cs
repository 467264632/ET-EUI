namespace ET.Client
{
    [MessageHandler(SceneType.Client)]
    public class M2C_StartSceneChangeHandler : AMHandler<M2C_StartSceneChange>
    {
        protected override async ETTask Run(Session session, M2C_StartSceneChange message)
        {
            SceneChangeHelper.SceneChangeTo(session.ClientScene(), message.SceneName, message.SceneInstanceId).Coroutine();
            await ETTask.CompletedTask;
        }
    }
}