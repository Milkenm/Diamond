using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Diamond.API.APIs;
using Diamond.API.Attributes;
using Diamond.API.Data;
using Diamond.API.Util;

using Discord;
using Discord.Interactions;

using Microsoft.EntityFrameworkCore;

namespace Diamond.API.SlashCommands.Pokemon
{
	public partial class Pokemon
	{
		[DSlashCommand("search", "Search for a pokémon.")]
		public async Task PokemonSearchCommandAsync(
			[Summary("name", "The name of the pokémon to search for.")] string name,
			[ShowEveryone] bool showEveryone = false
		)
		{
			await this.DeferAsync(!showEveryone);

			using DiamondContext db = new DiamondContext();

			DbPokemon pokemon = this._pokeApi.SearchPokemon(name);

			List<string> typesList = pokemon.TypesList.Split(",").ToList();
			StringBuilder typesSb = new StringBuilder();
			Dictionary<PokemonType, double> effectivenessMap = new Dictionary<PokemonType, double>();
			foreach (string type in typesList)
			{
				DbPokemonType dbType = db.PokemonTypes.Where(t => t.Name == type).FirstOrDefault();

				// Get all counters
				foreach (DbPokemonAttackEffectives atkef in db.PokemonAttackEffectives.Include(af => af.AttackerType).Where(af => af.TargetType == dbType))
				{
					// Ignore "neutral" attacks
					if (atkef.Value <= 1) continue;

					PokemonType attackerType = PokemonAPI.GetPokemonTypeByName(atkef.AttackerType.Name);

					PokemonType pokeType = PokemonAPI.GetPokemonTypeByName(type);
					if (effectivenessMap.ContainsKey(pokeType))
					{
						effectivenessMap[attackerType] += atkef.Value;
					}
					else
					{
						effectivenessMap.Add(attackerType, atkef.Value);
					}
				}

				_ = typesSb.Append($"{this._pokeApi.GetTypeEmoji(type)}", " ");
			}
			StringBuilder countersSb = new StringBuilder();
			foreach (KeyValuePair<PokemonType, double> counter in effectivenessMap)
			{
				if (counter.Value == 1) continue;

				_ = countersSb.Append(counter.Key.ToString(), "\n");
			}

			List<string> abilitiesList = pokemon.AbilitiesList.Split(",").ToList();
			StringBuilder abilitiesSb = new StringBuilder();
			foreach (string ability in abilitiesList)
			{
				PokemonPassive passive = db.PokemonPassives.Where(p => p.Name == ability).FirstOrDefault();
				_ = abilitiesSb.Append($"**{passive.Name}**: {passive.Description}", "\n");
			}

			DefaultEmbed embed = new DefaultEmbed("Pokémon Search", "🥎", this.Context)
			{
				Title = pokemon.Name,
				Description = typesSb.ToString(),
				ThumbnailUrl = PokemonAPI.GetPokemonGif(pokemon.Name),
				ImageUrl = PokemonAPI.GetPokemonImage((int)pokemon.DexNumber),
			};
			_ = embed.AddField("Stats", $"HP: {pokemon.HealthPoints}\nAttack: {pokemon.Attack}\nDefense: {pokemon.Defense}\nSp. Atk: {pokemon.SpecialAttack}\nSp. Def: {pokemon.SpecialDefense}\nSpeed: {pokemon.SpecialDefense}", true);
			_ = embed.AddField("Counters", countersSb.ToString(), true);
			_ = embed.AddField($"Abilit{(abilitiesList.Count != 1 ? "ies" : "y")}", abilitiesSb.ToString());
			MessageComponent components = new ComponentBuilder()
				.WithButton("View on Pokédex", style: ButtonStyle.Link, url: PokemonAPI.GetPokedexLink((int)pokemon.DexNumber))
				.WithButton("View on Smogon", style: ButtonStyle.Link, url: PokemonAPI.GetSmogonLink(pokemon.Name))
				.Build();
			embed.Component = components;

			_ = await embed.SendAsync();
		}
	}
}
