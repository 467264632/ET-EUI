using System.Collections.Generic;

namespace ET.Client
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgRoles :Entity,IAwake,IUILogic
	{

		public DlgRolesViewComponent View { get => this.GetComponent<DlgRolesViewComponent>();} 

		public Dictionary<int, Scroll_Item_role> ScrollItemRoles;

	}
}
