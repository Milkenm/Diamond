#region Usings
using System.Threading.Tasks;

using Discord.Commands;
#endregion Usings



namespace DiamondGui.Commands
{
	internal class League_of_Legends : ModuleBase<SocketCommandContext>
	{
		[Command("summoner"), Alias("sum"), Summary("Gets information about a League of Legends summoner.")]
		internal async Task CMD_Summoner()
		{
			
		}
	}
}