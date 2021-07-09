
using Diamond.Brainz;

using Discord;

using System;
using System.Threading.Tasks;

namespace Diamond_Test
{
	internal class Program
	{
		public static void Main()
		{
			MainAsync().GetAwaiter().GetResult();
		}

		public static async Task MainAsync()
		{
			Bot bot = new Bot();
			bot.Client.Log += Client_Log;

			await bot.Start();
			await bot.SetGame(ActivityType.Playing, "with Miłkenm 💦");

			await Task.Delay(-1);
		}

		private static Task Client_Log(LogMessage arg)
		{
			Console.WriteLine(arg.Message);
			return Task.CompletedTask;
		}
	}
}
