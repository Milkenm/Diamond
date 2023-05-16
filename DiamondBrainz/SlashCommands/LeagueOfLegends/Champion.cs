using System.Collections.Generic;
using System.Threading.Tasks;

using Diamond.API.Schemes.LolDataDragon;

using Discord;
using Discord.Interactions;

using static Diamond.API.Utils;

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

		// Search champ
		List<SearchMatchInfo<LolChampion>> search = await Utils.Search(this._dataDragonApi.DdragonChampionData.ChampionsList, championName);
		if (search.Count == 0)
		{
			embed.Title = "Champion not found";
			embed.Description = $"A champion named '{championName}' could not be found.";
			await embed.SendAsync();
			return;
		}
		LolChampion champ = search[0].Item;

		// Calcs
		int maxHealth = (int)this.CalculateMaxLevelStat(champ.Stats.Health, champ.Stats.HealthPerLevel);
		int maxMana = (int)this.CalculateMaxLevelStat(champ.Stats.Mana, champ.Stats.ManaPerLevel);
		int maxArmor = (int)this.CalculateMaxLevelStat(champ.Stats.Armor, champ.Stats.ArmorPerLevel);
		double maxHealthRegeneration = this.CalculateMaxLevelStat(champ.Stats.HealthRegeneration, champ.Stats.HealthRegenerationPerLevel, false);
		double maxManaRegeneration = this.CalculateMaxLevelStat(champ.Stats.ManaRegeneration, champ.Stats.ManaRegenerationPerLevel, false);
		int maxMagicResistance = (int)this.CalculateMaxLevelStat(champ.Stats.MagicResist, champ.Stats.MagicResistPerLevel);
		int maxAttackDamage = (int)this.CalculateMaxLevelStat(champ.Stats.AttackDamage, champ.Stats.AttackDamagePerLevel);
		double maxAttackSpeed = this.CalculateMaxLevelStat(champ.Stats.AttackSpeed, champ.Stats.AttackSpeedPerLevel, false);

		// Main embed info
		embed.Title = champ.ChampionName;
		embed.Description = champ.Lore;
		embed.ThumbnailUrl = this._dataDragonApi.GetChampionSquareImageUrl(champ);
		embed.ImageUrl = this._dataDragonApi.GetChampionSplashImageUrl(champ);
		// First row
		embed.AddField("<:lol_health_icon:1106763686368006164> __Health__", $"{champ.Stats.Health} - {maxHealth}\n*+{champ.Stats.HealthPerLevel} / level*", true);
		embed.AddField("<:lol_mana_icon:1106763684275048528> __Mana__", $"{champ.Stats.Mana} - {maxMana}\n*+{champ.Stats.ManaPerLevel} / level*", true);
		embed.AddField("<:lol_armor_icon:1106763680055562240> __Armor__", $"{champ.Stats.Armor} - {maxArmor}\n*+{champ.Stats.ArmorPerLevel} / level*", true);
		// Second row
		embed.AddField("<:lol_health_regeneration_icon:1106763683046101062> __Health Regeneration__", $"{champ.Stats.HealthRegeneration} - {maxHealthRegeneration}\n*+{champ.Stats.HealthRegenerationPerLevel} / level*", true);
		embed.AddField("<:lol_mana_regeneration_icon:1106763681519386655> __Mana Regeneration__", $"{champ.Stats.ManaRegeneration} - {maxManaRegeneration}\n*+{champ.Stats.ManaRegenerationPerLevel} / level*", true);
		embed.AddField("<:lol_magic_resist_icon:1106763678822432809> __Magic Resist__", $"{champ.Stats.MagicResist} - {maxMagicResistance}\n*+{champ.Stats.MagicResistPerLevel} / level*", true);
		// Third row
		embed.AddField("<:lol_attack_damage_icon:1106763687550779433> __Attack Damage__", $"{champ.Stats.AttackDamage} - {maxAttackDamage}\n*+{champ.Stats.AttackDamagePerLevel} / level*", true);
		embed.AddField("<:lol_attack_speed_icon:1106764595001045042> __Attack Speed__", $"{champ.Stats.AttackSpeed} - {maxAttackSpeed}\n*+{champ.Stats.AttackSpeedPerLevel} / level*", true);
		embed.AddField("<:lol_movement_speed_icon:1106763688339312702> __Movement Speed__", champ.Stats.MovementSpeed, true);
		// Tips
		if (champ.AllyTipsList.Count > 0)
		{
			embed.AddField($"__Tips for playing as {champ.ChampionName}__", $"🔹 {string.Join("\n🔹 ", champ.AllyTipsList)}");
		}
		if (champ.EnemyTipsList.Count > 0)
		{
			embed.AddField($"__Tips for playing against {champ.ChampionName} __", $"🔸 {string.Join("\n🔸 ", champ.EnemyTipsList)}");
		}
		// Fandom button
		ComponentBuilder component = new ComponentBuilder()
			.WithButton("View on Fandom", style: ButtonStyle.Link, emote: Emoji.Parse("📖"), url: this._dataDragonApi.GetChampionFandomWikiPageUrl(champ).Replace(" ", "_"));

		await embed.SendAsync(component: component.Build());
	}

	private double CalculateMaxLevelStat(double baseStat, double perLevelStatIncrease, bool round = true) => System.Math.Round(baseStat + (perLevelStatIncrease * 17), (round ? 0 : 1));
}
