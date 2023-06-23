using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diamond.Data.Models.Pokemons
{
	[Table("PokemonSmogonUsers")]
	public class PokemonSmogonUser
	{
		[Key] public long Id { get; set; }
		public required string Username { get; set; }
		public required long SmogonUserId { get; set; }
	}
}
