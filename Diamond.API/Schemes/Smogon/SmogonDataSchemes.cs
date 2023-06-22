using System.Collections.Generic;

using Newtonsoft.Json;

namespace Diamond.API.Schemes.Smogon
{
	public class SmogonGeneration
	{
		[JsonProperty("name")] public string Name { get; set; }
		[JsonProperty("shorthand")] public string Shorthand { get; set; }
	}

	public class SmogonData
	{
		[JsonProperty("pokemon")] public List<SmogonPokemonInfo> PokemonsList { get; set; }
		[JsonProperty("formats")] public List<SmogonFormat> FormatsList { get; set; }
		[JsonProperty("natures")] public List<SmogonNature> NaturesList { get; set; }
		[JsonProperty("abilities")] public List<SmogonAbility> AbilitiesList { get; set; }
		[JsonProperty("moveflags")] public List<object> Moveflags { get; set; }
		[JsonProperty("moves")] public List<SmogonMove> MovesList { get; set; }
		[JsonProperty("types")] public List<SmogonPokemonType> TypesList { get; set; }
		[JsonProperty("items")] public List<SmogonItem> ItemsList { get; set; }
	}

	public class SmogonPokemonInfo
	{
		[JsonProperty("name")] public string Name { get; set; }
		[JsonProperty("hp")] public int HealthPoints { get; set; }
		[JsonProperty("atk")] public int Attack { get; set; }
		[JsonProperty("def")] public int Defense { get; set; }
		[JsonProperty("spa")] public int SpecialAttack { get; set; }
		[JsonProperty("spd")] public int SpecialDefense { get; set; }
		[JsonProperty("spe")] public int Speed { get; set; }
		[JsonProperty("weight")] public float Weight { get; set; }
		[JsonProperty("height")] public float Height { get; set; }
		[JsonProperty("types")] public List<string> TypesList { get; set; }
		[JsonProperty("abilities")] public List<string> AbilitiesList { get; set; }
		[JsonProperty("formats")] public List<string> FormatsList { get; set; }
		[JsonProperty("isNonstandard")] public string IsNonstandard { get; set; }
		[JsonProperty("oob.dex_number")] public int DexNumber { get; set; }
		[JsonProperty("oob")] public SmogonOob Oob { get; set; }
	}

	public class SmogonOob
	{
		[JsonProperty("evos")] public List<string> EvolutionsList { get; set; }
		[JsonProperty("alts")] public List<string> AltsList { get; set; }
		[JsonProperty("genfamily")] public List<string> GenerationsList { get; set; }
	}

	public class SmogonFormat
	{
		[JsonProperty("name")] public string Name { get; set; }
		[JsonProperty("shorthand")] public string Abbreviation { get; set; }
		[JsonProperty("genfamily")] public List<string> GenerationsList { get; set; }
	}

	public class SmogonNature
	{
		[JsonProperty("name")] public string Name { get; set; }
		[JsonProperty("hp")] public int HealthPoints { get; set; }
		[JsonProperty("atk")] public float Attack { get; set; }
		[JsonProperty("def")] public float Defense { get; set; }
		[JsonProperty("spa")] public float SpecialAttack { get; set; }
		[JsonProperty("spd")] public float SpecialDefense { get; set; }
		[JsonProperty("spe")] public float Speed { get; set; }
		[JsonProperty("summary")] public string Summary { get; set; }
		[JsonProperty("genfamily")] public List<string> GenerationsList { get; set; }
	}

	public class SmogonAbility
	{
		[JsonProperty("name")] public string Name { get; set; }
		[JsonProperty("description")] public string Description { get; set; }
		[JsonProperty("isNonstandard")] public string IsNonstandard { get; set; }
		[JsonProperty("genfamily")] public List<string> GenerationsList { get; set; }
	}

	public class SmogonMove
	{
		[JsonProperty("name")] public string Name { get; set; }
		[JsonProperty("isNonstandard")] public string IsNonstandard { get; set; }
		[JsonProperty("category")] public string Category { get; set; }
		[JsonProperty("power")] public int Power { get; set; }
		[JsonProperty("accuracy")] public int Accuracy { get; set; }
		[JsonProperty("priority")] public int Priority { get; set; }
		[JsonProperty("pp")] public int PowerPoints { get; set; }
		[JsonProperty("description")] public string Description { get; set; }
		[JsonProperty("type")] public string Type { get; set; }
		[JsonProperty("flags")] public List<object> FlagsList { get; set; }
		[JsonProperty("genfamily")] public List<string> GenerationsList { get; set; }
	}

	public class SmogonPokemonType
	{
		[JsonProperty("name")] public string Name { get; set; }
		[JsonProperty("atk_effectives")] public List<object> AttackEffectivesList { get; set; }
		[JsonProperty("genfamily")] public List<string> GenerationsList { get; set; }
		[JsonProperty("description")] public string Description { get; set; }
	}

	public class SmogonItem
	{
		[JsonProperty("name")] public string Name { get; set; }
		[JsonProperty("description")] public string Description { get; set; }
		[JsonProperty("isNonstandard")] public string IsNonstandard { get; set; }
		[JsonProperty("genfamily")] public List<string> GenerationsList { get; set; }
	}
}
