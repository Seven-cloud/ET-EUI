
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgServerViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button E_JoinServerButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_JoinServerButton == null )
     			{
		    		this.m_E_JoinServerButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Sprite_BackGround/E_JoinServer");
     			}
     			return this.m_E_JoinServerButton;
     		}
     	}

		public UnityEngine.UI.Image E_JoinServerImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_JoinServerImage == null )
     			{
		    		this.m_E_JoinServerImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Sprite_BackGround/E_JoinServer");
     			}
     			return this.m_E_JoinServerImage;
     		}
     	}

		public UnityEngine.UI.LoopVerticalScrollRect ELoopScrollList_ServerLoopVerticalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELoopScrollList_ServerLoopVerticalScrollRect == null )
     			{
		    		this.m_ELoopScrollList_ServerLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject,"Sprite_BackGround/ELoopScrollList_Server");
     			}
     			return this.m_ELoopScrollList_ServerLoopVerticalScrollRect;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_JoinServerButton = null;
			this.m_E_JoinServerImage = null;
			this.m_ELoopScrollList_ServerLoopVerticalScrollRect = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_JoinServerButton = null;
		private UnityEngine.UI.Image m_E_JoinServerImage = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_ELoopScrollList_ServerLoopVerticalScrollRect = null;
		public Transform uiTransform = null;
	}
}
