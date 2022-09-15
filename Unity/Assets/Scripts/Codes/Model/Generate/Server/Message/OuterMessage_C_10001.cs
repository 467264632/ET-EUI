using ET;
using ProtoBuf;
using System.Collections.Generic;
namespace ET
{
	[Message(OuterMessage.HttpGetRouterResponse)]
	[ProtoContract]
	public partial class HttpGetRouterResponse: Object
	{
		[ProtoMember(1)]
		public List<string> Realms = new List<string>();

		[ProtoMember(2)]
		public List<string> Routers = new List<string>();

	}

	[Message(OuterMessage.RouterSync)]
	[ProtoContract]
	public partial class RouterSync: Object
	{
		[ProtoMember(1)]
		public uint ConnectId { get; set; }

		[ProtoMember(2)]
		public string Address { get; set; }

	}

	[ResponseType(nameof(M2C_TestResponse))]
	[Message(OuterMessage.C2M_TestRequest)]
	[ProtoContract]
	public partial class C2M_TestRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public string request { get; set; }

	}

	[Message(OuterMessage.M2C_TestResponse)]
	[ProtoContract]
	public partial class M2C_TestResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public string response { get; set; }

	}

	[ResponseType(nameof(Actor_TransferResponse))]
	[Message(OuterMessage.Actor_TransferRequest)]
	[ProtoContract]
	public partial class Actor_TransferRequest: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public int MapIndex { get; set; }

	}

	[Message(OuterMessage.Actor_TransferResponse)]
	[ProtoContract]
	public partial class Actor_TransferResponse: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(G2C_EnterMap))]
	[Message(OuterMessage.C2G_EnterMap)]
	[ProtoContract]
	public partial class C2G_EnterMap: Object, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.G2C_EnterMap)]
	[ProtoContract]
	public partial class G2C_EnterMap: Object, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

// 自己unitId
		[ProtoMember(4)]
		public long MyId { get; set; }

	}

	[Message(OuterMessage.MoveInfo)]
	[ProtoContract]
	public partial class MoveInfo: Object
	{
		[ProtoMember(1)]
		public List<float> X = new List<float>();

		[ProtoMember(2)]
		public List<float> Y = new List<float>();

		[ProtoMember(3)]
		public List<float> Z = new List<float>();

		[ProtoMember(4)]
		public float A { get; set; }

		[ProtoMember(5)]
		public float B { get; set; }

		[ProtoMember(6)]
		public float C { get; set; }

		[ProtoMember(7)]
		public float W { get; set; }

		[ProtoMember(8)]
		public int TurnSpeed { get; set; }

	}

	[Message(OuterMessage.UnitInfo)]
	[ProtoContract]
	public partial class UnitInfo: Object
	{
		[ProtoMember(1)]
		public long UnitId { get; set; }

		[ProtoMember(2)]
		public int ConfigId { get; set; }

		[ProtoMember(3)]
		public int Type { get; set; }

		[ProtoMember(4)]
		public float X { get; set; }

		[ProtoMember(5)]
		public float Y { get; set; }

		[ProtoMember(6)]
		public float Z { get; set; }

		[ProtoMember(7)]
		public float ForwardX { get; set; }

		[ProtoMember(8)]
		public float ForwardY { get; set; }

		[ProtoMember(9)]
		public float ForwardZ { get; set; }

		[ProtoMember(10)]
		public List<int> Ks = new List<int>();

		[ProtoMember(11)]
		public List<long> Vs = new List<long>();

		[ProtoMember(12)]
		public MoveInfo MoveInfo { get; set; }

	}

	[Message(OuterMessage.M2C_CreateUnits)]
	[ProtoContract]
	public partial class M2C_CreateUnits: Object, IActorMessage
	{
		[ProtoMember(2)]
		public List<UnitInfo> Units = new List<UnitInfo>();

	}

	[Message(OuterMessage.M2C_CreateMyUnit)]
	[ProtoContract]
	public partial class M2C_CreateMyUnit: Object, IActorMessage
	{
		[ProtoMember(1)]
		public UnitInfo Unit { get; set; }

	}

	[Message(OuterMessage.M2C_StartSceneChange)]
	[ProtoContract]
	public partial class M2C_StartSceneChange: Object, IActorMessage
	{
		[ProtoMember(1)]
		public long SceneInstanceId { get; set; }

		[ProtoMember(2)]
		public string SceneName { get; set; }

	}

	[Message(OuterMessage.M2C_RemoveUnits)]
	[ProtoContract]
	public partial class M2C_RemoveUnits: Object, IActorMessage
	{
		[ProtoMember(2)]
		public List<long> Units = new List<long>();

	}

	[Message(OuterMessage.C2M_PathfindingResult)]
	[ProtoContract]
	public partial class C2M_PathfindingResult: Object, IActorLocationMessage
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public float X { get; set; }

		[ProtoMember(2)]
		public float Y { get; set; }

		[ProtoMember(3)]
		public float Z { get; set; }

	}

	[Message(OuterMessage.C2M_Stop)]
	[ProtoContract]
	public partial class C2M_Stop: Object, IActorLocationMessage
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.M2C_PathfindingResult)]
	[ProtoContract]
	public partial class M2C_PathfindingResult: Object, IActorMessage
	{
		[ProtoMember(1)]
		public long Id { get; set; }

		[ProtoMember(2)]
		public float X { get; set; }

		[ProtoMember(3)]
		public float Y { get; set; }

		[ProtoMember(4)]
		public float Z { get; set; }

		[ProtoMember(5)]
		public List<float> Xs = new List<float>();

		[ProtoMember(6)]
		public List<float> Ys = new List<float>();

		[ProtoMember(7)]
		public List<float> Zs = new List<float>();

	}

	[Message(OuterMessage.M2C_Stop)]
	[ProtoContract]
	public partial class M2C_Stop: Object, IActorMessage
	{
		[ProtoMember(1)]
		public int Error { get; set; }

		[ProtoMember(2)]
		public long Id { get; set; }

		[ProtoMember(3)]
		public float X { get; set; }

		[ProtoMember(4)]
		public float Y { get; set; }

		[ProtoMember(5)]
		public float Z { get; set; }

		[ProtoMember(6)]
		public float A { get; set; }

		[ProtoMember(7)]
		public float B { get; set; }

		[ProtoMember(8)]
		public float C { get; set; }

		[ProtoMember(9)]
		public float W { get; set; }

	}

	[ResponseType(nameof(G2C_Ping))]
	[Message(OuterMessage.C2G_Ping)]
	[ProtoContract]
	public partial class C2G_Ping: Object, IRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.G2C_Ping)]
	[ProtoContract]
	public partial class G2C_Ping: Object, IResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public long Time { get; set; }

	}

	[Message(OuterMessage.G2C_Test)]
	[ProtoContract]
	public partial class G2C_Test: Object, IMessage
	{
	}

	[ResponseType(nameof(M2C_Reload))]
	[Message(OuterMessage.C2M_Reload)]
	[ProtoContract]
	public partial class C2M_Reload: Object, IRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public string Account { get; set; }

		[ProtoMember(2)]
		public string Password { get; set; }

	}

	[Message(OuterMessage.M2C_Reload)]
	[ProtoContract]
	public partial class M2C_Reload: Object, IResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(R2C_Login))]
	[Message(OuterMessage.C2R_Login)]
	[ProtoContract]
	public partial class C2R_Login: Object, IRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public string Account { get; set; }

		[ProtoMember(2)]
		public string Password { get; set; }

	}

	[Message(OuterMessage.R2C_Login)]
	[ProtoContract]
	public partial class R2C_Login: Object, IResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public string Address { get; set; }

		[ProtoMember(2)]
		public long Key { get; set; }

		[ProtoMember(3)]
		public long GateId { get; set; }

	}

	[ResponseType(nameof(G2C_LoginGate))]
	[Message(OuterMessage.C2G_LoginGate)]
	[ProtoContract]
	public partial class C2G_LoginGate: Object, IRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long Key { get; set; }

		[ProtoMember(2)]
		public long GateId { get; set; }

	}

	[Message(OuterMessage.G2C_LoginGate)]
	[ProtoContract]
	public partial class G2C_LoginGate: Object, IResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public long PlayerId { get; set; }

	}

	[Message(OuterMessage.G2C_TestHotfixMessage)]
	[ProtoContract]
	public partial class G2C_TestHotfixMessage: Object, IMessage
	{
		[ProtoMember(1)]
		public string Info { get; set; }

	}

	[ResponseType(nameof(M2C_TestRobotCase))]
	[Message(OuterMessage.C2M_TestRobotCase)]
	[ProtoContract]
	public partial class C2M_TestRobotCase: Object, IActorLocationRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public int N { get; set; }

	}

	[Message(OuterMessage.M2C_TestRobotCase)]
	[ProtoContract]
	public partial class M2C_TestRobotCase: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public int N { get; set; }

	}

	[ResponseType(nameof(M2C_TransferMap))]
	[Message(OuterMessage.C2M_TransferMap)]
	[ProtoContract]
	public partial class C2M_TransferMap: Object, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.M2C_TransferMap)]
	[ProtoContract]
	public partial class M2C_TransferMap: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(G2C_Benchmark))]
	[Message(OuterMessage.C2G_Benchmark)]
	[ProtoContract]
	public partial class C2G_Benchmark: Object, IRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.G2C_Benchmark)]
	[ProtoContract]
	public partial class G2C_Benchmark: Object, IResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(A2C_LoginAccount))]
	[Message(OuterMessage.C2A_LoginAccount)]
	[ProtoContract]
	public partial class C2A_LoginAccount: Object, IRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public string AccountName { get; set; }

		[ProtoMember(2)]
		public string Password { get; set; }

	}

	[Message(OuterMessage.A2C_LoginAccount)]
	[ProtoContract]
	public partial class A2C_LoginAccount: Object, IResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public string Token { get; set; }

		[ProtoMember(2)]
		public long AccountId { get; set; }

	}

	[Message(OuterMessage.A2C_Disconnect)]
	[ProtoContract]
	public partial class A2C_Disconnect: Object, IMessage
	{
		[ProtoMember(1)]
		public int Error { get; set; }

	}

	[Message(OuterMessage.ServerInfoProto)]
	[ProtoContract]
	public partial class ServerInfoProto: Object
	{
		[ProtoMember(1)]
		public int Id { get; set; }

		[ProtoMember(2)]
		public int Status { get; set; }

		[ProtoMember(3)]
		public string ServerName { get; set; }

	}

	[ResponseType(nameof(A2C_GetServerInfos))]
	[Message(OuterMessage.C2A_GetServerInfos)]
	[ProtoContract]
	public partial class C2A_GetServerInfos: Object, IRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public string Token { get; set; }

		[ProtoMember(2)]
		public long AccountId { get; set; }

	}

	[Message(OuterMessage.A2C_GetServerInfos)]
	[ProtoContract]
	public partial class A2C_GetServerInfos: Object, IResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public List<ServerInfoProto> ServerInfosList = new List<ServerInfoProto>();

	}

	[Message(OuterMessage.RoleInfoProto)]
	[ProtoContract]
	public partial class RoleInfoProto: Object
	{
		[ProtoMember(1)]
		public long Id { get; set; }

		[ProtoMember(2)]
		public string Name { get; set; }

		[ProtoMember(3)]
		public int State { get; set; }

		[ProtoMember(4)]
		public long AccountId { get; set; }

		[ProtoMember(5)]
		public long LastLoginTime { get; set; }

		[ProtoMember(6)]
		public long CreateTime { get; set; }

		[ProtoMember(7)]
		public int ServerId { get; set; }

	}

	[ResponseType(nameof(A2C_CreateRole))]
	[Message(OuterMessage.C2A_CreateRole)]
	[ProtoContract]
	public partial class C2A_CreateRole: Object, IRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public string Token { get; set; }

		[ProtoMember(2)]
		public long AccountId { get; set; }

		[ProtoMember(3)]
		public string Name { get; set; }

		[ProtoMember(4)]
		public int ServerId { get; set; }

	}

	[Message(OuterMessage.A2C_CreateRole)]
	[ProtoContract]
	public partial class A2C_CreateRole: Object, IResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public RoleInfoProto RoleInfo { get; set; }

	}

	[ResponseType(nameof(A2C_GetRoles))]
	[Message(OuterMessage.C2A_GetRoles)]
	[ProtoContract]
	public partial class C2A_GetRoles: Object, IRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public string Token { get; set; }

		[ProtoMember(2)]
		public long AccountId { get; set; }

		[ProtoMember(3)]
		public int ServerId { get; set; }

	}

	[Message(OuterMessage.A2C_GetRoles)]
	[ProtoContract]
	public partial class A2C_GetRoles: Object, IResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public List<RoleInfoProto> RoleInfo = new List<RoleInfoProto>();

	}

	[ResponseType(nameof(A2C_DeleteRole))]
	[Message(OuterMessage.C2A_DeleteRole)]
	[ProtoContract]
	public partial class C2A_DeleteRole: Object, IRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public string Token { get; set; }

		[ProtoMember(2)]
		public long AccountId { get; set; }

		[ProtoMember(3)]
		public long RoleInfoId { get; set; }

		[ProtoMember(4)]
		public int ServerId { get; set; }

	}

	[Message(OuterMessage.A2C_DeleteRole)]
	[ProtoContract]
	public partial class A2C_DeleteRole: Object, IResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public long DeletedRoleInfoId { get; set; }

	}

	[ResponseType(nameof(A2C_GetRealmKey))]
	[Message(OuterMessage.C2A_GetRealmKey)]
	[ProtoContract]
	public partial class C2A_GetRealmKey: Object, IRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public string Token { get; set; }

		[ProtoMember(2)]
		public int ServerId { get; set; }

		[ProtoMember(3)]
		public long AccountId { get; set; }

	}

	[Message(OuterMessage.A2C_GetRealmKey)]
	[ProtoContract]
	public partial class A2C_GetRealmKey: Object, IResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public string RealmKey { get; set; }

		[ProtoMember(2)]
		public string RealmAddress { get; set; }

	}

	[ResponseType(nameof(R2C_LoginRealm))]
	[Message(OuterMessage.C2R_LoginRealm)]
	[ProtoContract]
	public partial class C2R_LoginRealm: Object, IRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public long AccountId { get; set; }

		[ProtoMember(2)]
		public string RealmTokenKey { get; set; }

	}

	[Message(OuterMessage.R2C_LoginRealm)]
	[ProtoContract]
	public partial class R2C_LoginRealm: Object, IResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public string GateSessionKey { get; set; }

		[ProtoMember(2)]
		public string GateAddress { get; set; }

	}

	[ResponseType(nameof(G2C_LoginGameGate))]
	[Message(OuterMessage.C2G_LoginGameGate)]
	[ProtoContract]
	public partial class C2G_LoginGameGate: Object, IRequest
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public string Key { get; set; }

		[ProtoMember(2)]
		public long RoleId { get; set; }

		[ProtoMember(3)]
		public long Account { get; set; }

	}

	[Message(OuterMessage.G2C_LoginGameGate)]
	[ProtoContract]
	public partial class G2C_LoginGameGate: Object, IResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public long PlayerId { get; set; }

	}

	[ResponseType(nameof(G2C_EnterGame))]
	[Message(OuterMessage.C2G_EnterGame)]
	[ProtoContract]
	public partial class C2G_EnterGame: Object, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.G2C_EnterGame)]
	[ProtoContract]
	public partial class G2C_EnterGame: Object, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

// 自己unitId
		[ProtoMember(4)]
		public long MyId { get; set; }

	}

	[Message(OuterMessage.M2C_NoticeUnitNumeric)]
	[ProtoContract]
	public partial class M2C_NoticeUnitNumeric: Object, IActorMessage
	{
		[ProtoMember(1)]
		public long UnitId { get; set; }

		[ProtoMember(2)]
		public int NumericType { get; set; }

		[ProtoMember(4)]
		public long NewValue { get; set; }

	}

	[ResponseType(nameof(M2C_TestUnitNumeric))]
	[Message(OuterMessage.C2M_TestUnitNumeric)]
	[ProtoContract]
	public partial class C2M_TestUnitNumeric: Object, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.M2C_TestUnitNumeric)]
	[ProtoContract]
	public partial class M2C_TestUnitNumeric: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2C_AddAttributePoint))]
	[Message(OuterMessage.C2M_AddAttributePoint)]
	[ProtoContract]
	public partial class C2M_AddAttributePoint: Object, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int NumericType { get; set; }

	}

	[Message(OuterMessage.M2C_AddAttributePoint)]
	[ProtoContract]
	public partial class M2C_AddAttributePoint: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2C_StartGameLevel))]
	[Message(OuterMessage.C2M_StartGameLevel)]
	[ProtoContract]
	public partial class C2M_StartGameLevel: Object, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int LevelId { get; set; }

	}

	[Message(OuterMessage.M2C_StartGameLevel)]
	[ProtoContract]
	public partial class M2C_StartGameLevel: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2C_EndGameLevel))]
	[Message(OuterMessage.C2M_EndGameLevel)]
	[ProtoContract]
	public partial class C2M_EndGameLevel: Object, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Round { get; set; }

		[ProtoMember(3)]
		public int BattleResult { get; set; }

	}

	[Message(OuterMessage.M2C_EndGameLevel)]
	[ProtoContract]
	public partial class M2C_EndGameLevel: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2C_UpRoleLevel))]
	[Message(OuterMessage.C2M_UpRoleLevel)]
	[ProtoContract]
	public partial class C2M_UpRoleLevel: Object, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.M2C_UpRoleLevel)]
	[ProtoContract]
	public partial class M2C_UpRoleLevel: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

	}

	[Message(OuterMessage.ItemInfo)]
	[ProtoContract]
	public partial class ItemInfo: Object
	{
		[ProtoMember(1)]
		public long ItemUid { get; set; }

		[ProtoMember(2)]
		public int ItemConfigId { get; set; }

		[ProtoMember(3)]
		public int ItemQuality { get; set; }

		[ProtoMember(4)]
		public EquipInfoProto EquipInfo { get; set; }

	}

	[Message(OuterMessage.M2C_AllItemsList)]
	[ProtoContract]
	public partial class M2C_AllItemsList: Object, IActorMessage
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public List<ItemInfo> ItemInfoList = new List<ItemInfo>();

		[ProtoMember(2)]
		public int ContainerType { get; set; }

	}

	[Message(OuterMessage.M2C_ItemUpdateOpInfo)]
	[ProtoContract]
	public partial class M2C_ItemUpdateOpInfo: Object, IActorMessage
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(1)]
		public ItemInfo ItemInfo { get; set; }

		[ProtoMember(2)]
		public int Op { get; set; }

		[ProtoMember(3)]
		public int ContainerType { get; set; }

	}

	[Message(OuterMessage.AttributeEntryProto)]
	[ProtoContract]
	public partial class AttributeEntryProto: Object
	{
		[ProtoMember(1)]
		public long Id { get; set; }

		[ProtoMember(2)]
		public int Key { get; set; }

		[ProtoMember(3)]
		public long Value { get; set; }

		[ProtoMember(4)]
		public int EntryType { get; set; }

	}

	[Message(OuterMessage.EquipInfoProto)]
	[ProtoContract]
	public partial class EquipInfoProto: Object
	{
		[ProtoMember(1)]
		public long Id { get; set; }

		[ProtoMember(2)]
		public int Score { get; set; }

		[ProtoMember(3)]
		public List<AttributeEntryProto> AttributeEntryProtoList = new List<AttributeEntryProto>();

	}

	[ResponseType(nameof(M2C_EquipItem))]
	[Message(OuterMessage.C2M_EquipItem)]
	[ProtoContract]
	public partial class C2M_EquipItem: Object, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long ItemUid { get; set; }

	}

	[Message(OuterMessage.M2C_EquipItem)]
	[ProtoContract]
	public partial class M2C_EquipItem: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2C_UnloadEquipItem))]
	[Message(OuterMessage.C2M_UnloadEquipItem)]
	[ProtoContract]
	public partial class C2M_UnloadEquipItem: Object, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int EquipPosition { get; set; }

	}

	[Message(OuterMessage.M2C_UnloadEquipItem)]
	[ProtoContract]
	public partial class M2C_UnloadEquipItem: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(M2C_SellItem))]
	[Message(OuterMessage.C2M_SellItem)]
	[ProtoContract]
	public partial class C2M_SellItem: Object, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long ItemUid { get; set; }

	}

	[Message(OuterMessage.M2C_SellItem)]
	[ProtoContract]
	public partial class M2C_SellItem: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

	}

	[Message(OuterMessage.ProductionProto)]
	[ProtoContract]
	public partial class ProductionProto: Object
	{
		[ProtoMember(1)]
		public long Id { get; set; }

		[ProtoMember(2)]
		public int ConfigId { get; set; }

		[ProtoMember(3)]
		public long TargetTime { get; set; }

		[ProtoMember(4)]
		public long StartTime { get; set; }

		[ProtoMember(5)]
		public int ProductionState { get; set; }

	}

	[ResponseType(nameof(M2C_StartProduction))]
	[Message(OuterMessage.C2M_StartProduction)]
	[ProtoContract]
	public partial class C2M_StartProduction: Object, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int ConfigId { get; set; }

	}

	[Message(OuterMessage.M2C_StartProduction)]
	[ProtoContract]
	public partial class M2C_StartProduction: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(2)]
		public ProductionProto ProductionProto { get; set; }

	}

	[ResponseType(nameof(M2C_ReceiveProduction))]
	[Message(OuterMessage.C2M_ReceiveProduction)]
	[ProtoContract]
	public partial class C2M_ReceiveProduction: Object, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long ProducitonId { get; set; }

	}

	[Message(OuterMessage.M2C_ReceiveProduction)]
	[ProtoContract]
	public partial class M2C_ReceiveProduction: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public ProductionProto ProductionProto { get; set; }

	}

	[Message(OuterMessage.M2C_AllProductionList)]
	[ProtoContract]
	public partial class M2C_AllProductionList: Object, IActorMessage
	{
		[ProtoMember(1)]
		public List<ProductionProto> ProductionProtoList = new List<ProductionProto>();

	}

	[Message(OuterMessage.TaskInfoProto)]
	[ProtoContract]
	public partial class TaskInfoProto: Object
	{
		[ProtoMember(1)]
		public int ConfigId { get; set; }

		[ProtoMember(2)]
		public int TaskState { get; set; }

		[ProtoMember(3)]
		public int TaskPogress { get; set; }

	}

	[Message(OuterMessage.M2C_UpdateTaskInfo)]
	[ProtoContract]
	public partial class M2C_UpdateTaskInfo: Object, IActorMessage
	{
		[ProtoMember(1)]
		public TaskInfoProto TaskInfoProto { get; set; }

	}

	[Message(OuterMessage.M2C_AllTaskInfoList)]
	[ProtoContract]
	public partial class M2C_AllTaskInfoList: Object, IActorMessage
	{
		[ProtoMember(1)]
		public List<TaskInfoProto> TaskInfoProtoList = new List<TaskInfoProto>();

	}

	[ResponseType(nameof(M2C_ReceiveTaskReward))]
	[Message(OuterMessage.C2M_ReceiveTaskReward)]
	[ProtoContract]
	public partial class C2M_ReceiveTaskReward: Object, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int TaskConfigId { get; set; }

	}

	[Message(OuterMessage.M2C_ReceiveTaskReward)]
	[ProtoContract]
	public partial class M2C_ReceiveTaskReward: Object, IActorLocationResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

	}

	[Message(OuterMessage.RankInfoProto)]
	[ProtoContract]
	public partial class RankInfoProto: Object
	{
		[ProtoMember(1)]
		public long Id { get; set; }

		[ProtoMember(2)]
		public long UnitId { get; set; }

		[ProtoMember(4)]
		public string Name { get; set; }

		[ProtoMember(5)]
		public int Count { get; set; }

	}

	[ResponseType(nameof(Rank2C_GetRanksInfo))]
	[Message(OuterMessage.C2Rank_GetRanksInfo)]
	[ProtoContract]
	public partial class C2Rank_GetRanksInfo: Object, IActorRankInfoRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.Rank2C_GetRanksInfo)]
	[ProtoContract]
	public partial class Rank2C_GetRanksInfo: Object, IActorRankInfoResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public List<RankInfoProto> RankInfoProtoList = new List<RankInfoProto>();

	}

	[ResponseType(nameof(Chat2C_SendChatInfo))]
	[Message(OuterMessage.C2Chat_SendChatInfo)]
	[ProtoContract]
	public partial class C2Chat_SendChatInfo: Object, IActorChatInfoRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public string ChatMessage { get; set; }

	}

	[Message(OuterMessage.Chat2C_SendChatInfo)]
	[ProtoContract]
	public partial class Chat2C_SendChatInfo: Object, IActorChatInfoResponse
	{
		[ProtoMember(90)]
		public int RpcId { get; set; }

		[ProtoMember(91)]
		public int Error { get; set; }

		[ProtoMember(92)]
		public string Message { get; set; }

	}

	[Message(OuterMessage.Chat2C_NoticeChatInfo)]
	[ProtoContract]
	public partial class Chat2C_NoticeChatInfo: Object, IActorMessage
	{
		[ProtoMember(1)]
		public string Name { get; set; }

		[ProtoMember(2)]
		public string ChatMessage { get; set; }

	}

	public static class OuterMessage
	{
		 public const ushort HttpGetRouterResponse = 10002;
		 public const ushort RouterSync = 10003;
		 public const ushort C2M_TestRequest = 10004;
		 public const ushort M2C_TestResponse = 10005;
		 public const ushort Actor_TransferRequest = 10006;
		 public const ushort Actor_TransferResponse = 10007;
		 public const ushort C2G_EnterMap = 10008;
		 public const ushort G2C_EnterMap = 10009;
		 public const ushort MoveInfo = 10010;
		 public const ushort UnitInfo = 10011;
		 public const ushort M2C_CreateUnits = 10012;
		 public const ushort M2C_CreateMyUnit = 10013;
		 public const ushort M2C_StartSceneChange = 10014;
		 public const ushort M2C_RemoveUnits = 10015;
		 public const ushort C2M_PathfindingResult = 10016;
		 public const ushort C2M_Stop = 10017;
		 public const ushort M2C_PathfindingResult = 10018;
		 public const ushort M2C_Stop = 10019;
		 public const ushort C2G_Ping = 10020;
		 public const ushort G2C_Ping = 10021;
		 public const ushort G2C_Test = 10022;
		 public const ushort C2M_Reload = 10023;
		 public const ushort M2C_Reload = 10024;
		 public const ushort C2R_Login = 10025;
		 public const ushort R2C_Login = 10026;
		 public const ushort C2G_LoginGate = 10027;
		 public const ushort G2C_LoginGate = 10028;
		 public const ushort G2C_TestHotfixMessage = 10029;
		 public const ushort C2M_TestRobotCase = 10030;
		 public const ushort M2C_TestRobotCase = 10031;
		 public const ushort C2M_TransferMap = 10032;
		 public const ushort M2C_TransferMap = 10033;
		 public const ushort C2G_Benchmark = 10034;
		 public const ushort G2C_Benchmark = 10035;
		 public const ushort C2A_LoginAccount = 10036;
		 public const ushort A2C_LoginAccount = 10037;
		 public const ushort A2C_Disconnect = 10038;
		 public const ushort ServerInfoProto = 10039;
		 public const ushort C2A_GetServerInfos = 10040;
		 public const ushort A2C_GetServerInfos = 10041;
		 public const ushort RoleInfoProto = 10042;
		 public const ushort C2A_CreateRole = 10043;
		 public const ushort A2C_CreateRole = 10044;
		 public const ushort C2A_GetRoles = 10045;
		 public const ushort A2C_GetRoles = 10046;
		 public const ushort C2A_DeleteRole = 10047;
		 public const ushort A2C_DeleteRole = 10048;
		 public const ushort C2A_GetRealmKey = 10049;
		 public const ushort A2C_GetRealmKey = 10050;
		 public const ushort C2R_LoginRealm = 10051;
		 public const ushort R2C_LoginRealm = 10052;
		 public const ushort C2G_LoginGameGate = 10053;
		 public const ushort G2C_LoginGameGate = 10054;
		 public const ushort C2G_EnterGame = 10055;
		 public const ushort G2C_EnterGame = 10056;
		 public const ushort M2C_NoticeUnitNumeric = 10057;
		 public const ushort C2M_TestUnitNumeric = 10058;
		 public const ushort M2C_TestUnitNumeric = 10059;
		 public const ushort C2M_AddAttributePoint = 10060;
		 public const ushort M2C_AddAttributePoint = 10061;
		 public const ushort C2M_StartGameLevel = 10062;
		 public const ushort M2C_StartGameLevel = 10063;
		 public const ushort C2M_EndGameLevel = 10064;
		 public const ushort M2C_EndGameLevel = 10065;
		 public const ushort C2M_UpRoleLevel = 10066;
		 public const ushort M2C_UpRoleLevel = 10067;
		 public const ushort ItemInfo = 10068;
		 public const ushort M2C_AllItemsList = 10069;
		 public const ushort M2C_ItemUpdateOpInfo = 10070;
		 public const ushort AttributeEntryProto = 10071;
		 public const ushort EquipInfoProto = 10072;
		 public const ushort C2M_EquipItem = 10073;
		 public const ushort M2C_EquipItem = 10074;
		 public const ushort C2M_UnloadEquipItem = 10075;
		 public const ushort M2C_UnloadEquipItem = 10076;
		 public const ushort C2M_SellItem = 10077;
		 public const ushort M2C_SellItem = 10078;
		 public const ushort ProductionProto = 10079;
		 public const ushort C2M_StartProduction = 10080;
		 public const ushort M2C_StartProduction = 10081;
		 public const ushort C2M_ReceiveProduction = 10082;
		 public const ushort M2C_ReceiveProduction = 10083;
		 public const ushort M2C_AllProductionList = 10084;
		 public const ushort TaskInfoProto = 10085;
		 public const ushort M2C_UpdateTaskInfo = 10086;
		 public const ushort M2C_AllTaskInfoList = 10087;
		 public const ushort C2M_ReceiveTaskReward = 10088;
		 public const ushort M2C_ReceiveTaskReward = 10089;
		 public const ushort RankInfoProto = 10090;
		 public const ushort C2Rank_GetRanksInfo = 10091;
		 public const ushort Rank2C_GetRanksInfo = 10092;
		 public const ushort C2Chat_SendChatInfo = 10093;
		 public const ushort Chat2C_SendChatInfo = 10094;
		 public const ushort Chat2C_NoticeChatInfo = 10095;
	}
}
