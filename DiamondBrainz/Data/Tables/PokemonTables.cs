using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace Diamond.API.Data
{
	public partial class DiamondContext
	{
		public DbSet<DbPokemon> Pokemons { get; set; }
		public DbSet<PokemonAbility> PokemonAbilities { get; set; }
		public DbSet<PokemonFormat> PokemonFormats { get; set; }
		public DbSet<PokemonItem> PokemonItems { get; set; }
		public DbSet<PokemonNature> PokemonNatures { get; set; }
		public DbSet<PokemonPassive> PokemonPassives { get; set; }
		public DbSet<DbPokemonType> PokemonTypes { get; set; }
		public DbSet<DbPokemonAttackEffectives> PokemonAttackEffectives { get; set; }
	}

	[Table("Pokemons")]
	public class DbPokemon
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string TypesList { get; set; }
		public string AbilitiesList { get; set; }
		public string? FormatsList { get; set; }
		public bool IsNonstandard { get; set; }
		public int HealthPoints { get; set; }
		public int Attack { get; set; }
		public int Defense { get; set; }
		public int SpecialAttack { get; set; }
		public int SpecialDefense { get; set; }
		public int Speed { get; set; }
		public float Weight { get; set; }
		public float Height { get; set; }
		public int? DexNumber { get; set; }
		public string? EvolutionsList { get; set; }
		public string? GenerationsList { get; set; }
	}

	[Table("PokemonAbilities")]
	public class PokemonAbility
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Type { get; set; }
		public string Category { get; set; }
		public int Power { get; set; }
		public int Accuracy { get; set; }
		public int Priority { get; set; }
		public int PowerPoints { get; set; }
		public string GenerationsList { get; set; }
	}

	[Table("PokemonFormats")]
	public class PokemonFormat
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string Abbreviation { get; set; }
		public string GenerationsList { get; set; }
	}

	[Table("PokemonItems")]
	public class PokemonItem
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public bool IsNonstandard { get; set; }
		public string GenerationsList { get; set; }
	}

	[Table("PokemonNatures")]
	public class PokemonNature
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string Summary { get; set; }
		public int HealthPoints { get; set; }
		public float Attack { get; set; }
		public float Defense { get; set; }
		public float SpecialAttack { get; set; }
		public float SpecialDefense { get; set; }
		public float Speed { get; set; }
		public string GenerationsList { get; set; }
	}

	[Table("PokemonPassives")]
	public class PokemonPassive
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public bool IsNonstandard { get; set; }
		public string GenerationsList { get; set; }
	}

	[Table("PokemonTypes")]
	public class DbPokemonType
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string GenerationsList { get; set; }
	}

	[Table("PokemonAttackEffectives")]
	public class DbPokemonAttackEffectives
	{
		public long Id { get; set; }
		public DbPokemonType AttackerType { get; set; }
		public DbPokemonType TargetType { get; set; }
		public float Value { get; set; }
	}
}