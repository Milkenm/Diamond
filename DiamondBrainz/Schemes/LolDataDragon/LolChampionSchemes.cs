using System.Collections.Generic;

using Newtonsoft.Json;

namespace Diamond.API.Schemes.LolDataDragon
{
	public class LolChampionData
	{
		[JsonProperty("type")] public string Type { get; set; }
		[JsonProperty("format")] public string Format { get; set; }
		[JsonProperty("version")] public string Version { get; set; }
		[JsonProperty("data")] public Dictionary<string, LolChampion> ChampionsList { get; set; }
	}

	public class LolChampion
	{
		[JsonProperty("id")] public string InternalChampionName { get; set; }
		[JsonProperty("key")] public string ChampionId { get; set; }
		[JsonProperty("name")] public string ChampionName { get; set; }
		[JsonProperty("title")] public string ChampionTitle { get; set; }
		[JsonProperty("image")] public LolImage ImageInfo { get; set; }
		[JsonProperty("skins")] public List<LolChampionSkin> SkinsList { get; set; }
		[JsonProperty("lore")] public string Lore { get; set; }
		[JsonProperty("blurb")] public string ShortLore { get; set; }
		[JsonProperty("allytips")] public List<string> AllyTipsList { get; set; }
		[JsonProperty("enemytips")] public List<string> EnemyTipsList { get; set; }
		[JsonProperty("tags")] public List<string> Tags { get; set; }
		[JsonProperty("partype")] public string ResourceType { get; set; }
		[JsonProperty("info")] public LolChampionStatsResume ChampionStatsResume { get; set; }
		[JsonProperty("stats")] public LolChampionStats Stats { get; set; }
		[JsonProperty("spells")] public List<LolChampionAbility> AbilitiesList { get; set; }
		[JsonProperty("passive")] public List<LolChampionPassive> Passive { get; set; }
		[JsonProperty("version")] public string Version { get; set; }
	}

	public class LolChampionStatsResume
	{
		[JsonProperty("attack")] public int Attack { get; set; }
		[JsonProperty("defense")] public int Defense { get; set; }
		[JsonProperty("magic")] public int Magic { get; set; }
		[JsonProperty("difficulty")] public int Difficulty { get; set; }
	}

	public class LolImage
	{
		[JsonProperty("full")] public string FullImageName { get; set; }
		[JsonProperty("sprite")] public string SpriteImageName { get; set; }
		[JsonProperty("group")] public string ImageGroupName { get; set; }
		[JsonProperty("x")] public int SpriteX { get; set; }
		[JsonProperty("y")] public int SpriteY { get; set; }
		[JsonProperty("w")] public int SpriteWidth { get; set; }
		[JsonProperty("h")] public int SpriteHeight { get; set; }
	}

	public class LolChampionSkin
	{
		[JsonProperty("id")] public string Id { get; set; }
		[JsonProperty("num")] public int SkinIndex { get; set; }
		[JsonProperty("name")] public string SkinName { get; set; }
		[JsonProperty("chromas")] public bool HasChromas { get; set; }
	}

	public class LolChampionStats
	{
		[JsonProperty("hp")] public double Health { get; set; }
		[JsonProperty("hpperlevel")] public double HealthPerLevel { get; set; }
		[JsonProperty("mp")] public double Mana { get; set; }
		[JsonProperty("mpperlevel")] public double ManaPerLevel { get; set; }
		[JsonProperty("movespeed")] public int MovementSpeed { get; set; }
		[JsonProperty("armor")] public double Armor { get; set; }
		[JsonProperty("armorperlevel")] public double ArmorPerLevel { get; set; }
		[JsonProperty("spellblock")] public double MagicResist { get; set; }
		[JsonProperty("spellblockperlevel")] public double MagicResistPerLevel { get; set; }
		[JsonProperty("attackrange")] public int AttackRange { get; set; }
		[JsonProperty("hpregen")] public double HealthRegeneration { get; set; }
		[JsonProperty("hpregenperlevel")] public double HealthRegenerationPerLevel { get; set; }
		[JsonProperty("mpregen")] public double ManaRegeneration { get; set; }
		[JsonProperty("mpregenperlevel")] public double ManaRegenerationPerLevel { get; set; }
		[JsonProperty("crit")] public int CriticalStrikeChance { get; set; }
		[JsonProperty("critperlevel")] public int CriticalStrikeChancePerLevel { get; set; }
		[JsonProperty("attackdamage")] public double AttackDamage { get; set; }
		[JsonProperty("attackdamageperlevel")] public double AttackDamagePerLevel { get; set; }
		[JsonProperty("attackspeed")] public double AttackSpeed { get; set; }
		[JsonProperty("attackspeedperlevel")] public double AttackSpeedPerLevel { get; set; }
	}

	public class LolChampionAbility
	{
		[JsonProperty("id")] public string Id { get; set; }
		[JsonProperty("name")] public string Name { get; set; }
		[JsonProperty("description")] public string Description { get; set; }
		[JsonProperty("tooltip")] public string Tooltip { get; set; }
		[JsonProperty("leveltip")] public LolChampionAbilityLevelTip LevelTipsList { get; set; }
		[JsonProperty("maxrank")] public int MaxLevel { get; set; }
		[JsonProperty("cooldown")] public double CooldownsPerLevelList { get; set; }
		[JsonProperty("cooldownBurn")] public string CooldownBurn { get; set; }
		[JsonProperty("cost")] public double ResourceCost { get; set; }
		[JsonProperty("costBurn")] public string CostBurn { get; set; }
		// TODO: [JsonProperty("datavalues")] 
		[JsonProperty("costType")] public string CostType { get; set; }
		[JsonProperty("maxammo")] public string MaxAmmo { get; set; }
		[JsonProperty("range")] public List<int> RangePerLevelList { get; set; }
		[JsonProperty("rangeBurn")] public string RangeBurn { get; set; }
		[JsonProperty("image")] public LolImage Image { get; set; }
		[JsonProperty("resource")] public string Resource { get; set; }
	}

	public class LolChampionAbilityLevelTip
	{
		[JsonProperty("label")] public List<string> LabelsList { get; set; }
		[JsonProperty("effect")] public List<string> EffectsList { get; set; }
	}

	public class LolChampionPassive
	{
		[JsonProperty("name")] public string Name { get; set; }
		[JsonProperty("description")] public string Description { get; set; }
		[JsonProperty("image")] public LolImage Image { get; set; }
	}
}