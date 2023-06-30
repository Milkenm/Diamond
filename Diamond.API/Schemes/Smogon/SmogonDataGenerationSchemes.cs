using Newtonsoft.Json;

namespace Diamond.API.Schemes.Smogon
{
	public class SmogonGeneration
	{
		[JsonProperty("name")] public string Name { get; set; }
		[JsonProperty("shorthand")] public string Shorthand { get; set; }
	}
}
