using System.Collections.Generic;

using Newtonsoft.Json;

namespace Diamond.API.Schemes.Smogon
{

	public class SmogonAditionalInfo
	{
		[JsonProperty("languages")] public List<string> LanguagesList { get; set; }
		[JsonProperty("learnset")] public List<string> LearnsetList { get; set; }
		[JsonProperty("strategies")] public List<SmogonStrategy> StrategiesList { get; set; }
	}

	public class SmogonStrategy
	{
		[JsonProperty("format")] public string Format { get; set; }
		[JsonProperty("outdated")] public bool? Outdated { get; set; }
		[JsonProperty("overview")] public string Overview { get; set; }
		[JsonProperty("comments")] public string Comments { get; set; }
		[JsonProperty("movesets")] public List<SmogonStrategyMoveSet> MovesetsList { get; set; }
		[JsonProperty("credits")] public SmogonStrategyCredits Credits { get; set; }
	}

	public class SmogonStrategyCredits
	{
		[JsonProperty("writtenBy")] public List<SmogonStrategyMember> WrittenByList { get; set; }
		[JsonProperty("teams")] public List<SmogonStrategyTeam> TeamsList { get; set; }
	}

	public class SmogonStrategyTeam
	{
		[JsonProperty("name")] public string Name { get; set; }
		[JsonProperty("members")] public List<SmogonStrategyMember> MembersList { get; set; }
	}

	public class SmogonStrategyMember
	{
		[JsonProperty("username")] public string Username { get; set; }
		[JsonProperty("user_id")] public int UserId { get; set; }
	}

	public class SmogonStrategyMoveSet
	{
		[JsonProperty("name")] public string Name { get; set; }
		[JsonProperty("pokemon")] public string Pokemon { get; set; }
		[JsonProperty("shiny")] public bool IsShiny { get; set; }
		[JsonProperty("gender")] public string Gender { get; set; }
		[JsonProperty("levels")] public List<object> LevelsList { get; set; }
		[JsonProperty("description")] public string Description { get; set; }
		[JsonProperty("abilities")] public List<string> AbilitiesList { get; set; }
		[JsonProperty("items")] public List<string> ItemsList { get; set; }
		[JsonProperty("teratypes")] public List<object> TeratypesList { get; set; }
		[JsonProperty("moveslots")] public List<List<SmogonStrategyMoveSlot>> MoveSlotsList { get; set; }
		[JsonProperty("evconfigs")] public List<SmogonStrategyStatsConfig> EvConfigsList { get; set; }
		[JsonProperty("ivconfigs")] public List<SmogonStrategyStatsConfig> IvConfigsList { get; set; }
		[JsonProperty("natures")] public List<string> NaturesList { get; set; }
	}

	public class SmogonStrategyMoveSlot
	{
		[JsonProperty("move")] public string Move { get; set; }
		[JsonProperty("type")] public string? Type { get; set; }
	}

	public class SmogonStrategyStatsConfig
	{
		[JsonProperty("hp")] public int HP { get; set; }
		[JsonProperty("atk")] public int Attack { get; set; }
		[JsonProperty("def")] public int Defense { get; set; }
		[JsonProperty("spa")] public int SpecialAttack { get; set; }
		[JsonProperty("spd")] public int SpecialDefense { get; set; }
		[JsonProperty("spe")] public int Speed { get; set; }
	}
}
