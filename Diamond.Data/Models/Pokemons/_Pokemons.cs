using Diamond.Data.Models.Pokemons;

using Microsoft.EntityFrameworkCore;

namespace Diamond.Data
{
	public partial class DiamondContext
	{
		public DbSet<DbPokemon> Pokemons { get; set; }
		public DbSet<DbPokemonAttackEffectiveness> PokemonAttackEffectivenesses { get; set; }
		public DbSet<DbPokemonFormat> PokemonFormats { get; set; }
		public DbSet<DbPokemonGeneration> PokemonGenerations { get; set; }
		public DbSet<DbPokemonItem> PokemonItems { get; set; }
		public DbSet<DbPokemonLearnset> PokemonLearnsets { get; set; }
		public DbSet<DbPokemonMove> PokemonMoves { get; set; }
		public DbSet<DbPokemonNature> PokemonNatures { get; set; }
		public DbSet<DbPokemonPassive> PokemonPassives { get; set; }
		public DbSet<DbPokemonStrategy> PokemonStrategies { get; set; }
		public DbSet<PokemonSmogonUser> PokemonSmogonUsers { get; set; }
		public DbSet<DbPokemonStrategyCreditsTeam> PokemonStrategyCreditsTeams { get; set; }
		public DbSet<DbPokemonStrategyMoveset> PokemonStrategyMovesets { get; set; }
		public DbSet<DbPokemonType> PokemonTypes { get; set; }
	}
}