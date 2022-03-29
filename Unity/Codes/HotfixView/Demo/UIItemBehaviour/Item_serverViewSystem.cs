
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_Item_serverDestroySystem : DestroySystem<Scroll_Item_server> 
	{
		public override void Destroy( Scroll_Item_server self )
		{
			self.DestroyWidget();
		}
	}
}
