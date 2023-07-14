using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diamond.Data.Models.Pokemons
{
	[Table("PokemonFormats")]
	public class DbPokemonFormat
	{
		[Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public long Id { get; set; }
		public required string Name { get; set; }
		public required string Abbreviation { get; set; }
		public required List<DbPokemonGeneration> GenerationsList { get; set; }
		public required List<DbPokemon> PokemonsWithFormatList { get; set; }
		public required string GenerationAbbreviation { get; set; }
	}
}