
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgShowSystemMessageBoxViewComponentAwakeSystem : AwakeSystem<DlgShowSystemMessageBoxViewComponent> 
	{
		protected override void Awake(DlgShowSystemMessageBoxViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgShowSystemMessageBoxViewComponentDestroySystem : DestroySystem<DlgShowSystemMessageBoxViewComponent> 
	{
		protected override void Destroy(DlgShowSystemMessageBoxViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
