
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgTestViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.RectTransform EGBackGroundRectTransform
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EGBackGroundRectTransform == null )
     			{
		    		this.m_EGBackGroundRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject,"EGBackGround");
     			}
     			return this.m_EGBackGroundRectTransform;
     		}
     	}

		public UnityEngine.UI.Button E_EnterMapButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_EnterMapButton == null )
     			{
		    		this.m_E_EnterMapButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/E_EnterMap");
     			}
     			return this.m_E_EnterMapButton;
     		}
     	}

		public UnityEngine.UI.Image E_EnterMapImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_EnterMapImage == null )
     			{
		    		this.m_E_EnterMapImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/E_EnterMap");
     			}
     			return this.m_E_EnterMapImage;
     		}
     	}

		public UnityEngine.UI.Button E_TestButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_TestButton == null )
     			{
		    		this.m_E_TestButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/E_Test");
     			}
     			return this.m_E_TestButton;
     		}
     	}

		public UnityEngine.UI.Image E_TestImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_TestImage == null )
     			{
		    		this.m_E_TestImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/E_Test");
     			}
     			return this.m_E_TestImage;
     		}
     	}

		public UnityEngine.UI.Button EText_TestButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EText_TestButton == null )
     			{
		    		this.m_EText_TestButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EGBackGround/EText_Test");
     			}
     			return this.m_EText_TestButton;
     		}
     	}

		public UnityEngine.UI.Image EText_TestImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EText_TestImage == null )
     			{
		    		this.m_EText_TestImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EGBackGround/EText_Test");
     			}
     			return this.m_EText_TestImage;
     		}
     	}

		public ESCommonUI ESCommonUI
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_escommonui == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"ESCommonUI");
		    	   this.m_escommonui = this.AddChild<ESCommonUI,Transform>(subTrans);
     			}
     			return this.m_escommonui;
     		}
     	}

		public UnityEngine.UI.LoopVerticalScrollRect ELoopScrollList_TestLoopVerticalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELoopScrollList_TestLoopVerticalScrollRect == null )
     			{
		    		this.m_ELoopScrollList_TestLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject,"ELoopScrollList_Test");
     			}
     			return this.m_ELoopScrollList_TestLoopVerticalScrollRect;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EGBackGroundRectTransform = null;
			this.m_E_EnterMapButton = null;
			this.m_E_EnterMapImage = null;
			this.m_E_TestButton = null;
			this.m_E_TestImage = null;
			this.m_EText_TestButton = null;
			this.m_EText_TestImage = null;
			this.m_escommonui?.Dispose();
			this.m_escommonui = null;
			this.m_ELoopScrollList_TestLoopVerticalScrollRect = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EGBackGroundRectTransform = null;
		private UnityEngine.UI.Button m_E_EnterMapButton = null;
		private UnityEngine.UI.Image m_E_EnterMapImage = null;
		private UnityEngine.UI.Button m_E_TestButton = null;
		private UnityEngine.UI.Image m_E_TestImage = null;
		private UnityEngine.UI.Button m_EText_TestButton = null;
		private UnityEngine.UI.Image m_EText_TestImage = null;
		private ESCommonUI m_escommonui = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_ELoopScrollList_TestLoopVerticalScrollRect = null;
		public Transform uiTransform = null;
	}
}
