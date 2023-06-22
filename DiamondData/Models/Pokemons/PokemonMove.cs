using System.ComponentModel.DataAnnotations.Schema;

namespace Diamond.Data.Models.Pokemons
{
	[Table("PokemonAbilities")]
	public class DbPokemonMove
	{
		public long Id { get; set; }
		public required string Name { get; set; }
		public required string Description { get; set; }
		public required string Type { get; set; }
		public required string Category { get; set; }
		public required int Power { get; set; }
		public required int Accuracy { get; set; }
		public required int Priority { get; set; }
		public required int PowerPoints { get; set; }
		public required string GenerationsList { get; set; }
	}
}