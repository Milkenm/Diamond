using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diamond.Data.Models.Pokemons
{

	[Table("PokemonStrategyCreditsTeams")]
	public class DbPokemonStrategyCreditsTeam
	{
		[Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public long Id { get; set; }
		public required string TeamName { get; set; }
		public required List<DbPokemonSmogonUser> TeamMembersList { get; set; }
	}
}
