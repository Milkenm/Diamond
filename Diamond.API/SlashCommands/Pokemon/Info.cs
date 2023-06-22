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

			Dictionary<PokemonType, double> effectivenessMap = new Dictionary<PokemonType, double>();
			StringBuilder typesSb = new StringBuilder();
			foreach (string type in pokemon.TypesList.Split(",").ToList())
			{
				DbPokemonType dbType = db.PokemonTypes.Where(t => t.Name == type).FirstOrDefault();
				PokemonType pokeType = PokemonAPIHelpers.GetPokemonTypeByName(type);

				// Get all counters
				foreach (DbPokemonAttackEffectiveness atkef in db.PokemonAttackEffectivenesses.Include(af => af.AttackerType).Where(af => af.TargetType == dbType))
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

				_ = typesSb.Append($"{PokemonAPIHelpers.GetTypeDisplay(type, replaceEmojis)}", " ");
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
			List<string> abilitiesList = pokemon.AbilitiesList.Split(",").ToList();
			StringBuilder abilitiesSb = new StringBuilder();
			foreach (string ability in abilitiesList)
			{
				DbPokemonPassive passive = db.PokemonPassives.Where(p => p.Name == ability).FirstOrDefault();
				_ = abilitiesSb.Append($"**{passive.Name}**: {passive.Description}", "\n");
			}

			// Evolutions
			List<string> evolutionsList = pokemon.EvolutionsList.Split(",").ToList();
			StringBuilder evolutionsSb = new StringBuilder();
			foreach (string evolution in evolutionsList)
			{
				_ = evolutionsSb.Append($"[{evolution}]({PokemonAPIHelpers.GetSmogonUrl(evolution)})", "\n");
			}

			DefaultEmbed embed = new DefaultEmbed("Pokédex", "🖥️", this.Context)
			{
				Title = $"{pokemon.Name} #{pokemon.DexNumber}",
				Description = typesSb.ToString(),
				/*ThumbnailUrl = PokemonAPIHelpers.GetPokemonGif(pokemon.Name),
				ImageUrl = PokemonAPIHelpers.GetPokemonImage((int)pokemon.DexNumber),*/
			};

			// Generations
			List<string> generationsList = pokemon.GenerationsList.Split(",").ToList();
			StringBuilder generationsSb = new StringBuilder();
			foreach (string generation in generationsList)
			{
				string fullGeneration = PokemonAPIHelpers.GetGenerationName(generation);
				_ = generationsSb.Append($"[{fullGeneration}]({PokemonAPIHelpers.GetGenerationUrl(generation)})", "\n");
			}

			// Formats
			List<string> formatsList = pokemon.FormatsList.Split(",").ToList();
			StringBuilder formatsSb = new StringBuilder();
			foreach (string format in formatsList)
			{
				string fullFormat = PokemonAPIHelpers.GetFormatName(format);
				_ = formatsSb.Append($"[{fullFormat}]({PokemonAPIHelpers.GetFormatUrl(format)})", "\n");
			}

			// First row
			_ = embed.AddField("⚔️ **__Stats__**", $"HP: {pokemon.HealthPoints}\nAttack: {pokemon.Attack}\nDefense: {pokemon.Defense}\nSp. Atk: {pokemon.SpecialAttack}\nSp. Def: {pokemon.SpecialDefense}\nSpeed: {pokemon.SpecialDefense}", true);
			_ = embed.AddField($"⬆️ **__{Utils.Plural("Evolution", "", "s", evolutionsList)}__**", evolutionsSb.ToStringOrDefault("None"), true);
			_ = embed.AddEmptyField(true);
			// Second row
			_ = embed.AddField("😨 **__Weak to__**", weakToSb.ToStringOrDefault("None"), true);
			_ = embed.AddField("💪 **__Resists to__**", resistsToSb.ToStringOrDefault("None"), true);
			_ = embed.AddField("🛡️ **__Immune to__**", immuneToSb.ToStringOrDefault("None"), true);
			// Third row
			_ = embed.AddField($"📆 **__{Utils.Plural("Generation", "", "s", generationsList)}__**", generationsSb.ToStringOrDefault("None"), true);
			_ = embed.AddField($"🏆 **__{Utils.Plural("Format", "", "s", formatsList)}__**", formatsSb.ToStringOrDefault("None"), true);
			_ = embed.AddEmptyField(true);
			// Fourth row
			_ = embed.AddField($"✨ **__{Utils.Plural("Abilit", "y", "ies", abilitiesList)}__**", abilitiesSb.ToString());

			_ = await embed.SendAsync(await this.GetEmbedButtonsAsync(pokemonName, PokemonEmbed.Info, replaceEmojis));
		}

		private static string GetAttackStatString(int value, Stat stat)
		{
			return value == 0
				? "—"
				: stat switch
				{
					Stat.Accuracy => $"{value}%",
					Stat.PowerPoints => $"{value}PP",
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
