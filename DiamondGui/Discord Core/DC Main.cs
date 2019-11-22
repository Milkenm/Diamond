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
	internal static partial class DiscordCore
	{
		internal static async Task DiscordCoreMain()
		{
			try
			{
				// Initialize
				await DiscordCoreInitialize();

				// Logs
				Client.Log += DiscordCoreLogger;

				// Status & Game
				SetDiscordGame();
				SetDiscordStatus();

				// Message Event
				Client.MessageReceived += MessageReceived;

				// Command Handler
				Client.MessageReceived += DiscordCoreCommandHandler;
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
