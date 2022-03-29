
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

		public UnityEngine.UI.Toggle E_ToggleToggle
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
     				if( this.m_E_ToggleToggle == null )
     				{
		    			this.m_E_ToggleToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"E_Toggle");
     				}
     				return this.m_E_ToggleToggle;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"E_Toggle");
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
		    			this.m_E_LabelText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Label");
     				}
     				return this.m_E_LabelText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Label");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_ToggleToggle = null;
			this.m_E_LabelText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Toggle m_E_ToggleToggle = null;
		private UnityEngine.UI.Text m_E_LabelText = null;
		public Transform uiTransform = null;
	}
}
