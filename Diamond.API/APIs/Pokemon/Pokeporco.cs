using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.Util;
using Diamond.Data;
using Diamond.Data.Models.Pokemons;

namespace Diamond.API.APIs.Pokemon
{
	public class Pokeporco
	{

		private readonly Dictionary<string, DbPokemon> PokemonGenerationsMap = new Dictionary<string, DbPokemon>();
		public int DexNumber { get; private set; }
		public string PokemonName { get; private set; }

		private readonly DiamondContext _db;

		public Pokeporco(DbPokemon dbPokemon, DiamondContext db)
		{
			this._db = db;

			this.PokemonName = dbPokemon.Name;

			this.SetDexNumber(dbPokemon);
		}

		public Pokeporco(string pokemonName, DiamondContext db)
		{
			this._db = db;

			DbPokemon searchPokemon = this.SearchPokemon(pokemonName);
			this.PokemonName = searchPokemon.Name;

			this.SetDexNumber(searchPokemon);
		}

		public Pokeporco(int dexNumber, DiamondContext db)
		{
			this._db = db;

			this.DexNumber = dexNumber;

			DbPokemon searchPokemon = this.SearchPokemon(this.DexNumber) ?? throw new PokemonNotFoundException(this.DexNumber);

			this.PokemonName = searchPokemon.Name;
			this.CachePokemon(searchPokemon);
		}

		private void SetDexNumber(DbPokemon dbPokemon)
		{
			// This means the pokemon is "form", ex: Charizard-Mega-Y
			if (dbPokemon.DexNumber == null)
			{
				DbPokemon basePokemon = this.GetBasePokemon(dbPokemon.Name);
				this.DexNumber = (int)basePokemon.DexNumber;
			}
			else
			{
				this.DexNumber = (int)dbPokemon.DexNumber;
			}
		}

		private DbPokemon GetBasePokemon(string pokemonName)
		{
			// I hope this works
			string basePokemonName = pokemonName.Split("-")[0];
			DbPokemon basePokemon = this._db.Pokemons.Where(p => p.Name == basePokemonName).FirstOrDefault() ?? throw new PokemonNotFoundException(basePokemonName);
			return basePokemon;
		}

		private DbPokemon SearchPokemon(int dexNumber)
		{
			return this._db.Pokemons.Where(p => p.DexNumber == dexNumber).FirstOrDefault();
		}

		private DbPokemon SearchPokemon(string pokemonName)
		{
			IEnumerable<DbPokemon> uniquePokes = this._db.Pokemons.AsEnumerable().DistinctBy(p => p.Name);

			Dictionary<string, DbPokemon> pokeMap = new Dictionary<string, DbPokemon>();
			foreach (DbPokemon poke in uniquePokes)
			{
				pokeMap.Add(poke.Name, poke);
			}

			List<SearchMatchInfo<DbPokemon>> searchResult = Utils.Search(pokeMap, pokemonName);
			DbPokemon found = searchResult.First().Item;

			this.CachePokemon(found);

			return found;
		}

		private void CachePokemon(DbPokemon dbPokemon)
		{
			this.PokemonGenerationsMap.Add(dbPokemon.GenerationAbbreviation, dbPokemon);
		}

		public DbPokemon GetFromGeneration(string generationAbbreviation)
		{
			if (this.PokemonGenerationsMap.ContainsKey(generationAbbreviation))
			{
				return this.PokemonGenerationsMap[generationAbbreviation];
			}

			IQueryable<DbPokemon> results = this._db.Pokemons.Where(p => p.Name == this.PokemonName && p.GenerationAbbreviation == generationAbbreviation);

			if (!results.Any()) return null;

			DbPokemon found = results.First();
			this.PokemonGenerationsMap.Add(generationAbbreviation, found);
			return found;
		}

		public DbPokemon GetFullFromGeneration(string? generationAbbreviation)
		{
			DbPokemon dbPokemon = this.GetFromGeneration(generationAbbreviation);
			dbPokemon = this._db.SelectFullPokemon(dbPokemon.Id);
			return dbPokemon;
		}

		public string GetPokemonGif(string generationAbbreviation)
		{
			return string.Format(PokemonAPI.SMOGON_POKEMON_GIFS_URL, generationAbbreviation.ToLowerInvariant(), this.PokemonName.ToLowerInvariant().Replace(" ", "-"));
		}

		public string GetPokemonImage()
		{
			return string.Format(PokemonAPI.POKEMON_IMAGES_URL, this.DexNumber);
		}

		public string GetSmogonPokemonUrl(string generationAbbreviation)
		{
			return string.Format(PokemonAPI.SMOGON_POKEMON_URL, generationAbbreviation.ToLowerInvariant(), this.PokemonName.ToLowerInvariant().Replace(" ", "-"));
		}

		public string GetPokedexUrl()
		{
			return string.Format(PokemonAPI.POKEMON_POKEDEX_URL, this.DexNumber);
		}

		public async Task<List<DbPokemonMove>> GetMovesAsync(string generationAbbreviation)
		{
			IQueryable<DbPokemonLearnset> moves = this._db.PokemonLearnsets.Where(ls => ls.Pokemon.Name == this.PokemonName);
			if (!moves.Any())
			{
				await PokemonUtils.DownloadPokemonLearnsetAsync(this.PokemonName, generationAbbreviation, this._db);
				return await this.GetMovesAsync(generationAbbreviation);
			}
			return moves.Select(ls => ls.Move).ToList();
		}

		public async Task<List<DbPokemonStrategy>> GetStrategies(string generationAbbreviation)
		{
			DbPokemon dbPokemon = this.GetFromGeneration(generationAbbreviation);

			IQueryable<DbPokemonStrategy> strategies = this._db.PokemonStrategies.Where(st => st.Pokemon.Name == this.PokemonName && st.Pokemon.GenerationAbbreviation == generationAbbreviation);
			if (!strategies.Any())
			{
				bool hasStrategies = await PokemonUtils.DownloadPokemonStratsAsync(this.PokemonName, generationAbbreviation, this._db);

				dbPokemon.HasStrategies = hasStrategies;
				await this._db.SaveAsync();

				return !hasStrategies ? new List<DbPokemonStrategy>() : await this.GetStrategies(generationAbbreviation);
			}
			return strategies.ToList();
		}
	}

	public class PokemonNotFoundException : Exception
	{
		public PokemonNotFoundException(int dexNumber)
			: base(
				  $"A pokémon with the dex number '{dexNumber}' could not be found."
			)
		{ }

		public PokemonNotFoundException(string pokemonName)
			: base(
				  $"A pokémon named '{pokemonName}' could not be found."
			)
		{ }
	}
}
