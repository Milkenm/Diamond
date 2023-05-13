using Diamond.API.APIs;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.LeagueOfLegends;

[Group("lol", "League of Legends related commands.")]
public partial class LeagueOfLegends : InteractionModuleBase<SocketInteractionContext>
{
	private readonly LeagueOfLegendsAPI _leagueOfLegendsApi;

	public LeagueOfLegends(LeagueOfLegendsAPI leagueOfLegendsAPI)
	{
		this._leagueOfLegendsApi = leagueOfLegendsAPI;
	}
}
