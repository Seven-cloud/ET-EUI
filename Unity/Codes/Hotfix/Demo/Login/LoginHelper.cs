using System;
using CommandLine;
using UnityEngine;

namespace ET
{
    public static class LoginHelper
    {
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