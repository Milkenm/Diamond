#region Using
using System;
using System.Threading.Tasks;

using Discord.Commands;

using static DiamondGui.Static;
#endregion



namespace DiamondGui
{
	public partial class CommandsModule : ModuleBase<SocketCommandContext>
	{
		[Command("random number"), Alias("rnum", "rand num", "rand number", "random num", "rn", "random", "rnd", "rand"), Summary("Generates a random number between a min number and max.")]
		public async Task GenerateRandomNumber(int min, int max)
		{
			Settings.CommandsUsed++;

			await ReplyAsync("Random number: **" + new Random().Next(min, max) + "**");
		}
	}
}