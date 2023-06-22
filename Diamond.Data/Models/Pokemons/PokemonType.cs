using System.ComponentModel.DataAnnotations.Schema;

namespace Diamond.Data.Models.Pokemons
{
	[Table("PokemonTypes")]
	public class DbPokemonType
	{
		public long Id { get; set; }
		public required string Name { get; set; }
		public required string Description { get; set; }
		public required string GenerationsList { get; set; }
	}
}