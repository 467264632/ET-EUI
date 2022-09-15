using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof(DlgLogin))]
    public static  class DlgLoginSystem
    {

        public static void RegisterUIEvent(this DlgLogin self)
        {
            self.View.E_LoginButton.AddListenerAsync(() => { return self.OnLoginClickHandler();});
        }

        public static void ShowWindow(this DlgLogin self, Entity contextData = null)
        {
            self.View.E_AccountInputField.text  = PlayerPrefs.GetString("Account", string.Empty);
            self.View.E_PasswordInputField.text = PlayerPrefs.GetString("Password", string.Empty);
        }

        public static async ETTask OnLoginClickHandler(this DlgLogin self)
        {
            try
            {
                string account = self.View.E_AccountInputField.text;
                string password = self.View.E_PasswordInputField.text;
                int errorCode = await LoginHelper.Login(self.ClientScene(), account, password);
                if (errorCode != ErrorCode.ERR_Success)
                {
                    Log.Error(errorCode.ToString());
                    return;
                }
                errorCode = await LoginHelper.GetServerInfos(self.ClientScene());
                if (errorCode != ErrorCode.ERR_Success)
                {
                    Log.Error(errorCode.ToString());
                    return;
                }
                self.ClientScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_Login);
                self.ClientScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Server);
                PlayerPrefs.SetString("Account",account);
                PlayerPrefs.SetString("Password",password);
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
			
        }
		
    }
}