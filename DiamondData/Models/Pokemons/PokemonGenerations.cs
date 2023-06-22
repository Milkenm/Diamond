using System.ComponentModel.DataAnnotations.Schema;

namespace Diamond.Data.Models.Pokemons
{
	[Table("PokemonGenerations")]
	public class DbPokemonGenerations
	{
		public long Id { get; set; }
		public required string Name { get; set; }
		public required string Abbreviation { get; set; }
	}
}