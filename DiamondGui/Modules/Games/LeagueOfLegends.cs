#region Usings
using System;
using System.Threading.Tasks;

using Discord.Commands;

using static DiamondGui.Functions;
#endregion Usings



namespace DiamondGui.Commands
{
	public class LeagueOfLegends : ModuleBase<SocketCommandContext>
	{
		[Command("summoner"), Alias("sum"), Summary("Gets information about a League of Legends summoner.")]
		public async Task CMD_Summoner()
		{
            try
            {

            }
            catch (Exception _Exception)
            {
                ShowException(_Exception, "DiamondGui.Commands.LeagueOfLegends.CMD_Summoner()");
            }
		}
	}
}