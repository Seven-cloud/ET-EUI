using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace ET
{
	public static  class DlgServerSystem
	{

		public static void RegisterUIEvent(this DlgServer self)
		{
			self.View.E_JoinServerButton.AddListenAsync(() =>
				{
					return self.OnJoinServerClickHandler();
				}
			);
			self.View.ELoopScrollList_ServerLoopVerticalScrollRect.AddItemRefreshListener((transform,index) =>
			{
				self.OnScrollItemRefreshHandler(transform,index);
			});
		 
		}

		public static void ShowWindow(this DlgServer self, Entity contextData = null)
		{
			int count = self.ZoneScene().GetComponent<ServerInfoComponent>().ServerInfoList.Count;
			
			Log.Error( "========count===="+ count .ToString());
			self.AddUIScrollItems(ref self.ScrollItemServerDict,count);
			self.View.ELoopScrollList_ServerLoopVerticalScrollRect.SetVisible(true,count);

		}

		public static void HideWindow(this  DlgServer self)
		{
			self.RemoveUIScrollItems(ref  self.ScrollItemServerDict);
		}

		public static  async ETTask  OnJoinServerClickHandler(this  DlgServer self)
		{
			
			bool isSelect = self.ZoneScene().GetComponent<ServerInfoComponent>().CurrentServerId != 0;
			if (!isSelect)
			{
				Log.Error("请选择区服");
				return;
			}

			try
			{
				int errorCode = await LoginHelper.GetRole(self.ZoneScene());

				if (errorCode != ErrorCode.ERR_Success)
				{
					Log.Debug(errorCode.ToString());
					return;
				}
				self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Roles);
				self.ZoneScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_Server);
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}
			await ETTask.CompletedTask;


		}

		public static void OnScrollItemRefreshHandler(this DlgServer self, Transform transform, int index)
		{
			Scroll_Item_server server = self.ScrollItemServerDict[index].BindTrans(transform);
			ServerInfo info = self.ZoneScene().GetComponent<ServerInfoComponent>().ServerInfoList[index];
			server.E_LabelText.SetText(info.ServerName);
			server.E_ToggleToggle.isOn = info.Id == self.ZoneScene().GetComponent<ServerInfoComponent>().CurrentServerId;
			Log.Error("============" + server.E_ToggleToggle.isOn);
			server.E_ToggleToggle.AddListener((t) =>
			{
				Log.Error("================xuan");
					 self.OnSelectServerItemHandler( info.Id);
			});
		}

		public static void  OnSelectServerItemHandler(this DlgServer self, long serverId)
		{
			self.ZoneScene().GetComponent<ServerInfoComponent>().CurrentServerId = int.Parse(serverId.ToString());
			Log.Debug($"当前选的服务器 Id 是：{serverId}");
			self.View.ELoopScrollList_ServerLoopVerticalScrollRect.RefillCells();
		}
	}
}
