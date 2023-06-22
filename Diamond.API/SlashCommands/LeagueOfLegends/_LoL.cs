using Diamond.API.APIs.LeagueOfLegends;
using Discord.Interactions;

namespace Diamond.API.SlashCommands.LeagueOfLegends
{
    [Group("lol", "League of Legends related commands.")]
	public partial class LeagueOfLegends : InteractionModuleBase<SocketInteractionContext>
	{
		private readonly LeagueOfLegendsDataDragonAPI _dataDragonApi;

		public LeagueOfLegends(LeagueOfLegendsDataDragonAPI leagueOfLegendsAPI)
		{
			this._dataDragonApi = leagueOfLegendsAPI;
		}
	}
}