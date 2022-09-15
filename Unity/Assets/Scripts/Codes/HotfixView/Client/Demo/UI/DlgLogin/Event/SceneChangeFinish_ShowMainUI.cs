namespace ET.Client
{
    [Event(SceneType.Current)]
    public class SceneChangeFinish_ShowCurrentSceneUI: AEvent<EventType.SceneChangeFinish>
    {
        protected override async ETTask Run(Scene scene, EventType.SceneChangeFinish args)
        {
            args.ZoneScene.GetComponent<UIComponent>().CloseWindow(WindowID.WindowID_Loading);
            args.ZoneScene.GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Main);
            await ETTask.CompletedTask;
        }
    }
    
}