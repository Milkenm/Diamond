using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diamond.Data.Models.Pokemons
{
	[Table("PokemonLearnsets")]
	public class DbPokemonLearnset
	{
		[Key] public long Id { get; set; }
		public required DbPokemon Pokemon { get; set; }
		public required DbPokemonMove Move { get; set; }
	}
}
