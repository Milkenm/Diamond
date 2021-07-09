using Newtonsoft.Json;

namespace Diamond.Brainz
{
	public static partial class Classes
	{
		public class BotConfig
		{
			[JsonProperty("token")] public string Token { get; set; }
			[JsonProperty("prefix")] public string Prefix { get; set; }
			[JsonProperty("debugChannels")] public ulong[] DebugChannels { get; set; }
		}
	}
}
