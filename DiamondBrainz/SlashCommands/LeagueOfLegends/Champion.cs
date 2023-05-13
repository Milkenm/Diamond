using System.Linq;
using System.Threading.Tasks;

using Diamond.API.Schems.LolDataDragon;

using Discord;
using Discord.Interactions;

namespace Diamond.API.SlashCommands.LeagueOfLegends;
public partial class LeagueOfLegends
{
	[SlashCommand("champion", "View info about a champion.")]
	public async Task ChampionCommandAsync(
		[Summary("champion-name", "The name of the champion to view.")] string championName,
		[Summary("show-everyone", "Show the command output to everyone.")] bool showEveryone = false
	)
	{
		await this.DeferAsync(!showEveryone);
		DefaultEmbed embed = new DefaultEmbed("LoL Champion", "🧙", this.Context.Interaction);

		LolChampion champion = this._leagueOfLegendsApi.DdragonChampionData.ChampionsList.Values.Where(champ => champ.ChampionName.ToLower() == championName.ToLower()).FirstOrDefault(defaultValue: null);

		if (champion == null)
		{
			embed.Title = "Champion not found";
			embed.Description = $"A champion named '{championName}' could not be found.";
			await embed.SendAsync();
			return;
		}

		// Main embed info
		embed.Title = champion.ChampionName;
		embed.Description = champion.Lore;
		embed.ThumbnailUrl = this._leagueOfLegendsApi.GetChampionSquareImageUrl(champion);
		embed.ImageUrl = this._leagueOfLegendsApi.GetChampionSplashImageUrl(champion);
		// First row
		embed.AddField("<:lol_health_icon:1106763686368006164> __Health__", $"{champion.Stats.Health} *+ {champion.Stats.HealthPerLevel} / level*", true);
		embed.AddField("<:lol_mana_icon:1106763684275048528> __Mana__", $"{champion.Stats.Mana} *+ {champion.Stats.ManaPerLevel} / level*", true);
		embed.AddField("<:lol_armor_icon:1106763680055562240> __Armor__", $"{champion.Stats.Armor} *+ {champion.Stats.ArmorPerLevel} / level*", true);
		// Second row
		embed.AddField("<:lol_health_regeneration_icon:1106763683046101062> __Health Regeneration__", $"{champion.Stats.HealthRegeneration} *+ {champion.Stats.HealthRegenerationPerLevel} / level*", true);
		embed.AddField("<:lol_mana_regeneration_icon:1106763681519386655> __Mana Regeneration__", $"{champion.Stats.ManaRegeneration} *+ {champion.Stats.ManaRegenerationPerLevel} / level*", true);
		embed.AddField("<:lol_magic_resist_icon:1106763678822432809> __Magic Resist__", $"{champion.Stats.MagicResist} *+ {champion.Stats.MagicResistPerLevel} / level*", true);
		// Third row
		embed.AddField("<:lol_attack_damage_icon:1106763687550779433> __Attack Damage__", $"{champion.Stats.AttackDamage} *+ {champion.Stats.AttackDamagePerLevel} / level*", true);
		embed.AddField("<:lol_attack_speed_icon:1106764595001045042> __Attack Speed__", $"{champion.Stats.AttackSpeed} *+ {champion.Stats.AttackSpeedPerLevel} / level*", true);
		embed.AddField("<:lol_movement_speed_icon:1106763688339312702> __Movement Speed__", champion.Stats.MovementSpeed, true);
		// Fandom button
		ComponentBuilder component = new ComponentBuilder()
			.WithButton("View on Fandom", style: ButtonStyle.Link, emote: Emoji.Parse("📖"), url: this._leagueOfLegendsApi.GetChampionFandomWikiPageUrl(champion));

		await embed.SendAsync(component: component.Build());
	}
}
