﻿using System;

namespace ET
{
    public class C2A_CreateRoleHandler :    AMRpcHandler<C2A_CreateRole,A2C_CreateRole>
    {
        protected override  async ETTask Run(Session session, C2A_CreateRole request, A2C_CreateRole response, Action reply)
        {
            
            if (session.DomainScene().SceneType != SceneType.Account)
            {
                Log.Debug($"请求的Scene错误，当前Scene为:{ session.DomainScene().SceneType}");
                session.Dispose();
                return;
            }

            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                response.Error = ErrorCode.ERR_RequestRepeatError;
                reply();
                session?.Disconnect().Coroutine();
                return;
            }
            string token = session.DomainScene().GetComponent<TokenComponent>().Get(request.AccountId);
            if (token == null || token != request.Token)
            {
                response.Error = ErrorCode.ERR_TokenError;
                reply();
                session?.Disconnect().Coroutine();
            }
            
            if (string.IsNullOrEmpty(request.Name))
            {
                response.Error = ErrorCode.ERR_RoleNameIsNull;
                reply();
                // session?.Disconnect().Coroutine();
                return;
            }

            using (session.AddComponent<SessionLockingComponent>() )
            {
                using (await  CoroutineLockComponent.Instance.Wait(CoroutineLockType.CreateRole,request.AccountId))
                {
                    var roleInfo = await DBManagerComponent.Instance.GetZoneDB(session.DomainZone())
                            .Query<RoleInfo>(d => d.Name == request.Name && d.ServerId == request.ServerId);
                    if (roleInfo != null &&  roleInfo.Count > 0)
                    {
                        response.Error = ErrorCode.ERR_RoleNameSame;
                        reply();
                        return;
                    }

                    RoleInfo newRoleInfo = session.AddChildWithId<RoleInfo>(IdGenerater.Instance.GenerateUnitId(request.ServerId));
                    newRoleInfo.Name = request.Name;
                    newRoleInfo.State = (int)RoleInfoState.Normal;
                    newRoleInfo.ServerId = request.ServerId;
                    newRoleInfo.Account = request.AccountId;
                    newRoleInfo.CreateTime = TimeHelper.ServerNow();
                    newRoleInfo.CreateTime = 0;
                    await DBManagerComponent.Instance.GetZoneDB(session.DomainZone()).Save(newRoleInfo);
                    response.RoleInfo = newRoleInfo.ToMessage();
                    reply();
                    newRoleInfo?.Dispose();
                }
            }
            
        }
    }
}