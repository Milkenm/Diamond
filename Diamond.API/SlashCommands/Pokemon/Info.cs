using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Diamond.API.APIs.Pokemon;
using Diamond.API.Attributes;
using Diamond.API.Helpers;
using Diamond.API.Util;
using Diamond.Data;
using Diamond.Data.Models.Pokemons;

using Discord.Interactions;

using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Diamond.API.SlashCommands.Pokemon
{
	public partial class Pokemon
	{
		[DSlashCommand("info", "View info about a pokémon.")]
		public async Task PokemonSearchCommandAsync(
			[Summary("name", "The name of the pokémon.")] string pokemonName,
			[Summary("replace-emojis", "Replaces the type emojis with a text in case you a have trouble reading.")] bool replaceEmojis = false,
			[ShowEveryone] bool showEveryone = false
		)
		{
			await this.DeferAsync(!showEveryone);

			await this.SendInfoEmbedAsync(pokemonName, replaceEmojis);
		}

		[ComponentInteraction($"{BUTTON_POKEMON_VIEW_INFO}:*,*", true)]
		public async Task ButtonViewStatsHandler(string pokemonName, bool replaceEmojis)
		{
			await this.DeferAsync();

			await this.SendInfoEmbedAsync(pokemonName, replaceEmojis);
		}

		private async Task SendInfoEmbedAsync(string pokemonName, bool replaceEmojis)
		{
			using DiamondContext db = new DiamondContext();

			DbPokemon pokemon = (await this._pokemonApi.SearchItemAsync(pokemonName))[0].Item;
			pokemon = db.Pokemons.Where(p => p.Id == pokemon.Id)
				.Include(p => p.TypesList)
				.Include(p => p.AbilitiesList)
				.Include(p => p.EvolutionsList)
				.Include(p => p.GenerationsList)
				.Include(p => p.FormatsList)
				.First();

			PokemonAPIHelpers.LoadPokemonExtraInfo(ref pokemon);

			Dictionary<PokemonType, double> effectivenessMap = new Dictionary<PokemonType, double>();
			StringBuilder typesSb = new StringBuilder();
			foreach (DbPokemonType type in pokemon.TypesList)
			{
				PokemonType pokeType = PokemonAPIHelpers.GetPokemonTypeByName(type.Name);

				// Get all counters
				foreach (DbPokemonAttackEffectiveness atkef in db.PokemonAttackEffectivenesses.Include(af => af.AttackerType).Where(af => af.TargetType == type))
				{
					if (atkef.Value == 1) continue;

					PokemonType attackerType = PokemonAPIHelpers.GetPokemonTypeByName(atkef.AttackerType.Name);
					if (effectivenessMap.ContainsKey(attackerType))
					{
						if (atkef.Value == 0.5)
						{
							effectivenessMap[attackerType] -= 1;
						}
						else
						{
							effectivenessMap[attackerType] += atkef.Value;
						}
					}
					else
					{
						if (atkef.Value == 0.5)
						{
							effectivenessMap.Add(attackerType, -1);
						}
						else
						{
							effectivenessMap.Add(attackerType, atkef.Value);
						}
					}
				}

				_ = typesSb.Append($"{PokemonAPIHelpers.GetTypeDisplay(pokeType, replaceEmojis)}", " ");
			}
			StringBuilder weakToSb = new StringBuilder();
			StringBuilder immuneToSb = new StringBuilder();
			StringBuilder resistsToSb = new StringBuilder();
			foreach (KeyValuePair<PokemonType, double> effectiveness in effectivenessMap.OrderBy(kv => kv.Value))
			{
				if (effectiveness.Value > 1)
				{
					if (effectiveness.Value == 2)
					{
						_ = weakToSb.Append($"{PokemonAPIHelpers.GetTypeDisplay(effectiveness.Key, replaceEmojis)} (x2)", "\n");
					}
					else if (effectiveness.Value == 3)
					{
						_ = weakToSb.Append($"**{PokemonAPIHelpers.GetTypeDisplay(effectiveness.Key, replaceEmojis)} (x4)**", "\n");
					}
				}
				else if (effectiveness.Value == 0)
				{
					_ = immuneToSb.Append(PokemonAPIHelpers.GetTypeDisplay(effectiveness.Key, replaceEmojis), "\n");
				}
				else if (effectiveness.Value < 0)
				{
					if (effectiveness.Value == -1)
					{
						_ = resistsToSb.Append($"{PokemonAPIHelpers.GetTypeDisplay(effectiveness.Key, replaceEmojis)} (x0.5)", "\n");
					}
					else if (effectiveness.Value == -2)
					{
						_ = resistsToSb.Append($"**{PokemonAPIHelpers.GetTypeDisplay(effectiveness.Key, replaceEmojis)} (x0.25)**", "\n");
					}
				}
			}

			// Abilities
			StringBuilder abilitiesSb = new StringBuilder();
			foreach (DbPokemonPassive ability in pokemon.AbilitiesList)
			{
				_ = abilitiesSb.Append($"**{ability.Name}**: {ability.Description}", "\n");
			}

			// Evolutions
			int evolutions = 0;
			StringBuilder evolutionsSb = new StringBuilder();
			foreach (DbPokemon evolution in pokemon.EvolutionsList)
			{
				evolutions++;
				_ = evolutionsSb.Append($"[{evolution.Name}]({PokemonAPIHelpers.GetSmogonPokemonUrl(evolution.Name)})", "\n");
			}

			DefaultEmbed embed = new DefaultEmbed("Pokédex - Info", "🖥️", this.Context)
			{
				Title = $"{pokemon.Name} #{pokemon.DexNumber}",
				Description = typesSb.ToString(),
				/*ThumbnailUrl = PokemonAPIHelpers.GetPokemonGif(pokemon.Name),
				ImageUrl = PokemonAPIHelpers.GetPokemonImage((int)pokemon.DexNumber),*/
			};

			// Generations
			StringBuilder generationsSb = new StringBuilder();
			foreach (DbPokemonGeneration generation in pokemon.GenerationsList)
			{
				_ = generationsSb.Append($"[{generation.Name}]({PokemonAPIHelpers.GetGenerationUrl(generation.Abbreviation)})", "\n");
			}

			// Formats
			StringBuilder formatsSb = new StringBuilder();
			foreach (DbPokemonFormat format in pokemon.FormatsList)
			{
				_ = formatsSb.Append($"[{format.Name}]({PokemonAPIHelpers.GetFormatUrl(format.Abbreviation)})", "\n");
			}

			// First row
			_ = embed.AddField("⚔️ **__Stats__**", $"HP: {pokemon.HealthPoints}\nAttack: {pokemon.Attack}\nDefense: {pokemon.Defense}\nSp. Atk: {pokemon.SpecialAttack}\nSp. Def: {pokemon.SpecialDefense}\nSpeed: {pokemon.SpecialDefense}", true);
			_ = embed.AddField($"⬆️ **__{Utils.Plural("Evolution", "", "s", evolutions)}__**", evolutionsSb.ToStringOrDefault("None"), true);
			_ = embed.AddEmptyField(true);
			// Second row
			_ = embed.AddField("😨 **__Weak to__**", weakToSb.ToStringOrDefault("None"), true);
			_ = embed.AddField("💪 **__Resists to__**", resistsToSb.ToStringOrDefault("None"), true);
			_ = embed.AddField("🛡️ **__Immune to__**", immuneToSb.ToStringOrDefault("None"), true);
			// Third row
			_ = embed.AddField($"📆 **__{Utils.Plural("Generation", "", "s", pokemon.GenerationsList)}__**", generationsSb.ToStringOrDefault("None"), true);
			_ = embed.AddField($"🏆 **__{Utils.Plural("Format", "", "s", pokemon.FormatsList)}__**", formatsSb.ToStringOrDefault("None"), true);
			_ = embed.AddEmptyField(true);
			// Fourth row
			_ = embed.AddField($"✨ **__{Utils.Plural("Abilit", "y", "ies", pokemon.AbilitiesList)}__**", abilitiesSb.ToString());

			_ = await embed.SendAsync(await this.GetEmbedButtonsAsync(pokemonName, PokemonEmbed.Info, replaceEmojis));
		}

		private static string GetAttackStatString(int value, Stat stat)
		{
			return value == 0
				? "—"
				: stat switch
				{
					Stat.Accuracy => $"{value}%",
					Stat.PowerPoints => $"{value} PP",
					_ => value.ToString()
				};
		}

		private enum Stat
		{
			Power,
			Accuracy,
			PowerPoints,
		}
	}
}
