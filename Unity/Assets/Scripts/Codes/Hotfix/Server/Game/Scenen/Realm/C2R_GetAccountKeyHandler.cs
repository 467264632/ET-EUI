using System;
using System.Text.RegularExpressions;

namespace ET.Server
{
	[MessageHandler(SceneType.Realm)]
	public class C2R_GetAccountKeyHandler : AMRpcHandler<C2R_GetAccountKey, R2C_GetAccountKey>
	{
		protected override async ETTask Run(Session session, C2R_GetAccountKey request, R2C_GetAccountKey response, Action reply)
		{
			if (session.GetComponent<SessionLockingComponent>() != null)
			{
				response.Error = ErrorCode.ERR_RequestRepeatedly;
				reply();
				session.Disconnect().Coroutine();
				return;
			}

			if (string.IsNullOrEmpty(request.AccountName))
			{
				response.Error = ErrorCode.ERR_LoginInfoIsNull;
				reply();
				session.Disconnect().Coroutine();
				return;
			}
			
			if (!Regex.IsMatch(request.AccountName.Trim(),@"^(?=.*[0-9].*)(?=.*[A-Z].*)(?=.*[a-z].*).{6,15}$"))
			{
				response.Error = ErrorCode.ERR_AccountNameFormError;
				reply();
				session.Disconnect().Coroutine();
				return;
			}
			
			using (session.AddComponent<SessionLockingComponent>())
			{
				string accountName = request.AccountName.Trim();
				// 随机分配一个Account
				StartSceneConfig config = GetSceneHelper.GetAccount(accountName);
			
				// 向gate请求一个key,客户端可以拿着这个key连接gate
				A2R_GetAccountKey a2R_LoginAccount = (A2R_GetAccountKey) await ActorMessageSenderComponent.Instance.Call(
					config.InstanceId, new R2A_GetAccountKey() { AccountName = accountName});

				response.Address = config.InnerIPOutPort.ToString();
				response.AccountKey = a2R_LoginAccount.AccountKey;
				reply();
				// session.Disconnect().Coroutine();
			}
		}
	}
}
