using Newtonsoft.Json;

namespace Diamond.API.Schemes.Smogon
{
	public class SmogonRootObject
	{
		[JsonProperty("injectRpcs")] public object[][] InjectRpcs { get; set; }
	}
}
