using System.Collections.Generic;

using Newtonsoft.Json;

namespace Diamond.API.Schemes.Smogon
{
	public class SmogonPokemonStrats
	{
		[JsonProperty("language")] public List<string> Languages { get; set; }
		[JsonProperty("learnset")] public List<string> Learnset { get; set; }
		// TODO: vvvv this is broken vvvv ('v' thi is an arrow btw)
		[JsonProperty("strategies")] public List<object> Strategies { get; set; }
	}
}
