#region Usings
using System.Threading.Tasks;
using System.Windows.Forms;

using Discord.Commands;
using Discord.WebSocket;

using static DiamondGui.Functions;
using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
	internal static partial class DiscordCore
	{
		internal static async Task DiscordCoreInitialize()
		{
			Client = new DiscordSocketClient(new DiscordSocketConfig
			{
				LogLevel = GetLogLevel(),
			});

			Command = new CommandService(new CommandServiceConfig
			{
				CaseSensitiveCommands = false,
				DefaultRunMode = RunMode.Async,
				LogLevel = GetLogLevel(),
			});
		}
	}
}