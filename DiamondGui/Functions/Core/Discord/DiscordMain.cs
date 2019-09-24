#region Usings
using System;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

using static DiamondGui.Functions;
using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
	internal static partial class Core
	{
		internal static async Task DiscordMain()
		{
			try
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



				Client = new DiscordSocketClient();

				Client.Log += DiscordLog;

				// Remember to keep token private or to read it from an 
				// external source! In this case, we are reading the token 
				// from an environment variable. If you do not know how to set-up
				// environment variables, you may find more information on the 
				// Internet or by using other methods such as reading from 
				// a configuration.
				await Client.LoginAsync(TokenType.Bot, MainForm.textBox_token.Text);
				await Client.StartAsync();

				// Block this task until the program is closed.
				await Task.Delay(-1);
			}
			catch (Exception _Exception)
			{
				ShowException(_Exception, "Core.DiscordMain()");
			}
		}
	}
}
