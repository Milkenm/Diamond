using System;
using System.Diagnostics;

using Diamond.API.Schemes.Mcsrvstat;

using ScriptsLibV2.Util;

namespace Diamond.API.APIs
{
	public class McsrvstatAPI
	{
		// {0}: Address
		private const string JAVA_API = "https://api.mcsrvstat.us/2/{0}";
		// {0}: Address
		private const string BEDROCK_API = "https://api.mcsrvstat.us/bedrock/2/{0}";

		private readonly Cache<ServerStatus> _cache = new Cache<ServerStatus>();

		public ServerStatus GetServerStatus(string hostname, int? port = null, bool isBedrock = false)
		{
			string serverIp = hostname + (port != null ? $":{port}" : "");
			if (this._cache.GetCachedValue(serverIp) is ServerStatus cachedStatus)
			{
				Debug.WriteLine("Returned ServerStatus from cache");
				return cachedStatus;
			}

			string requestUrl = string.Format(!isBedrock ? JAVA_API : BEDROCK_API, serverIp);
			ServerStatus serverStatus = RequestUtils.Get<ServerStatus>(requestUrl);

			long keepFor = GetTimeUntilNextRefresh(serverStatus);
			if (keepFor > 0)
			{
				Debug.WriteLine($"Saving ServerStatus to cache for {keepFor} seconds");
				this._cache.CacheValue(serverIp, serverStatus, keepFor);
			}

			Debug.WriteLine("Returned ServerStatus from API");
			return serverStatus;
		}

		public static long GetTimeUntilNextRefresh(ServerStatus serverStatus)
		{
			return serverStatus.Debug.CacheExpire - DateTimeOffset.UtcNow.ToUnixTimeSeconds();
		}
	}
}
