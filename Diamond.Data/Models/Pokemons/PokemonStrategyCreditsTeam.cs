using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diamond.Data.Models.Pokemons
{

	[Table("PokemonStrategyCreditsTeams")]
	public class DbPokemonStrategyCreditsTeam
	{
		[Key] public long Id { get; set; }
		public required string TeamName { get; set; }
		public required List<PokemonSmogonUser> TeamMembersList { get; set; }
	}
}
