namespace ET.Client
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgShowSystemMessageBox :Entity,IAwake,IUILogic
	{

		public DlgShowSystemMessageBoxViewComponent View { get => this.GetComponent<DlgShowSystemMessageBoxViewComponent>();} 

		 

	}
}
