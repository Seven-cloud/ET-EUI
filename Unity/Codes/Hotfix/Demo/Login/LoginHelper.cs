using System;
using CommandLine;
using UnityEngine;

namespace ET
{
    public static class LoginHelper
    {
        
        /// <summary>
        /// 账号登录请求
        /// </summary>
        /// <param name="zoneScene"></param>
        /// <param name="address"></param>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static async ETTask<int> Login(Scene zoneScene, string address, string account, string password)
        { 
            A2C_LoginAccount a2CLoginAccount = null;
            Session accountSession = null;
            try
            {
               
                accountSession = zoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(address));

                password = MD5Helper.StringMD5(password);
                a2CLoginAccount = (A2C_LoginAccount) await accountSession.Call(new C2A_LoginAccount() { Account = account, Password = password });
            }
            catch (Exception e)
            {
                accountSession?.Dispose();
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }

            if (a2CLoginAccount.Error != ErrorCode.ERR_Success)
            {
                accountSession?.Dispose();
                return a2CLoginAccount.Error;
            }

            zoneScene.GetComponent<SessionComponent>().Session = accountSession;
            zoneScene.GetComponent<SessionComponent>().Session.AddComponent<PingComponent>();
            zoneScene.GetComponent<AccountInfoComponent>().Token = a2CLoginAccount.Token;
            zoneScene.GetComponent<AccountInfoComponent>().AccountId = a2CLoginAccount.AccountId;
            return ErrorCode.ERR_Success;
        }
        /// <summary>
        /// 获得服务器区服
        /// </summary>
        /// <param name="zoneScene"></param>
        /// <returns></returns>
        public static async ETTask<int> GetSeverInfo(Scene zoneScene)
        {
            A2C_GetServerInfo a2CGetServerInfo = null;
            try
            {
                a2CGetServerInfo = (A2C_GetServerInfo) await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2A_GetServerInfo()
                {
                    Token = zoneScene.GetComponent<AccountInfoComponent>().Token,
                    Account = zoneScene.GetComponent<AccountInfoComponent>().AccountId
                });
            }
            catch (Exception e)
            {
                Log.Debug(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }

            if (a2CGetServerInfo?.Error != ErrorCode.ERR_Success)
            {
                return a2CGetServerInfo.Error;
            }

            foreach (var infoProto in a2CGetServerInfo.ServerInfoList)
            {
                ServerInfo serverInfo = zoneScene.GetComponent<ServerInfoComponent>().AddChild<ServerInfo>();
                serverInfo.FromMessage(infoProto);
                zoneScene.GetComponent<ServerInfoComponent>().Add(serverInfo);
            }
            await ETTask.CompletedTask;

            return ErrorCode.ERR_Success;
        }

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="zoneScene"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static async ETTask<int> CreateRole(Scene zoneScene,string name)
        {
            A2C_CreateRole a2CCreateRole = null;
            try
            {
                a2CCreateRole = (A2C_CreateRole) await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2A_CreateRole()
                {
                    AccountId = zoneScene.GetComponent<AccountInfoComponent>().AccountId,
                    Token = zoneScene.GetComponent<AccountInfoComponent>().Token,
                    ServerId = zoneScene.GetComponent<ServerInfoComponent>().CurrentServerId,
                    Name = name
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

            RoleInfo newRoleInfo = zoneScene.GetComponent<RoleInfoComponent>().AddChild<RoleInfo>();
            newRoleInfo.FromMessage(a2CCreateRole.RoleInfo);
            
            zoneScene.GetComponent<RoleInfoComponent>().RoleInfos.Add(newRoleInfo);
            return ErrorCode.ERR_Success;
        }
        /// <summary>
        ///  获取玩家角色列表
        /// </summary>
        /// <param name="zoneScene"></param>
        /// <returns></returns>
        public static async ETTask<int> GetRole(Scene zoneScene)
        {
            A2C_GetRole a2CGetRole = null;
            try
            {
                a2CGetRole = (A2C_GetRole) await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2A_GetRole()
                {
                    Token = zoneScene.GetComponent<AccountInfoComponent>().Token,
                    AccountId = zoneScene.GetComponent<AccountInfoComponent>().AccountId,
                    ServerId =  zoneScene.GetComponent<ServerInfoComponent>().CurrentServerId,
                });
              
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }
            if (a2CGetRole.Error != ErrorCode.ERR_Success)
            {
                Log.Error(a2CGetRole.Error.ToString());
                return a2CGetRole.Error;
            }

            zoneScene.GetComponent<RoleInfoComponent>().RoleInfos.Clear();
            foreach (var info in a2CGetRole.RoleInfo)
            {
                RoleInfo  roleInfo = zoneScene.GetComponent<RoleInfoComponent>().AddChild<RoleInfo>();
                 roleInfo.FromMessage(info);
                 zoneScene.GetComponent<RoleInfoComponent>().RoleInfos.Add(roleInfo);
            }
            
            
            return ErrorCode.ERR_Success;
        }
        /// <summary>
        /// 删除选定的角色
        /// </summary>
        /// <param name="zoneScene"></param>
        /// <returns></returns>
        public static async ETTask<int> DeleteRole(Scene zoneScene)
        {
            A2C_DeleteRole a2CDeleteRole = null;
            try
            {
                a2CDeleteRole = (A2C_DeleteRole) await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2A_DeleteRole()
                {
                    Token = zoneScene.GetComponent<AccountInfoComponent>().Token,
                    AccountId = zoneScene.GetComponent<AccountInfoComponent>().AccountId,
                    RoleInfoId =  zoneScene.GetComponent<RoleInfoComponent>().CurrentRoleId,
                    ServerId =  zoneScene.GetComponent<ServerInfoComponent>().CurrentServerId
                });
              
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }
            if (a2CDeleteRole.Error != ErrorCode.ERR_Success)
            {
                Log.Error(a2CDeleteRole.Error.ToString());
                return a2CDeleteRole.Error;
            }

            int index =     zoneScene.GetComponent<RoleInfoComponent>().RoleInfos.FindIndex((info) => { return info.Id == a2CDeleteRole.DeleteRoleInfoId;});
            zoneScene.GetComponent<RoleInfoComponent>().RoleInfos.RemoveAt(index);
            
            
            return ErrorCode.ERR_Success;
        }
        /// <summary>
        ///  链接均衡服务器
        /// </summary>
        /// <param name="zoneScene"></param>
        /// <returns></returns>
        public static async ETTask<int> GetRealmmKey(Scene zoneScene)
        {
            A2C_GetRealmKey a2CGetRealmKey = null;
            try
            {
                a2CGetRealmKey = (A2C_GetRealmKey) await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2A_GetRealmKey()
                {
                    Token = zoneScene.GetComponent<AccountInfoComponent>().Token,
                    AccountId = zoneScene.GetComponent<AccountInfoComponent>().AccountId,
                    ServerId =  zoneScene.GetComponent<ServerInfoComponent>().CurrentServerId
                });
              
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }
            if (a2CGetRealmKey.Error != ErrorCode.ERR_Success)
            {
                Log.Error(a2CGetRealmKey.Error.ToString());
                return a2CGetRealmKey.Error;
            }
            zoneScene.GetComponent<AccountInfoComponent>().RealmKey = a2CGetRealmKey.RealmKey;
            zoneScene.GetComponent<AccountInfoComponent>().RealmAddress = a2CGetRealmKey.RealmAddress;
            zoneScene.GetComponent<SessionComponent>().Session.Dispose();
            return ErrorCode.ERR_Success;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="zoneScene"></param>
        /// <returns></returns>
        public static async ETTask<int> EnterGame(Scene zoneScene)
        {
            string realmAddress = zoneScene.GetComponent<AccountInfoComponent>().RealmAddress;
            R2C_LoginRealm r2CLoginRealm = null;

            Session session = zoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(realmAddress));
            try
            {
                r2CLoginRealm = (R2C_LoginRealm) await session.Call(new C2R_LoginRealm()
                {
                    AccountId = zoneScene.GetComponent<AccountInfoComponent>().AccountId,
                    RealmTokenKey = zoneScene.GetComponent<AccountInfoComponent>().RealmKey
                });
            }
            catch (Exception e)
            {
              Log.Error(e.ToString());
              session?.Dispose();
              return ErrorCode.ERR_NetWorkError;
            }
            session?.Dispose();
            if (r2CLoginRealm.Error != ErrorCode.ERR_Success)
            {
                return r2CLoginRealm.Error;
            }
            Log.Warning($"GateAddress:{r2CLoginRealm.GateAddress}");
            
            Session gateSession = zoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(r2CLoginRealm.GateAddress));
            gateSession.AddComponent<PingComponent>();
            zoneScene.GetComponent<SessionComponent>().Session = gateSession;
            // 开始连接Gate
            long currentRoleId = zoneScene.GetComponent<RoleInfoComponent>().CurrentRoleId;
            G2C_LoginGameGate g2CLoginGameGate = null;
            try
            {
                g2CLoginGameGate = (G2C_LoginGameGate) await gateSession.Call(new C2G_LoginGameGate()
                {
                    Account = gateSession.GetComponent<AccountInfoComponent>().AccountId,
                    Key = r2CLoginRealm.GateSessionKey,
                    RoleId = currentRoleId
                });
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                zoneScene.GetComponent<SessionComponent>().Session .Dispose();
                return ErrorCode.ERR_NetWorkError;
            }

            if (g2CLoginGameGate.Error != ErrorCode.ERR_Success)
            {
                zoneScene.GetComponent<SessionComponent>().Session .Dispose();
                return g2CLoginGameGate.Error;
            }
            Log.Debug("登录gate成功！");
            return ErrorCode.ERR_Success;
        }
        public static async ETTask LoginGate(Scene zoneScene, string address, string account, string password)
        {
            try
            {
                // 创建一个ETModel层的Session
                R2C_Login r2CLogin;
                Session session = null;
                try
                {
                    session = zoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(address));
                    {
                        r2CLogin = (R2C_Login) await session.Call(new C2R_Login() { Account = account, Password = password });
                    }
                }
                finally
                {
                    session?.Dispose();
                }

                // 创建一个gate Session,并且保存到SessionComponent中
                Session gateSession = zoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(r2CLogin.Address));
                gateSession.AddComponent<PingComponent>();
                zoneScene.AddComponent<SessionComponent>().Session = gateSession;
				
                G2C_LoginGate g2CLoginGate = (G2C_LoginGate)await gateSession.Call(
                    new C2G_LoginGate() { Key = r2CLogin.Key, GateId = r2CLogin.GateId});

                Log.Debug("登陆gate成功!");

                await Game.EventSystem.PublishAsync(new EventType.LoginFinish() {ZoneScene = zoneScene});
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        } 
    }
}