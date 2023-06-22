using System.ComponentModel.DataAnnotations.Schema;

namespace Diamond.Data.Models.Pokemons
{
	[Table("PokemonNatures")]
	public class DbPokemonNature
	{
		public long Id { get; set; }
		public required string Name { get; set; }
		public required string Summary { get; set; }
		public required int HealthPoints { get; set; }
		public required float Attack { get; set; }
		public required float Defense { get; set; }
		public required float SpecialAttack { get; set; }
		public required float SpecialDefense { get; set; }
		public required float Speed { get; set; }
		public required string GenerationsList { get; set; }
	}
}