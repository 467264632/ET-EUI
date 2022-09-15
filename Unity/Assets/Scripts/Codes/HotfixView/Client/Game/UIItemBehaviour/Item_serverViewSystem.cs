
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class Scroll_Item_serverDestroySystem : DestroySystem<Scroll_Item_server> 
	{
		protected override void Destroy( Scroll_Item_server self )
		{
			self.DestroyWidget();
		}
	}
}
