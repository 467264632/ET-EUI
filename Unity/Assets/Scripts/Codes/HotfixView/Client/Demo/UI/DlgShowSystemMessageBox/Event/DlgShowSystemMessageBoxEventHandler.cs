namespace ET.Client
{
	[FriendOf(typeof(WindowCoreData))]
	[FriendOf(typeof(UIBaseWindow))]
	[AUIEvent(WindowID.WindowID_ShowSystemMessageBox)]
	public  class DlgShowSystemMessageBoxEventHandler : IAUIEventHandler
	{

		public void OnInitWindowCoreData(UIBaseWindow uiBaseWindow)
		{
		  uiBaseWindow.WindowData.windowType = UIWindowType.Normal; 
		}

		public void OnInitComponent(UIBaseWindow uiBaseWindow)
		{
		  uiBaseWindow.AddComponent<DlgShowSystemMessageBox>().AddComponent<DlgShowSystemMessageBoxViewComponent>();
		}

		public void OnRegisterUIEvent(UIBaseWindow uiBaseWindow)
		{
		  uiBaseWindow.GetComponent<DlgShowSystemMessageBox>().RegisterUIEvent(); 
		}

		public void OnShowWindow(UIBaseWindow uiBaseWindow, Entity contextData = null)
		{
		  uiBaseWindow.GetComponent<DlgShowSystemMessageBox>().ShowWindow(contextData); 
		  long tillTime = TimeHelper.ClientFrameTime() + 1500;
		  // TimerComponent.Instance.NewOnceTimer(tillTime, TimerCoreCallbackId.DlgShowSystemMessageBox, this);
		  // TimerComponent.Instance.NewOnceTimer(tillTime, TimerCoreCallbackId.CoroutineTimeout, () => { return OnHideWindow;});
		}

		public void OnHideWindow(UIBaseWindow uiBaseWindow)
		{
		}

		public void BeforeUnload(UIBaseWindow uiBaseWindow)
		{
		}

	}
}
