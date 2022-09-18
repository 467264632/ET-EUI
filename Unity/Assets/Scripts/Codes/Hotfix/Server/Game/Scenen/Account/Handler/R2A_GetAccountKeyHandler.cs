using System;
using UnityEngine;

namespace ET.Server
{
    [ActorMessageHandler(SceneType.Account)]
    [FriendOf(typeof(SessionPlayerComponent))]
    public class R2A_GetAccountKeyHandler : AMActorRpcHandler<Scene,R2A_GetAccountKey,A2R_GetAccountKey>
    {
        protected override async ETTask Run(Scene scene, R2A_GetAccountKey request, A2R_GetAccountKey response, Action reply)
        {
            long key = RandomGenerator.RandInt64();
            scene.GetComponent<AccountSessionKeyComponent>().Add(key,request.AccountName);
            response.AccountKey = key;
            reply();
            await ETTask.CompletedTask;
        }
    }
}