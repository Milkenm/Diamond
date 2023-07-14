using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Diamond.API.Util;
using Diamond.Data;
using Diamond.Data.Models.Pokemons;

using Microsoft.EntityFrameworkCore;

namespace Diamond.API.APIs.Pokemon
{
	public class Pokeporco
	{
		private readonly Dictionary<string, DbPokemon> _pokemonGenerationsMap = new Dictionary<string, DbPokemon>();

		public int DexNumber { get; private set; }
		public string PokemonName { get; private set; }
		public List<DbPokemonGeneration> Generations { get; private set; }

		private readonly DiamondContext _db;

		public Pokeporco(DbPokemon dbPokemon, DiamondContext db)
		{
			this._db = db;

			this.PokemonName = dbPokemon.Name;
			this.Generations = this.GetGenerationsList();

			this.SetDexNumber(dbPokemon);
		}

		public Pokeporco(string pokemonName, DiamondContext db)
		{
			this._db = db;

			DbPokemon searchPokemon = this.SearchPokemon(pokemonName);
			this.PokemonName = searchPokemon.Name;
			this.Generations = this.GetGenerationsList();

			this.SetDexNumber(searchPokemon);
		}

		public Pokeporco(int dexNumber, DiamondContext db)
		{
			this._db = db;

			this.DexNumber = dexNumber;

			DbPokemon searchPokemon = this.SearchPokemon(this.DexNumber) ?? throw new PokemonNotFoundException(this.DexNumber);

			this.PokemonName = searchPokemon.Name;
			this.Generations = this.GetGenerationsList();
			this.CachePokemon(searchPokemon);
		}

		private List<DbPokemonGeneration> GetGenerationsList()
		{
			DbPokemon dbPokemonWithGenerations = this._db.Pokemons.Where(p => p.Name == this.PokemonName).Include(p => p.GenerationsList).First();
			return dbPokemonWithGenerations.GenerationsList;
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
			this._pokemonGenerationsMap.Add(dbPokemon.GenerationAbbreviation, dbPokemon);
		}

		public string GetValidGeneration(string? generationAbbreviation)
		{
			bool exists = false;
			if (generationAbbreviation != null)
			{
				foreach (DbPokemonGeneration dbGeneration in this.Generations)
				{
					if (dbGeneration.Abbreviation == generationAbbreviation)
					{
						exists = true;
						break;
					}
				}
			}
			return exists ? generationAbbreviation : this.Generations.Last().Abbreviation;
		}

		public DbPokemon GetFromGeneration(string generationAbbreviation)
		{
			if (this._pokemonGenerationsMap.ContainsKey(generationAbbreviation))
			{
				return this._pokemonGenerationsMap[generationAbbreviation];
			}

			IQueryable<DbPokemon> results = this._db.Pokemons.Where(p => p.Name == this.PokemonName && p.GenerationAbbreviation == generationAbbreviation);

			if (!results.Any()) return null;

			DbPokemon found = results.First();
			this._pokemonGenerationsMap.Add(generationAbbreviation, found);
			return found;
		}

		public DbPokemon? GetFullFromGeneration(string? generationAbbreviation)
		{
			DbPokemon dbPokemon = this.GetFromGeneration(generationAbbreviation);
			if (dbPokemon == null) return null;

			dbPokemon = this._db.SelectFullPokemon(dbPokemon.Id);
			return dbPokemon;
		}

		public string GetPokemonGif(string generationAbbreviation)
		{
			string imageFormat = "gif";
			if (generationAbbreviation is "RB" or "RS" or "DP")
			{
				imageFormat = "png";
			}
			else if (generationAbbreviation == "GS")
			{
				generationAbbreviation = "C";
			}
			else if (generationAbbreviation is "SM" or "SS" or "SV")
			{
				generationAbbreviation = "XY";
			}
			return string.Format(PokemonAPI.SMOGON_POKEMON_IMAGES_URL, generationAbbreviation.ToLowerInvariant(), this.PokemonName.ToLowerInvariant().Replace(" ", "-").Replace(".", ""), imageFormat);
		}

		public string GetPokemonImage()
		{
			return string.Format(PokemonAPI.POKEMON_IMAGES_URL, this.DexNumber.ToString().PadLeft(3, '0'));
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

	public class PokemonTypeEffectiveness
	{
		private readonly DiamondContext _db;

		private readonly Dictionary<PokemonType, double> EffectivenessMap = new Dictionary<PokemonType, double>();

		public PokemonTypeEffectiveness(List<DbPokemonType> pokemonTypes, DiamondContext db)
		{
			this._db = db;

			foreach (DbPokemonType type in pokemonTypes)
			{
				this.EffectivenessMap.Merge(this.GetEffectivenessMapForType(type), DictionaryMergeOperation.Multiply);
			}
		}

		private Dictionary<PokemonType, double> GetEffectivenessMapForType(DbPokemonType type)
		{
			Dictionary<PokemonType, double> effectivenessMap = new Dictionary<PokemonType, double>();

			foreach (DbPokemonAttackEffectiveness atkef in this._db.PokemonAttackEffectivenesses.Include(af => af.AttackerType).Where(af => af.TargetType == type && af.GenerationAbbreviation == type.GenerationAbbreviation))
			{
				Debug.WriteLine($"{type.Name} « {atkef.AttackerType.Name}: {atkef.Value}");

				PokemonType attackerType = PokemonUtils.GetPokemonTypeByName(atkef.AttackerType.Name);

				effectivenessMap.Merge(attackerType, atkef.Value == 0 ? -1 : atkef.Value, DictionaryMergeOperation.Sum);
			}

			return effectivenessMap;
		}

		public Dictionary<Effectiveness, string> ToString(bool replaceEmojis)
		{
			StringBuilder weakToSb = new StringBuilder();
			StringBuilder resistsToSb = new StringBuilder();
			StringBuilder immuneToSb = new StringBuilder();

			foreach (KeyValuePair<PokemonType, double> kv in this.EffectivenessMap.OrderBy(kv => kv.Value))
			{
				Debug.WriteLine($"kv.value: {kv.Value} ({kv.Key})");
				switch (kv.Value)
				{
					// Immune
					case -1 or -2: _ = immuneToSb.Append(PokemonUtils.GetTypeDisplay(kv.Key, replaceEmojis), "\n"); break;
					// Resists
					case 0.25: _ = resistsToSb.Append($"**{PokemonUtils.GetTypeDisplay(kv.Key, replaceEmojis)} (x0.25)**", "\n"); break;
					case 0.5: _ = resistsToSb.Append($"{PokemonUtils.GetTypeDisplay(kv.Key, replaceEmojis)} (x0.5)", "\n"); break;
					// Weak
					case 2: _ = weakToSb.Append($"{PokemonUtils.GetTypeDisplay(kv.Key, replaceEmojis)} (x2)", "\n"); break;
					case 4: _ = weakToSb.Preappend($"**{PokemonUtils.GetTypeDisplay(kv.Key, replaceEmojis)} (x4)**", "\n"); break;
				}
			}

			return new Dictionary<Effectiveness, string>()
				{
					{ Effectiveness.Weak, weakToSb.ToString() },
					{ Effectiveness.Resists, resistsToSb.ToString() },
					{ Effectiveness.Immune, immuneToSb.ToString() },
				};
		}
	}

	public enum Effectiveness
	{
		Weak,
		Immune,
		Resists,
		Normal,
	}
}
