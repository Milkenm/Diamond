using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diamond.Data.Models.Pokemons
{
	[Table("Pokemons")]
	public class DbPokemon
	{
		[Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public long Id { get; set; }
		public required string Name { get; set; }
		public required List<DbPokemonType> TypesList { get; set; }
		public required List<DbPokemonPassive> AbilitiesList { get; set; }
		public required List<DbPokemonFormat> FormatsList { get; set; }
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
		public required List<DbPokemon> EvolutionsList { get; set; }
		public required List<DbPokemon> AltsList { get; set; }
		public required List<DbPokemonGeneration> GenerationsList { get; set; }
		public bool? HasStrategies { get; set; }
		public required string GenerationAbbreviation { get; set; }
	}
}