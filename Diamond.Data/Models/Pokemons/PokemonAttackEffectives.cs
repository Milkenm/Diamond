using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diamond.Data.Models.Pokemons
{
	[Table("PokemonAttackEffectives")]
	public class DbPokemonAttackEffectiveness
	{
		[Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public long Id { get; set; }
		public required DbPokemonType AttackerType { get; set; }
		public required DbPokemonType TargetType { get; set; }
		public required float Value { get; set; }
	}
}