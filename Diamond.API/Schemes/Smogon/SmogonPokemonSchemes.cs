using System.Collections.Generic;

using Newtonsoft.Json;

namespace Diamond.API.Schemes.Smogon
{
	public class SmogonPokemonStrats
	{
		[JsonProperty("language")] public List<string> Languages { get; set; }
		[JsonProperty("learnset")] public List<string> Learnset { get; set; }
		[JsonProperty("strategies")] public List<object> Strategies { get; set; }
	}
}
