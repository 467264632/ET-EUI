using System.Collections.Generic;


namespace ET.Server
{
	public static class GetSceneHelper
	{
		public static StartSceneConfig GetGate(int zone)
		{
			List<StartSceneConfig> zoneGates = StartSceneConfigCategory.Instance.Gates[zone];
			
			int n = RandomGenerator.RandomNumber(0, zoneGates.Count);

			return zoneGates[n];
		}
		public static StartSceneConfig GetRealm(int zone)
		{
			List<StartSceneConfig> zoneRealm = StartSceneConfigCategory.Instance.Realms[zone];
			// foreach (var VARIABLE in zoneRealm)
			// {
			// 	Log.Info($"config:{MongoHelper.ToJson(VARIABLE)}");
			// }
			
			int n = RandomGenerator.RandomNumber(0, zoneRealm.Count);
			
			return zoneRealm[n];
		}
		public static StartSceneConfig GetLoginCenter()
		{
			return StartSceneConfigCategory.Instance.LoginCenters[0];
		}
		public static StartSceneConfig GetUnitCache(long unitId)
		{
			int zone = UnitIdStruct.GetUnitZone(unitId);
			return StartSceneConfigCategory.Instance.UnitCaches[zone];
		}
		
	}
}
