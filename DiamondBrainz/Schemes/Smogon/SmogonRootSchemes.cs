using Newtonsoft.Json;

namespace Diamond.API.Schemes.Smogon
{
	public class SmogonRootObject
	{
		[JsonProperty("injectRpcs")] public object[][] InjectRpcs { get; set; }
		[JsonProperty("showEditorUI")] public bool? ShowEditorUI { get; set; }
		[JsonProperty("showAds")] public bool ShowAds { get; set; }
		[JsonProperty("procSettings")] public Procsettings ProcSettings { get; set; }
	}

	public class Procsettings
	{
		[JsonProperty("spriteBase")] public string SpriteBase { get; set; }
	}
}
