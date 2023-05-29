using System.Threading.Tasks;

using Diamond.API.Attributes;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.LeagueOfLegends
{
	public partial class LeagueOfLegends
	{
		[DSlashCommand("summoner", "View a summoner's profile.")]
		public async Task SummonerCommandAsync(
			[Summary("summoner-name", "The name of the summoner to search for.")] string summonerName,
			[Summary("region", "The region the summoner is playing at.")] Region region,
			[ShowEveryone] bool showEveryone = false
		)
		{

		}

		public enum Region
		{
			[ChoiceDisplay("Brazil (BR)")] BR,
			[ChoiceDisplay("Europe Nordic & East (EUNE)")] EUNE,
			[ChoiceDisplay("Europe West (EUW)")] EUW,
			[ChoiceDisplay("Latin America North (LAN)")] LAN,
			[ChoiceDisplay("Latin America South (LAS)")] LAS,
			[ChoiceDisplay("North America (NA)")] NA,
			[ChoiceDisplay("Oceania (OCE)")] OCE,
			[ChoiceDisplay("Russia (RU)")] RU,
			[ChoiceDisplay("Turkey (TR)")] TR,
			[ChoiceDisplay("Japan (JP)")] JP,
			[ChoiceDisplay("Republic of Korea (KR)")] KR,
			[ChoiceDisplay("The Philippines (PH)")] PH,
			[ChoiceDisplay("Singapore, Malaysia, & Indonesia (SG)")] SG,
			[ChoiceDisplay("Taiwan, Hong Kong, and Macao (TW)")] TW,
			[ChoiceDisplay("Thailand (TH)")] TH,
			[ChoiceDisplay("Vietnam (VN)")] VN,
			[ChoiceDisplay("Public Beta Environment (PBE)")] PBE,
		}
	}
}