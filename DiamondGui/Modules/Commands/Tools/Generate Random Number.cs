using Discord.Commands;

using System;
using System.Threading.Tasks;

using static DiamondGui.Static;

namespace DiamondGui
{
	public partial class CommandsModule : ModuleBase<SocketCommandContext>
	{
		[Command("random number"), Alias("rnum", "rand num", "rand number", "random num", "rn", "random", "rnd", "rand"), Summary("Generates a random number between a min number and max.")]
		public async Task GenerateRandomNumber(int min, int max)
		{
			settings.CommandsUsed++;

			await ReplyAsync("Random number: **" + new Random().Next(min, max) + "**").ConfigureAwait(false);
		}
	}
}