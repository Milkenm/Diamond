using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diamond.Data.Models.Pokemons
{
	[Table("PokemonTypes")]
	public class DbPokemonType
	{
		[Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public long Id { get; set; }
		public required string Name { get; set; }
		public required string Description { get; set; }
		public required List<DbPokemonGeneration> GenerationsList { get; set; }
		public required List<DbPokemon> PokemonsWithTypeList { get; set; }
		public required string GenerationAbbreviation { get; set; }
	}
}