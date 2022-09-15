using System.Collections.Generic;


namespace ET.Server
{
	public static class RealmGateAddressHelper
	{
		public static StartSceneConfig GetGate(int zone)
		{
			List<StartSceneConfig> zoneGates = StartSceneConfigCategory.Instance.Gates[zone];
			
			int n = RandomGenerator.Instance.RandomNumber(0, zoneGates.Count);

			return zoneGates[n];
		}
		public static StartSceneConfig GetRealm(int zone)
		{
			List<StartSceneConfig> zoneRealm = StartSceneConfigCategory.Instance.Realms[zone];
			// foreach (var VARIABLE in zoneRealm)
			// {
			// 	Log.Info($"config:{MongoHelper.ToJson(VARIABLE)}");
			// }
			
			int n = RandomGenerator.Instance.RandomNumber(0, zoneRealm.Count);
			
			return zoneRealm[n];
		}
	}
}
