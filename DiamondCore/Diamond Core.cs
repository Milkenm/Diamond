using Discord;
using Discord.Commands;
using Discord.WebSocket;

using System;
using System.Reflection;
using System.Threading.Tasks;

namespace DiamondGui
{
	public class DiamondCore
	{
		public DiamondCore(string token, LogSeverity logLevel, string commandsPrefix, Assembly assembly, Func<LogMessage, Task> logFunction)
		{
			Token = token;
			LogLevel = logLevel;
			CommandsPrefix = commandsPrefix;
			Assembly = assembly;

			Initialize();

			Client.Log += logFunction;
			AddModules(assembly);
		}

		public DiamondCore(string token, LogSeverity logLevel)
		{
			Token = token;
			LogLevel = logLevel;

			Initialize();
		}

		public DiscordSocketClient Client;
		public CommandService Commands;

		public Assembly Assembly
		{
			get;
		}

		public LogSeverity LogLevel
		{
			get;
		}

		public string Token
		{
			get;
		}

		public string CommandsPrefix;
		public bool AllowCommandsByPrefix = true;
		public bool AllowCommandsByMention = true;
		public bool DisableCommands;

		public ActivityType ActivityType
		{
			get;
			private set;
		}

		public string ActivityText
		{
			get;
			private set;
		}

		public string StreamUrl
		{
			get;
			private set;
		}

		public UserStatus UserStatus;

		private void Initialize()
		{
			Client = new DiscordSocketClient(new DiscordSocketConfig
			{
				LogLevel = LogLevel,
			});

			Client.MessageReceived += CommandHandler;

			Commands = new CommandService(new CommandServiceConfig
			{
				CaseSensitiveCommands = false,
				DefaultRunMode = RunMode.Async,
				LogLevel = LogLevel,
			});
		}

		public async void SetGame(ActivityType type, string text, string streamUrl = null)
		{
			ActivityType = type;
			ActivityText = text;
			if (!string.IsNullOrEmpty(streamUrl))
			{
				StreamUrl = streamUrl;
			}

			if (Client?.LoginState == LoginState.LoggedIn && !string.IsNullOrEmpty(ActivityText) && (ActivityType == ActivityType.Streaming && !string.IsNullOrEmpty(StreamUrl)))
			{
				await Client.SetGameAsync(ActivityText, StreamUrl, ActivityType).ConfigureAwait(false);
			}
		}

		public async void SetStatus(UserStatus status)
		{
			UserStatus = status;

			if (Client?.LoginState == LoginState.LoggedIn)
			{
				await Client.SetStatusAsync(UserStatus).ConfigureAwait(false);
			}
		}

		public async void AddModules(Assembly assembly, IServiceProvider services = null)
		{
			await Commands.AddModulesAsync(assembly, services).ConfigureAwait(false);
		}

		public async void Start()
		{
			// Status & Game
			SetGame(ActivityType, ActivityText, StreamUrl);
			SetStatus(UserStatus);

			// Login
			await Client.LoginAsync(TokenType.Bot, Token).ConfigureAwait(false);
			await Client.StartAsync().ConfigureAwait(false);

			// Lock Task
			await Task.Delay(-1).ConfigureAwait(true);
		}

		public async void Stop()
		{
			await Client.LogoutAsync().ConfigureAwait(false);
			await Client.StopAsync().ConfigureAwait(false);
		}

		public void Reload()
		{
			Stop();

			Client.Dispose();

			Start();
		}

		private async Task CommandHandler(SocketMessage socketMsg)
		{
			if (!(socketMsg is SocketUserMessage msg))
			{
				return;
			}

			int argPos = 0;

			if (DisableCommands || !AllowCommandsByPrefix || !(msg.HasStringPrefix(CommandsPrefix, ref argPos) || (msg.HasMentionPrefix(Client.CurrentUser, ref argPos) && AllowCommandsByMention)) || msg.Author.IsBot)
			{
				return;
			}

			SocketCommandContext context = new SocketCommandContext(Client, msg);

			await Commands.ExecuteAsync(context, argPos, null).ConfigureAwait(false);
		}
	}
}