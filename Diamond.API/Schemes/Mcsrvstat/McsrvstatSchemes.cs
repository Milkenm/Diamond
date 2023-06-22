using System.Collections.Generic;

using Newtonsoft.Json;

namespace Diamond.API.Schemes.Mcsrvstat
{
	public class ServerStatus
	{
		[JsonProperty("online")] public bool IsOnline { get; set; }
		[JsonProperty("ip")] public string IP { get; set; }
		[JsonProperty("port")] public int Port { get; set; }
		[JsonProperty("debug")] public ServerDebug Debug { get; set; }
		[JsonProperty("motd")] public ServerMotd Motd { get; set; }
		[JsonProperty("players")] public ServerPlayers Players { get; set; }
		[JsonProperty("version")] public string Version { get; set; }
		[JsonProperty("protocol")] public int? Protocol { get; set; }
		[JsonProperty("hostname")] public string? Hostname { get; set; }
		[JsonProperty("icon")] public string? IconBase64 { get; set; }
		[JsonProperty("software")] public string? Software { get; set; }
		[JsonProperty("map")] public string Map { get; set; }
		[JsonProperty("gamemode")] public string? Gamemode { get; set; }
		[JsonProperty("serverid")] public string ServerId { get; set; }
		[JsonProperty("plugins")] public ServerAddons? Plugins { get; set; }
		[JsonProperty("mods")] public ServerAddons? Mods { get; set; }
		[JsonProperty("info")] public ServerInfo? Info { get; set; }
	}

	public class ServerDebug
	{
		[JsonProperty("ping")] public bool Ping { get; set; }
		[JsonProperty("query")] public bool Query { get; set; }
		[JsonProperty("srv")] public bool SRV { get; set; }
		[JsonProperty("querymismatch")] public bool QueryMismatch { get; set; }
		[JsonProperty("ipinsrv")] public bool IPinSRV { get; set; }
		[JsonProperty("cnameinsrv")] public bool CNAMEinSRV { get; set; }
		[JsonProperty("animatedmotd")] public bool AnimatedMotd { get; set; }
		[JsonProperty("cachehit")] public bool CacheHit { get; set; }
		[JsonProperty("cachetime")] public long CacheTime { get; set; }
		[JsonProperty("cacheexpire")] public long CacheExpire { get; set; }
		[JsonProperty("apiversion")] public int APIVersion { get; set; }
	}

	public class ServerMotd
	{
		[JsonProperty("raw")] public List<string> RawMotd { get; set; }
		[JsonProperty("clean")] public List<string> CleanMotd { get; set; }
		[JsonProperty("html")] public List<string> HtmlMotd { get; set; }
	}

	public class ServerPlayers
	{
		[JsonProperty("online")] public int OnlinePlayersCount { get; set; }
		[JsonProperty("max")] public int MaxPlayers { get; set; }
		[JsonProperty("list")] public List<string>? OnlinePlayersNames { get; set; }
		[JsonProperty("uuid")] public Dictionary<string, string>? OnlinePlayersUUID { get; set; }
	}

	public class ServerAddons
	{
		[JsonProperty("names")] public List<string> Names { get; set; }
		[JsonProperty("raw")] public List<string> Raw { get; set; }
	}

	public class ServerInfo
	{
		[JsonProperty("raw")] public List<string> RawInfo { get; set; }
		[JsonProperty("clean")] public List<string> CleanInfo { get; set; }
		[JsonProperty("html")] public List<string> HtmlInfo { get; set; }
	}
}
