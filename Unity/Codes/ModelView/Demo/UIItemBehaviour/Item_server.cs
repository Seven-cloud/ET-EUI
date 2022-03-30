
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class Scroll_Item_server : Entity,IAwake,IDestroy,IUIScrollItem 
	{
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_Item_server BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public UnityEngine.UI.Button E_SelectItemButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_SelectItemButton == null )
     				{
		    			this.m_E_SelectItemButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_SelectItem");
     				}
     				return this.m_E_SelectItemButton;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_SelectItem");
     			}
     		}
     	}

		public UnityEngine.UI.Image E_SelectItemImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_SelectItemImage == null )
     				{
		    			this.m_E_SelectItemImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_SelectItem");
     				}
     				return this.m_E_SelectItemImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_SelectItem");
     			}
     		}
     	}

		public UnityEngine.UI.Text E_LabelText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_LabelText == null )
     				{
		    			this.m_E_LabelText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_SelectItem/E_Label");
     				}
     				return this.m_E_LabelText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_SelectItem/E_Label");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_SelectItemButton = null;
			this.m_E_SelectItemImage = null;
			this.m_E_LabelText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_SelectItemButton = null;
		private UnityEngine.UI.Image m_E_SelectItemImage = null;
		private UnityEngine.UI.Text m_E_LabelText = null;
		public Transform uiTransform = null;
	}
}
