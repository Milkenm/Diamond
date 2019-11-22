#region Usings
using System;
using System.Reflection;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

using static DiamondGui.EventsModule;
using static DiamondGui.Functions;
using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
	internal static partial class Discord
	{
		internal static async Task DiscordMain()
		{
			try
			{
				// Initialize
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

				// Logs
				Client.Log += DiscordLog;

				// Status & Game
				SetDiscordGame();
				SetDiscordStatus();

				// Message Event
				Client.MessageReceived += MessageReceived;

				// Command Handler
				Client.MessageReceived += DiscordCommandHandler;
				await Command.AddModulesAsync(Assembly.GetEntryAssembly(), null);

				// Login
				await Client.LoginAsync(TokenType.Bot, OptionsForm.textBox_token.Text);
				await Client.StartAsync();


				// Lock Task
				await Task.Delay(-1);
			}
			catch (Exception _Exception)
			{
				MainForm.Invoke(new Action(() =>
				{
					MainForm.button_start.PerformClick();
				}));
				ShowException(_Exception);
			}
		}
	}
}
