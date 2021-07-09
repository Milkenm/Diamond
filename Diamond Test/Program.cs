using Diamond.Brainz;

using System;

namespace Diamond_Test
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			Main bot = new Main();
			bot.DCore.Start().GetAwaiter().GetResult();
			bot.DCore.SetGame(Discord.ActivityType.Playing, "with Miłkenm 💦").GetAwaiter().GetResult();
			Console.WriteLine("Running...\n\n\nPress <ENTER> to stop.");
			Console.ReadLine();
			bot.DCore.Stop().GetAwaiter().GetResult();
		}
	}
}
