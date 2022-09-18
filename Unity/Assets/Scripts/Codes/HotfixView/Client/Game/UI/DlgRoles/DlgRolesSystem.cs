using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(RoleInfo))]
	[FriendOf(typeof(RoleInfosComponent))]
	[FriendOf(typeof(DlgRoles))]
	public static  class DlgRolesSystem
	{

		public static void RegisterUIEvent(this DlgRoles self)
		{
			self.View.E_ConfirmButton.AddListenerAsync(() => { return self.OnConfirmClickHandler(); });
			self.View.E_CreateRoleButton.AddListenerAsync(() => { return self.OnCreateRoleClickHandler();});
			self.View.E_DeleteRoleButton.AddListenerAsync(() => { return self.OnDeleteRoleClickHandler();});
			self.View.E_RolesLoopVerticalScrollRect.AddItemRefreshListener((Transform transform,int index) => { self.OnRoleListRefreshHandler(transform,index);});
		}

		public static void ShowWindow(this DlgRoles self, Entity contextData = null)
		{
			self.RefreshRoleItems();
		}

		public static void RefreshRoleItems(this DlgRoles self)
		{
			int count = self.ClientScene().GetComponent<RoleInfosComponent>().RoleInfos.Count;
			self.AddUIScrollItems(ref self.ScrollItemRoles,count);
			self.View.E_RolesLoopVerticalScrollRect.SetVisible(true,count);
		}
		
		public static void OnRoleListRefreshHandler(this DlgRoles self, Transform transform, int index)
		{
			Scroll_Item_role scrollItemRole = self.ScrollItemRoles[index].BindTrans(transform);
			RoleInfo info = self.ClientScene().GetComponent<RoleInfosComponent>().RoleInfos[index];

			scrollItemRole.E_RoleImage.color = info.Id == self.ClientScene().GetComponent<RoleInfosComponent>().CurrentRoleId? Color.green : Color.gray;
			
			scrollItemRole.E_RoleNameText.SetText(info.Name);
			scrollItemRole.E_RoleButton.AddListener(() => { self.OnRoleItemClickHandler(info.Id); });
		}

		public static void OnRoleItemClickHandler(this DlgRoles self, long roleId)
		{
			self.ClientScene().GetComponent<RoleInfosComponent>().CurrentRoleId = roleId;
			self.View.E_RolesLoopVerticalScrollRect.RefillCells();
		}
		
		public static async ETTask OnCreateRoleClickHandler(this DlgRoles self)
		{
			string name = self.View.E_RoleNameInputField.text;

			if (string.IsNullOrEmpty(name))
			{
				Log.Error("Name is null");
				return;
			}
			
			try
			{
				int errorCode = await LoginHelper.CreateRole(self.ClientScene(),name);
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
		}
		
		
		public static async ETTask OnDeleteRoleClickHandler(this DlgRoles self)
		{
			if (self.ClientScene().GetComponent<RoleInfosComponent>().CurrentRoleId == 0)
			{
				Log.Error("请选择需要删除的角色");
				return;
			}
			
			try
			{
				int errorCode = await LoginHelper.DeleteRole(self.ClientScene());
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
		
		
		public static async ETTask OnConfirmClickHandler(this DlgRoles self)
		{

			if (self.ClientScene().GetComponent<RoleInfosComponent>().CurrentRoleId == 0)
			{
				Log.Error("请选择进入游戏的角色");
				return;
			}
			
			try
			{
				int errorCode = await LoginHelper.GetGatemKey(self.ClientScene());
				if (errorCode != ErrorCode.ERR_Success)
				{
					Log.Error(errorCode.ToString());
					return;
				}

				
				errorCode = await LoginHelper.EnterGame(self.ClientScene());
				if (errorCode != ErrorCode.ERR_Success)
				{
					Log.Error(errorCode.ToString());
					return;
				}
				self.ClientScene().GetComponent<UIComponent>().CloseWindow(WindowID.WindowID_Roles);
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}

		}

		
		
	}
}
