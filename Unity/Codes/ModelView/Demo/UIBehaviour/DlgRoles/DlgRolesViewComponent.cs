
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgRolesViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.InputField E_NameInputField
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_NameInputField == null )
     			{
		    		this.m_E_NameInputField = UIFindHelper.FindDeepChild<UnityEngine.UI.InputField>(this.uiTransform.gameObject,"Sprite_BackGround/E_Name");
     			}
     			return this.m_E_NameInputField;
     		}
     	}

		public UnityEngine.UI.Image E_NameImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_NameImage == null )
     			{
		    		this.m_E_NameImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Sprite_BackGround/E_Name");
     			}
     			return this.m_E_NameImage;
     		}
     	}

		public UnityEngine.UI.Toggle E_Toggle1Toggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_Toggle1Toggle == null )
     			{
		    		this.m_E_Toggle1Toggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"Sprite_BackGround/E_Toggle1");
     			}
     			return this.m_E_Toggle1Toggle;
     		}
     	}

		public UnityEngine.UI.Button E_DeleRoleButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_DeleRoleButton == null )
     			{
		    		this.m_E_DeleRoleButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Sprite_BackGround/E_DeleRole");
     			}
     			return this.m_E_DeleRoleButton;
     		}
     	}

		public UnityEngine.UI.Image E_DeleRoleImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_DeleRoleImage == null )
     			{
		    		this.m_E_DeleRoleImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Sprite_BackGround/E_DeleRole");
     			}
     			return this.m_E_DeleRoleImage;
     		}
     	}

		public UnityEngine.UI.Button E_CreateRoleButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CreateRoleButton == null )
     			{
		    		this.m_E_CreateRoleButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Sprite_BackGround/E_CreateRole");
     			}
     			return this.m_E_CreateRoleButton;
     		}
     	}

		public UnityEngine.UI.Image E_CreateRoleImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CreateRoleImage == null )
     			{
		    		this.m_E_CreateRoleImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Sprite_BackGround/E_CreateRole");
     			}
     			return this.m_E_CreateRoleImage;
     		}
     	}

		public UnityEngine.UI.Button E_PlayButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_PlayButton == null )
     			{
		    		this.m_E_PlayButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Sprite_BackGround/E_Play");
     			}
     			return this.m_E_PlayButton;
     		}
     	}

		public UnityEngine.UI.Image E_PlayImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_PlayImage == null )
     			{
		    		this.m_E_PlayImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Sprite_BackGround/E_Play");
     			}
     			return this.m_E_PlayImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_NameInputField = null;
			this.m_E_NameImage = null;
			this.m_E_Toggle1Toggle = null;
			this.m_E_DeleRoleButton = null;
			this.m_E_DeleRoleImage = null;
			this.m_E_CreateRoleButton = null;
			this.m_E_CreateRoleImage = null;
			this.m_E_PlayButton = null;
			this.m_E_PlayImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.InputField m_E_NameInputField = null;
		private UnityEngine.UI.Image m_E_NameImage = null;
		private UnityEngine.UI.Toggle m_E_Toggle1Toggle = null;
		private UnityEngine.UI.Button m_E_DeleRoleButton = null;
		private UnityEngine.UI.Image m_E_DeleRoleImage = null;
		private UnityEngine.UI.Button m_E_CreateRoleButton = null;
		private UnityEngine.UI.Image m_E_CreateRoleImage = null;
		private UnityEngine.UI.Button m_E_PlayButton = null;
		private UnityEngine.UI.Image m_E_PlayImage = null;
		public Transform uiTransform = null;
	}
}
