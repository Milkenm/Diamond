using Diamond.Data.Models.Pokemons;

using Microsoft.EntityFrameworkCore;

namespace Diamond.Data
{
	public partial class DiamondContext
	{
		#region Tables
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
		public DbSet<DbPokemonSmogonUser> PokemonSmogonUsers { get; set; }
		public DbSet<DbPokemonStrategyCreditsTeam> PokemonStrategyCreditsTeams { get; set; }
		public DbSet<DbPokemonStrategyMoveset> PokemonStrategyMovesets { get; set; }
		public DbSet<DbPokemonType> PokemonTypes { get; set; }
		#endregion

		public DbPokemon? SelectFullPokemon(long id)
		{
			return this.Pokemons.Where(p => p.Id == id)
				.Include(p => p.TypesList)
				.Include(p => p.AbilitiesList)
				.Include(p => p.EvolutionsList)
				.Include(p => p.GenerationsList)
				.Include(p => p.FormatsList)
				.First();
		}

		public DbPokemon? SelectPokemon(string pokemonName, DbPokemonGeneration generation)
		{
			return this.Pokemons.Where(p => p.Name == pokemonName && p.GenerationAbbreviation == generation.Abbreviation).FirstOrDefault();
		}

		public async Task<DbPokemon> CreatePokemonAsync(
			string pokemonName, string generationAbbreviation, string isNonstandard,
			int healthPoints, int attack, int defense, int specialAttack, int specialDefense, int speed, float weight, float height,
			int? dexNumber,
			bool save = true
		)
		{
			DbPokemon newPokemon = new DbPokemon()
			{
				Name = pokemonName,
				GenerationAbbreviation = generationAbbreviation,
				TypesList = new List<DbPokemonType>(),
				AbilitiesList = new List<DbPokemonPassive>(),
				FormatsList = new List<DbPokemonFormat>(),
				IsNonstandard = isNonstandard != "Standard",
				HealthPoints = healthPoints,
				Attack = attack,
				Defense = defense,
				SpecialAttack = specialAttack,
				SpecialDefense = specialDefense,
				Speed = speed,
				Weight = weight,
				Height = height,
				DexNumber = dexNumber,
				AltsList = new List<DbPokemon>(), // AltsList is filled later
				EvolutionsList = new List<DbPokemon>(), // EvolutionsList is filled later
				GenerationsList = new List<DbPokemonGeneration>(), // GenerationsList if filled later
				HasStrategies = null,
			};

			if (save)
			{
				await this.SaveAsync();
			}

			return newPokemon;
		}

		public DbPokemonGeneration? SelectGeneration(string generationAbbreviation)
		{
			return this.PokemonGenerations.Where(g => g.Abbreviation == generationAbbreviation).FirstOrDefault();
		}
	}
}