using Newtonsoft.Json;

namespace Diamond.Brainz
{
	public static partial class Models
	{
		public class JsonConfig
		{
			[JsonProperty("token")] public string Token;
			[JsonProperty("database")] public DatabaseConfig DatabaseConfig;
		}
	}
}
