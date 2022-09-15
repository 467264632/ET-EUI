// namespace ET.Client
// {
//     [Event(SceneType.Current)]
//     public class SceneChangeFinish_StartAdventure : AEvent<EventType.SceneChangeFinish>
//     {
//         protected override async ETTask Run(Scene scene, EventType.SceneChangeFinish args)
//         {
//             Unit unit = UnitHelper.GetMyUnitFromCurrentScene(args.CurrentScene);
//
//             if (unit.GetComponent<NumericComponent>().GetAsInt(NumericType.AdventureState) == 0)
//             {
//                 return;
//             }
//
//             await TimerComponent.Instance.WaitAsync(3000);
//             
//             // scene.GetComponent<AdventureComponent>().StartAdventure().Coroutine();
//             await ETTask.CompletedTask;
//         }
//     }
// }