using System.ComponentModel.DataAnnotations.Schema;

namespace Diamond.Data.Models.Pokemons
{
	[Table("Pokemons")]
	public class DbPokemon
	{
		public long Id { get; set; }
		public required string Name { get; set; }
		public required string TypesList { get; set; }
		public required string AbilitiesList { get; set; }
		public string? FormatsList { get; set; }
		public required bool IsNonstandard { get; set; }
		public required int HealthPoints { get; set; }
		public required int Attack { get; set; }
		public required int Defense { get; set; }
		public required int SpecialAttack { get; set; }
		public required int SpecialDefense { get; set; }
		public required int Speed { get; set; }
		public required float Weight { get; set; }
		public required float Height { get; set; }
		public int? DexNumber { get; set; }
		public string? EvolutionsList { get; set; }
		public string? GenerationsList { get; set; }
	}
}