using System.Collections.Generic;

using Newtonsoft.Json;

namespace Diamond.API.Schemes.Smogon
{
	public class Generation
	{
		[JsonProperty("name")] public string Name { get; set; }
		[JsonProperty("shorthand")] public string Shorthand { get; set; }
	}

	public class SmogonData
	{
		public List<PokemonInfo> pokemon { get; set; }
		public List<Format> formats { get; set; }
		public List<Nature> natures { get; set; }
		public List<Ability> abilities { get; set; }
		public List<object> moveflags { get; set; }
		public List<Move> moves { get; set; }
		public List<Type> types { get; set; }
		public List<Item> items { get; set; }
	}

	public class PokemonInfo
	{
		public string name { get; set; }
		public int hp { get; set; }
		public int atk { get; set; }
		public int def { get; set; }
		public int spa { get; set; }
		public int spd { get; set; }
		public int spe { get; set; }
		public float weight { get; set; }
		public float height { get; set; }
		public List<string> types { get; set; }
		public List<string> abilities { get; set; }
		public List<string> formats { get; set; }
		public string isNonstandard { get; set; }
		public Oob oob { get; set; }
	}

	public class Oob
	{
		public int dex_number { get; set; }
		public List<string> evos { get; set; }
		public List<string> alts { get; set; }
		public List<string> genfamily { get; set; }
	}

	public class Format
	{
		public string name { get; set; }
		public string shorthand { get; set; }
		public List<string> genfamily { get; set; }
	}

	public class Nature
	{
		public string name { get; set; }
		public int hp { get; set; }
		public float atk { get; set; }
		public float def { get; set; }
		public float spa { get; set; }
		public float spd { get; set; }
		public float spe { get; set; }
		public string summary { get; set; }
		public List<string> genfamily { get; set; }
	}

	public class Ability
	{
		public string name { get; set; }
		public string description { get; set; }
		public string isNonstandard { get; set; }
		public List<string> genfamily { get; set; }
	}

	public class Move
	{
		public string name { get; set; }
		public string isNonstandard { get; set; }
		public string category { get; set; }
		public int power { get; set; }
		public int accuracy { get; set; }
		public int priority { get; set; }
		public int pp { get; set; }
		public string description { get; set; }
		public string type { get; set; }
		public List<object> flags { get; set; }
		public List<string> genfamily { get; set; }
	}

	public class Type
	{
		public string name { get; set; }
		public List<object> atk_effectives { get; set; }
		public List<string> genfamily { get; set; }
		public string description { get; set; }
	}

	public class Item
	{
		public string name { get; set; }
		public string description { get; set; }
		public string isNonstandard { get; set; }
		public List<string> genfamily { get; set; }
	}
}
