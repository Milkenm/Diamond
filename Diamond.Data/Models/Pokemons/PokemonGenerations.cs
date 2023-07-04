using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diamond.Data.Models.Pokemons
{
	[Table("PokemonGenerations")]
	public class DbPokemonGeneration
	{
		[Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public long Id { get; set; }
		public required string Name { get; set; }
		public required string Abbreviation { get; set; }
		public required List<DbPokemon> PokemonsWithGenerationList { get; set; }
		public required List<DbPokemonFormat> FormatsWithGenerationList { get; set; }
		public required List<DbPokemonItem> ItemsWithGenerationList { get; set; }
		public required List<DbPokemonMove> MovesWithGenerationList { get; set; }
		public required List<DbPokemonNature> NaturesWithGenerationList { get; set; }
		public required List<DbPokemonType> TypesWithGenerationList { get; set; }
		public required List<DbPokemonPassive> PassivesWithGenerationList { get; set; }
	}
}