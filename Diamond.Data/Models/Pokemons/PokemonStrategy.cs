using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diamond.Data.Models.Pokemons
{
	[Table("PokemonStrategies")]
	public class DbPokemonStrategy
	{
		[Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public long Id { get; set; }
		public required DbPokemonFormat Format { get; set; }
		public bool? Outdated { get; set; }
		public required DbPokemon Pokemon { get; set; }
		public required string Overview { get; set; }
		public required string Comments { get; set; }
		public required string MovesetName { get; set; }
		public required string Gender { get; set; }
		public required List<DbPokemonPassive> PassivesList { get; set; }
		public required List<DbPokemonItem> ItemsList { get; set; }
		public required List<DbPokemonStrategyMoveset> Movesets { get; set; }
		public required int EVsHealth { get; set; }
		public required int EVsAttack { get; set; }
		public required int EVsDefense { get; set; }
		public required int EVsSpecialAttack { get; set; }
		public required int EVsSpecialDefense { get; set; }
		public required int EVsSpeed { get; set; }
		public required List<DbPokemonNature> NaturesList { get; set; }
		public required List<DbPokemonSmogonUser> WrittenByUsersList { get; set; }
		public required List<DbPokemonStrategyCreditsTeam> TeamsList { get; set; }
	}
}
