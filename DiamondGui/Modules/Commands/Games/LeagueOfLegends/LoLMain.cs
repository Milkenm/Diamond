#region Using
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using Discord.Commands;

using static DiamondGui.Functions;
#endregion



namespace DiamondGui
{
	public partial class CommandsModule : ModuleBase<SocketCommandContext>
	{
		[Command("lol main"), Alias("l main"), Summary("Gets the main champion of a summoner.")]
		public async Task LolMain(string summoner, [Optional] Regions region)
		{
			try
			{

			}
			catch (Exception _Exception) { ShowException(_Exception); }
		}

		public enum Regions
		{
			EUW,
		}
	}
}