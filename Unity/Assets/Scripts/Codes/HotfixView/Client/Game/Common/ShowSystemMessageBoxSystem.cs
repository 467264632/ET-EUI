// using UnityEngine;
//
// namespace ET.Client
// {
//     [Event(SceneType.Client)]
//     public class ShowSystemMessageBoxSystem: AEvent<EventType.ShowSystemMessageBox>
//     {
//         protected override async ETTask Run(Scene scene, EventType.ShowSystemMessageBox args)
//         {
//             int Errcode = args.ErrCode;
//             scene.GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Login);
//             scene.GetComponent<UIComponent>().GetDlgLogic<DlgShowSystemMessageBox>().View.E_TipText.text = args.ErrCode.ToString();
//             await ETTask.CompletedTask;
//         }
//     }
// }