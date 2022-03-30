
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class Scroll_Item_role : Entity,IAwake,IDestroy,IUIScrollItem 
	{
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_Item_role BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public UnityEngine.UI.Button EButtonButton
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
     				if( this.m_EButtonButton == null )
     				{
		    			this.m_EButtonButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EButton");
     				}
     				return this.m_EButtonButton;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EButton");
     			}
     		}
     	}

		public UnityEngine.UI.Image EButtonImage
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
     				if( this.m_EButtonImage == null )
     				{
		    			this.m_EButtonImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EButton");
     				}
     				return this.m_EButtonImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EButton");
     			}
     		}
     	}

		public UnityEngine.UI.Image EHeadImage
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
     				if( this.m_EHeadImage == null )
     				{
		    			this.m_EHeadImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EButton/EHead");
     				}
     				return this.m_EHeadImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EButton/EHead");
     			}
     		}
     	}

		public UnityEngine.UI.Text ENameText
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
     				if( this.m_ENameText == null )
     				{
		    			this.m_ENameText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"EButton/EName");
     				}
     				return this.m_ENameText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"EButton/EName");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EButtonButton = null;
			this.m_EButtonImage = null;
			this.m_EHeadImage = null;
			this.m_ENameText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_EButtonButton = null;
		private UnityEngine.UI.Image m_EButtonImage = null;
		private UnityEngine.UI.Image m_EHeadImage = null;
		private UnityEngine.UI.Text m_ENameText = null;
		public Transform uiTransform = null;
	}
}
