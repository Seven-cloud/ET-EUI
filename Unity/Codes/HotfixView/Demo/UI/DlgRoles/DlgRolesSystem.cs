using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	public static  class DlgRolesSystem
	{

		public static void RegisterUIEvent(this DlgRoles self)
		{
			//注册事件
			self.View.E_CreateRoleButton.AddListenAsync(() =>
			{
				return self.OnCreateRoleClickHandler();
			});
			self.View.E_DeleRoleButton.AddListenAsync(() =>
			{
				return self.OnDeleRoleClickHandler();
			});
			self.View.E_PlayButton.AddListenAsync(() =>
			{
				return self.OnPlayGameClickHandler();
			});
			self.View.ELoopScrollList_RoleLoopHorizontalScrollRect.AddItemRefreshListener((Transform transform,int  index) =>
			{
				self.OnRoleListRefreshHandler(transform,index);
			});
		}

		public static void ShowWindow(this DlgRoles self, Entity contextData = null)
		{
			self.RefreshRoleItems();
		}

		public static void RefreshRoleItems(this  DlgRoles self)
		{
			int count = self.ZoneScene().GetComponent<RoleInfoComponent>().RoleInfos.Count;
			self.AddUIScrollItems(ref  self.ScrollItemRoles,count);
			self.View.ELoopScrollList_RoleLoopHorizontalScrollRect.SetVisible(true,count);
		}

		public static void OnRoleListRefreshHandler(this  DlgRoles self,  Transform transform ,int index)
		{
			Scroll_Item_role itemRole = self.ScrollItemRoles[index].BindTrans(transform);
			RoleInfo info = self.ZoneScene().GetComponent<RoleInfoComponent>().RoleInfos[index];
			itemRole.ENameText.SetText(info.Name);
			itemRole.EButtonImage.color = info.Id == self.ZoneScene().GetComponent<RoleInfoComponent>().CurrentRoleId? Color.cyan : Color.white;
			itemRole.EButtonButton.AddListener(() =>
			{
				self.OnSelectItemHandler(info.Id);
			});
		}
		
		public static void  OnSelectItemHandler(this DlgRoles self, long RoleId)
		{
			self.ZoneScene().GetComponent<RoleInfoComponent>().CurrentRoleId = long.Parse(RoleId.ToString());
			Log.Debug($"当前选的角色 Id 是：{RoleId}");
			self.View.ELoopScrollList_RoleLoopHorizontalScrollRect.RefillCells();
		}
		public static async ETTask OnCreateRoleClickHandler(this DlgRoles self)
		{
			string name = self.View.E_NameInputField.text;
			if (string.IsNullOrEmpty(name))
			{
				Log.Error("Name Is Null");
				return;
			}

			try
			{
				int errorcode = await  LoginHelper.CreateRole(self.ZoneScene(), name);

				if (errorcode != ErrorCode.ERR_Success)
				{
					Log.Error(errorcode.ToString());
					return;
				}
				self.RefreshRoleItems();
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}
		}
		public static async ETTask OnDeleRoleClickHandler(this DlgRoles self)
		{

			if (self.ZoneScene().GetComponent<RoleInfoComponent>().CurrentRoleId == 0)
			{
				Log.Error("请选择需要删除的角色");
				return;
			}

			try
			{
				int errorCode = await LoginHelper.DeleteRole(self.ZoneScene());
				if (errorCode != ErrorCode.ERR_Success)
				{
					Log.Error(errorCode.ToString());
					return;
				}
				
				self.RefreshRoleItems();
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
				
			}
			await ETTask.CompletedTask;
		}
		public static async ETTask OnPlayGameClickHandler(this DlgRoles self)
		{
			if (self.ZoneScene().GetComponent<RoleInfoComponent>().CurrentRoleId == 0)
			{
				Log.Error("请选择进入游戏的角色");
				return;
			}

			try
			{
				int errorcode = await LoginHelper.GetRealmmKey(self.ZoneScene());
				if (errorcode != ErrorCode.ERR_Success)
				{
					Log.Error(errorcode.ToString());
					return;
				}

				errorcode = await LoginHelper.EnterGame(self.ZoneScene());
				if (errorcode != ErrorCode.ERR_Success)
				{
					Log.Error(errorcode.ToString());
					return;
				}

				Log.Debug("========进入角色成功");
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}
			await ETTask.CompletedTask;
		}
	}
}
