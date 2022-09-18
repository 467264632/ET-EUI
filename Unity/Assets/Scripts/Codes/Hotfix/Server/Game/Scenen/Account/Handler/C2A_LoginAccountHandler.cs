using System;
using System.Text.RegularExpressions;

namespace ET.Server
{
    [MessageHandler(SceneType.Account)]
    [FriendOf(typeof(Account))]
    public class C2A_LoginAccountHandler:AMRpcHandler<C2A_LoginAccount,A2C_LoginAccount>
    {
        protected override async ETTask Run(Session session, C2A_LoginAccount request, A2C_LoginAccount response, Action reply)
        {
            session.RemoveComponent<SessionAcceptTimeoutComponent>();

            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                response.Error = ErrorCode.ERR_RequestRepeatedly;
                reply();
                session.Disconnect().Coroutine();
                return;
            }
            

            if (string.IsNullOrEmpty(request.AccountName) || string.IsNullOrEmpty(request.Password))
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

            if (!Regex.IsMatch(request.Password.Trim(),@"^[A-Za-z0-9]+$"))
            {
                response.Error = ErrorCode.ERR_PasswordFormError;
                reply();
                session.Disconnect().Coroutine();
                return;
            }

            string accountName = session.DomainScene().GetComponent<AccountSessionKeyComponent>().Get(request.AccountKey);
            if (accountName == null)
            {
                response.Error = ErrorCode.ERR_GetAccountNameError;
                reply();
                session.Disconnect().Coroutine();
                return;
            }

            // string accountName = request.AccountName.Trim();
            if (session.GetComponent<RoleInfosZone>() == null)
            {
                session.AddComponent<RoleInfosZone>();
            }
            
            using (session.AddComponent<SessionLockingComponent>())
            {
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginAccount,accountName.GetHashCode()))
                {
                    var accountInfoList = await DBManagerComponent.Instance.GetZoneDB(session.DomainZone()).Query<Account>(d=>d.AccountName.Equals(accountName));
                    Account account     = null;
                    if (accountInfoList!=null && accountInfoList.Count > 0)
                    {
                        account = accountInfoList[0];
                        // session.GetComponent<AccountsZone>().AddChild(account);
                        if (!account.Password.Equals(request.Password))
                        {
                            response.Error = ErrorCode.ERR_LoginPasswordError;
                            reply();
                            session.Disconnect().Coroutine();
                            account?.Dispose();
                            return;
                        }
                        if (account.AccountType == (int)AccountType.BlackList)
                        {
                            response.Error = ErrorCode.ERR_AccountInBlackListError;
                            reply();
                            session.Disconnect().Coroutine();
                            account?.Dispose();
                            return;
                        }
                    }
                    else
                    {
                        // account             = session.GetComponent<AccountsZone>().AddChild<Account>();
                        account.AccountName = accountName;
                        account.Password    = request.Password;
                        account.CreateTime  = TimeHelper.ServerNow();
                        account.AccountType = (int)AccountType.General;
                        await DBManagerComponent.Instance.GetZoneDB(session.DomainZone()).Save<Account>(account);
                    }

                    StartSceneConfig startSceneConfig = GetSceneHelper.GetLoginCenter();
                    long loginCenterInstanceId = startSceneConfig.InstanceId;
                    var loginAccountResponse  = (L2A_LoginAccountResponse) await ActorMessageSenderComponent.Instance.Call(loginCenterInstanceId,new A2L_LoginAccountRequest(){AccountId = account.Id});

                    if (loginAccountResponse.Error != ErrorCode.ERR_Success)
                    {
                        response.Error = loginAccountResponse.Error;
                        reply();
                        session?.Disconnect().Coroutine();
                        account?.Dispose();
                        return;
                    }
                    
                    //将正在登录的其他账号踢下线
                    long accountSessionInstanceId = session.DomainScene().GetComponent<AccountSessionsComponent>().Get(account.Id);
                    using (Session otherSession   = EventSystem.Instance.Get(accountSessionInstanceId) as Session)
                    {
                        if (otherSession!=null && session.InstanceId != otherSession.InstanceId)
                        {
                            otherSession?.Send(new A2C_Disconnect(){Error = 0});
                            otherSession?.Disconnect().Coroutine();
                        }
                    }
                    session.DomainScene().GetComponent<AccountSessionsComponent>().Add(account.Id,session.InstanceId);

                    session.AddComponent<AccountCheckOutTimeComponent, long>(account.Id);

                    string Token = TimeHelper.ServerNow().ToString() + RandomHelper.RandomNumber(int.MinValue, int.MaxValue).ToString();
                    session.DomainScene().GetComponent<TokenComponent>().Remove(account.Id);
                    session.DomainScene().GetComponent<TokenComponent>().Add(account.Id,Token);

                    response.AccountId = account.Id;
                    response.Token     = Token;

                    reply();
                    account?.Dispose();
                    
                }
            }
        }
    }
}
