using Newtonsoft.Json;

namespace Diamond.API.Schemes.Smogon
{
	public class SmogonPokemonStrats
	{
		[JsonProperty("language")] public string[] Languages { get; set; }
		[JsonProperty("learnset")] public string[] Learnset { get; set; }
		[JsonProperty("strategies")] public object[] Strategies { get; set; }
	}
}
