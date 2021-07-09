using Newtonsoft.Json;

namespace Diamond.Brainz
{
	public static partial class Classes
	{
		public class JsonConfig
		{
			[JsonProperty("bot")] public BotConfig BotConfig { get; set; }
			[JsonProperty("database")] public DatabaseConfig DatabaseConfig { get; set; }
		}
	}
}
