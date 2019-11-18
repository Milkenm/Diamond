#region Usings
using System;
using System.Reflection;
using System.Threading.Tasks;

using static DiamondGui.Functions;
using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
	internal static partial class Discord
	{
		internal static async Task DiscordInstallCommands()
		{
			try
			{
				Client.MessageReceived += DiscordCommandHandler;

				await Command.AddModulesAsync(Assembly.GetEntryAssembly(), null);
			}
			catch (Exception _Exception) { ShowException(_Exception); }
		}
	}
}
