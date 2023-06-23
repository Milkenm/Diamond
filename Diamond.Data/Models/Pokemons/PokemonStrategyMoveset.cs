using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diamond.Data.Models.Pokemons
{
	[Table("PokemonStrategyMovesets")]
	public class DbPokemonStrategyMoveset
	{
		[Key] public long Id { get; set; }
		public required DbPokemonMove Move { get; set; }
		public DbPokemonType? Type { get; set; }
	}
}
