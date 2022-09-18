using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace ET.Client
{
    [FriendOf(typeof(AccountInfoComponent))]
    [FriendOf(typeof(ServerInfosComponent))]
    [FriendOf(typeof(RoleInfosComponent))]
    public static class LoginHelper
    {
        public static async ETTask<int> Login(Scene clientScene, string account, string password)
        {
            try
            {
                // 创建一个ETModel层的Session
                clientScene.RemoveComponent<RouterAddressComponent>();
                // 获取路由跟realmDispatcher地址
                RouterAddressComponent routerAddressComponent = clientScene.GetComponent<RouterAddressComponent>();
                if (routerAddressComponent == null)
                {
                    routerAddressComponent = clientScene.AddComponent<RouterAddressComponent, string, int>(ConstValue.RouterHttpHost, ConstValue.RouterHttpPort);
                    await routerAddressComponent.Init();
                    clientScene.AddComponent<NetClientComponent, AddressFamily>(routerAddressComponent.RouterManagerIPAddress.AddressFamily);
                }
                IPEndPoint realmAddress = routerAddressComponent.GetRealmAddress(account);
                R2C_GetAccountKey r2CGetAccountKey;
                using (Session realmSession = await RouterHelper.CreateRouterSession(clientScene, realmAddress))
                {
                    r2CGetAccountKey = (R2C_GetAccountKey) await realmSession.Call(new C2R_GetAccountKey() { AccountName = account });
                }
                if (r2CGetAccountKey.Error != ErrorCode.ERR_Success)
                {
                    return r2CGetAccountKey.Error;
                }
                
                password = MD5Helper.StringMD5(password);

                // 创建一个accountSession,并且保存到SessionComponent中
                Session accountSession = await RouterHelper.CreateRouterSession(clientScene, NetworkHelper.ToIPEndPoint(r2CGetAccountKey.Address));
                A2C_LoginAccount a2CLoginAccount = (A2C_LoginAccount) await accountSession.Call(new C2A_LoginAccount() { AccountKey = r2CGetAccountKey.AccountKey, AccountName = account, Password = password });
                
                if (a2CLoginAccount.Error != ErrorCode.ERR_Success)
                {
                    return a2CLoginAccount.Error;
                }
                // 创建一个gate Session,并且保存到SessionComponent中
                clientScene.AddComponent<SessionComponent>().Session = accountSession;
                clientScene.GetComponent<AccountInfoComponent>().Token = a2CLoginAccount.Token;
                clientScene.GetComponent<AccountInfoComponent>().AccountId = a2CLoginAccount.AccountId;
                
                return ErrorCode.ERR_Success;
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }
        }
        public static async ETTask<int> GetServerInfos(Scene clientScene)
        {
            A2C_GetServerInfos a2CGetServerInfos = null;

            try
            {
                a2CGetServerInfos = (A2C_GetServerInfos)await clientScene.GetComponent<SessionComponent>().Session.Call(new C2A_GetServerInfos()
                {
                    AccountId = clientScene.GetComponent<AccountInfoComponent>().AccountId,
                    Token = clientScene.GetComponent<AccountInfoComponent>().Token
                });

            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }

            if (a2CGetServerInfos.Error != ErrorCode.ERR_Success)
            {
                return a2CGetServerInfos.Error;
            }

            foreach (var serverInfoProto in a2CGetServerInfos.ServerInfosList)
            {
                ServerInfo serverInfo = clientScene.GetComponent<ServerInfosComponent>().AddChild<ServerInfo>();
                serverInfo.FromMessage(serverInfoProto);
                clientScene.GetComponent<ServerInfosComponent>().Add(serverInfo);
            }
            
            
            await ETTask.CompletedTask;
            return ErrorCode.ERR_Success;
        }
        public static async ETTask<int> GetRoles(Scene zoneScene)
        {
            A2C_GetRoles a2CGetRoles = null;

            try
            {
                a2CGetRoles = (A2C_GetRoles) await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2A_GetRoles()
                {
                    AccountId = zoneScene.GetComponent<AccountInfoComponent>().AccountId,
                    Token = zoneScene.GetComponent<AccountInfoComponent>().Token,
                    ServerId = zoneScene.GetComponent<ServerInfosComponent>().CurrentServerId,
                });
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }

            if (a2CGetRoles.Error != ErrorCode.ERR_Success)
            {
                return a2CGetRoles.Error;
            }

        
            zoneScene.GetComponent<RoleInfosComponent>().RoleInfos.Clear();
            foreach (var roleInfoProto in a2CGetRoles.RoleInfo)
            {
                RoleInfo roleInfo = zoneScene.GetComponent<RoleInfosComponent>().AddChild<RoleInfo>();
                roleInfo.FromMessage(roleInfoProto);
                zoneScene.GetComponent<RoleInfosComponent>().RoleInfos.Add(roleInfo);
            }

            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> CreateRole(Scene zoneScene, string name)
        {
            A2C_CreateRole a2CCreateRole = null;

            try
            {
                a2CCreateRole = (A2C_CreateRole) await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2A_CreateRole()
                {
                    AccountId = zoneScene.GetComponent<AccountInfoComponent>().AccountId,
                    Token = zoneScene.GetComponent<AccountInfoComponent>().Token,
                    Name = name,
                    ServerId = zoneScene.GetComponent<ServerInfosComponent>().CurrentServerId,
                });
            }
            catch (Exception e)
            {
               Log.Error(e.ToString());
               return ErrorCode.ERR_NetWorkError;
            }

            if (a2CCreateRole.Error != ErrorCode.ERR_Success)
            {
                return a2CCreateRole.Error;
            }

            RoleInfo newRoleInfo = zoneScene.GetComponent<RoleInfosComponent>().AddChild<RoleInfo>();
            newRoleInfo.FromMessage(a2CCreateRole.RoleInfo);
            
            zoneScene.GetComponent<RoleInfosComponent>().RoleInfos.Add(newRoleInfo);

            return ErrorCode.ERR_Success;
        }
        
        public static async ETTask<int> DeleteRole(Scene zoneScene)
        {
            A2C_DeleteRole a2CDeleteRole = null;

            try
            {
                 a2CDeleteRole = (A2C_DeleteRole) await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2A_DeleteRole()
                {
                    Token = zoneScene.GetComponent<AccountInfoComponent>().Token,
                    AccountId = zoneScene.GetComponent<AccountInfoComponent>().AccountId,
                    RoleInfoId = zoneScene.GetComponent<RoleInfosComponent>().CurrentRoleId,
                    ServerId = zoneScene.GetComponent<ServerInfosComponent>().CurrentServerId
                });
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }

            if (a2CDeleteRole.Error != ErrorCode.ERR_Success)
            {
                return a2CDeleteRole.Error;
            }

            int index = zoneScene.GetComponent<RoleInfosComponent>().RoleInfos.FindIndex((info) => { return info.Id == a2CDeleteRole.DeletedRoleInfoId; });
            
            zoneScene.GetComponent<RoleInfosComponent>().RoleInfos.RemoveAt(index);
            
            
            await ETTask.CompletedTask;
            return ErrorCode.ERR_Success;
        }
        
        public static async ETTask<int> GetGatemKey(Scene zoneScene)
        {
            A2C_GetGateKey a2C_GetGateKey = null;

            try
            {
                a2C_GetGateKey =(A2C_GetGateKey) await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2A_GetGateKey()
                {
                    Token = zoneScene.GetComponent<AccountInfoComponent>().Token,
                    AccountId = zoneScene.GetComponent<AccountInfoComponent>().AccountId,
                    ServerId = zoneScene.GetComponent<ServerInfosComponent>().CurrentServerId
                });

            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }

            if (a2C_GetGateKey.Error != ErrorCode.ERR_Success)
            {
                Log.Error(a2C_GetGateKey.Error.ToString());
                return a2C_GetGateKey.Error;
            }

            zoneScene.GetComponent<AccountInfoComponent>().GateKey = a2C_GetGateKey.GateKey;
            zoneScene.GetComponent<AccountInfoComponent>().GateAddress = a2C_GetGateKey.GateAddress;
            zoneScene.GetComponent<SessionComponent>().Session.Dispose();
            await ETTask.CompletedTask;
            return ErrorCode.ERR_Success;
        }
        
       public static async ETTask<int> EnterGame(Scene zoneScene)
        {
            string gateAddress = zoneScene.GetComponent<AccountInfoComponent>().GateAddress;
            string gateKey = zoneScene.GetComponent<AccountInfoComponent>().GateKey;
            
            // // 1. 连接Realm，获取分配的Gate
            // R2C_LoginRealm r2CLoginRealm;
            // using (Session session = await RouterHelper.CreateRouterSession(zoneScene, NetworkHelper.ToIPEndPoint(realmAddress)))
            // {
            //     try
            //     {
            //         r2CLoginRealm = (R2C_LoginRealm) await session.Call(new C2R_LoginRealm() 
            //         {    
            //             AccountId =  zoneScene.GetComponent<AccountInfoComponent>().AccountId,
            //             RealmTokenKey = zoneScene.GetComponent<AccountInfoComponent>().RealmKey 
            //         });
            //     }
            //     catch (Exception e)
            //     {
            //         Log.Error(e);
            //         session?.Dispose();
            //         return ErrorCode.ERR_NetWorkError;
            //     }
            //     session?.Dispose();
            //     
            //     if (r2CLoginRealm.Error != ErrorCode.ERR_Success)
            //     {
            //         return r2CLoginRealm.Error;
            //     }
            // }
    
            // 创建一个gate Session,并且保存到SessionComponent中
            Session gateSession = await RouterHelper.CreateRouterSession(zoneScene, NetworkHelper.ToIPEndPoint(gateAddress));
            zoneScene.GetComponent<SessionComponent>().Session = gateSession;
            
            
            // 2. 开始连接Gate
            long accountId = zoneScene.GetComponent<AccountInfoComponent>().AccountId;
            long currentRoleId = zoneScene.GetComponent<RoleInfosComponent>().CurrentRoleId;
            G2C_LoginGameGate g2CLoginGate = null;
            try
            {
                 g2CLoginGate = (G2C_LoginGameGate)await gateSession.Call(new C2G_LoginGameGate() { Key = gateKey, AccountId = accountId,RoleId = currentRoleId});
                
            }
            catch (Exception e)
            {
                Log.Error(e);
                zoneScene.GetComponent<SessionComponent>().Session.Dispose();
                return ErrorCode.ERR_NetWorkError;
            }
            
            if (g2CLoginGate.Error != ErrorCode.ERR_Success)
            {
                zoneScene.GetComponent<SessionComponent>().Session.Dispose();
                return g2CLoginGate.Error;
            }
            Log.Debug("登陆gate成功!");

            //3. 角色正式请求进入游戏逻辑服
            G2C_EnterGame g2CEnterGame = null;
            try
            {
   
                g2CEnterGame = (G2C_EnterGame)await gateSession.Call(new C2G_EnterGame() { });
            }
            catch (Exception e)
            {
                Log.Error(e);
                zoneScene.GetComponent<SessionComponent>().Session.Dispose();
                return ErrorCode.ERR_NetWorkError;
            }

            if (g2CEnterGame.Error != ErrorCode.ERR_Success)
            {
                Log.Error(g2CEnterGame.Error.ToString());
                return g2CEnterGame.Error;
            }
            
            Log.Debug("角色进入游戏成功!!!!");
            zoneScene.GetComponent<PlayerComponent>().MyId = g2CEnterGame.MyId;
            
            await zoneScene.GetComponent<ObjectWait>().Wait<Wait_SceneChangeFinish>();
            
            return ErrorCode.ERR_Success;
        }
    }
}