﻿#region Using
using System.Threading.Tasks;

using Discord.Commands;
#endregion



namespace Diamond.Commands
{
	public class League_of_Legends : ModuleBase<SocketCommandContext>
	{
		[Command("summoner"), Alias("sum"), Summary("Gets information about a League of Legends summoner.")]
		public async Task CMD_Summoner()
		{
			
		}
	}
}