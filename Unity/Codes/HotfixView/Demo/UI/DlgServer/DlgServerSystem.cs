using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

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
			self.View.ELoopScrollList_ServerLoopVerticalScrollRect.AddItemRefreshListener((Transform transform,int  index) =>
			{
				self.OnScrollItemRefreshHandler(transform,index);
			});
		 
		}

		public static void ShowWindow(this DlgServer self, Entity contextData = null)
		{
			int count = self.ZoneScene().GetComponent<ServerInfoComponent>().ServerInfoList.Count;
			self.AddUIScrollItems(ref self.ScrollItemServerDict,count);
			self.View.ELoopScrollList_ServerLoopVerticalScrollRect.SetVisible(true,count);

		}

		public static void HideWindow(this  DlgServer self)
		{
			self.RemoveUIScrollItems(ref  self.ScrollItemServerDict);
		}

		public static  async ETTask  OnJoinServerClickHandler(this  DlgServer self)
		{
			
			await ETTask.CompletedTask;

		}

		public static void OnScrollItemRefreshHandler(this  DlgServer self, Transform transform ,int index)
		{
			Scroll_Item_server server = self.ScrollItemServerDict[index].BindTrans(transform);
			ServerInfo info = self.ZoneScene().GetComponent<ServerInfoComponent>().ServerInfoList[index];
			server.E_LabelText.SetText(info.ServerName);
			server.E_ToggleToggle.isOn = info.Id == self.ZoneScene().GetComponent<ServerInfoComponent>().CurrentServerId;
			// server.E_ToggleToggle.AddListener(() => { self.OnScrollItemRefreshHandler(info.Id);});
		}

		public static void OnSelectServerItemHandler(this DlgServer self, long serverId)
		{
		}
		
	}
}
