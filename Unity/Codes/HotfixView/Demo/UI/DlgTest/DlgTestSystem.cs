using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	public static  class DlgTestSystem
	{

		public static void RegisterUIEvent(this DlgTest self)
		{
			self.View.E_TestButton.AddListener(self.ETestHandler);
			self.View.EText_TestButton.AddListener(self.ETextHandler);
			self.View.ELoopScrollList_TestLoopVerticalScrollRect.AddItemRefreshListener((Transform transform, int index) =>
			{
				self.OnLoopListItemRefreshHandler(transform, index);
			});
		}

		public static void ShowWindow(this DlgTest self, Entity contextData = null)
		{
			self.View.ESCommonUI.SetLabelContent("测试界面");
			int count  = 1000;
			self.AddUIScrollItems(ref self.ScrollItemServerTestsDict,count);
			self.View.ELoopScrollList_TestLoopVerticalScrollRect.SetVisible(true,count);
		}

		public static void HideWindow(this  DlgTest self)
		{
			
			self.RemoveUIScrollItems(ref self.ScrollItemServerTestsDict);
		}

		public static void ETestHandler(this DlgTest self)
		{
			Log.Debug("aaaaaaaaaETest");
			UIComponent uiComponent = self.ZoneScene().GetComponent<UIComponent>();
			uiComponent.CloseWindow(WindowID.WindowID_Test);
			uiComponent.ShowWindow(WindowID.WindowID_RedDot);
		}
		public static void ETextHandler(this DlgTest self)
		{
			Log.Debug("aaaaaaaaaEText");
			UIComponent uiComponent = self.ZoneScene().GetComponent<UIComponent>();
			uiComponent.CloseWindow(WindowID.WindowID_Test);
			uiComponent.ShowWindow(WindowID.WindowID_Login);
		}

		public static void OnLoopListItemRefreshHandler(this DlgTest self, Transform transform, int index)
		{
			Scroll_Item_serverTest item = self.ScrollItemServerTestsDict[index].BindTrans(transform);
			item.E_serverTestTipText.text = $"{index}服";
		}
	}
}
