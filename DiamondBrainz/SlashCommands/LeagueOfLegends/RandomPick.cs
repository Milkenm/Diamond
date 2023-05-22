using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.Schemes.LolDataDragon;

using Discord.Interactions;

using static Diamond.API.APIs.LeagueOfLegendsDataDragonAPI;

namespace Diamond.API.SlashCommands.LeagueOfLegends;
public partial class LeagueOfLegends
{
	[DSlashCommand("random-pick", "Pick a random champ and/or role.")]
	public async Task RandomizeCommandAsync(
		[Summary("champion-class", "Selects a champion based on the selected class.")] ChampionClass? championClass = null,
		[ShowEveryone] bool showEveryone = false
	)
	{
		await this.DeferAsync(!showEveryone);

		List<LolChampion> validChampions = this._dataDragonApi.DdragonChampionData.ChampionsList.Values.ToList();
		if (championClass != null)
		{
			validChampions.RemoveAll(champ => champ.Tags[0] != championClass.ToString());
		}

		int randomChampionIndex = new Random().Next(0, validChampions.Count);
		LolChampion randomChampion = validChampions.ElementAt(randomChampionIndex);

		DefaultEmbed embed = new DefaultEmbed("LoL Random Pick", "🎲", this.Context.Interaction)
		{
			Title = randomChampion.ChampionName,
			ImageUrl = this._dataDragonApi.GetChampionSplashImageUrl(randomChampion),
		};

		await embed.SendAsync();
	}
}
