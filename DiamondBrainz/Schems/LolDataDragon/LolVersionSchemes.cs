using Newtonsoft.Json;

namespace Diamond.API.Schems.LolDataDragon
{
	public class LolVersion
	{
		[JsonProperty("n.item")] public string ItemVersion { get; set; }
		[JsonProperty("n.rune")] public string RuneVersion { get; set; }
		[JsonProperty("n.mastery")] public string MasteryVersion { get; set; }
		[JsonProperty("n.summoner")] public string SummonerVersion { get; set; }
		[JsonProperty("n.champin")] public string ChampionVersion { get; set; }
		[JsonProperty("n.profileicon")] public string ProfileIconVersion { get; set; }
		[JsonProperty("n.map")] public string MapVersion { get; set; }
		[JsonProperty("n.language")] public string LanguageVersion { get; set; }
		[JsonProperty("n.sticker")] public string StickerVersion { get; set; }
		[JsonProperty("v")] public string PatchVersion { get; set; }
		[JsonProperty("l")] public string Language { get; set; }
		[JsonProperty("cdn")] public string DdragonCdnUrl { get; set; }
		[JsonProperty("dd")] public string DdragonVersion { get; set; }
		// Idk what the stuff below is
		[JsonProperty("lg")] public string Lg { get; set; }
		[JsonProperty("css")] public string Css { get; set; }
		[JsonProperty("profileiconmax")] public int ProfileIconMax { get; set; }
		[JsonProperty("store")] public string Store { get; set; }
	}
}
