using Diamond.Core.TypeReaders;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

namespace Diamond.Core
{
	public class DiamondCore
	{
		public DiamondCore(string token, LogSeverity logLevel, string commandsPrefix, Assembly assembly, IServiceProvider serviceProvider)
		{
			this.Token = token;
			this.LogLevel = logLevel;
			CommandsPrefix = commandsPrefix;
			this.Assembly = assembly;
			this.ServiceProvider = serviceProvider;

			this.CheckDebugMode();
			this.Initialize();
			this.LoadModules(this.Assembly, this.ServiceProvider).GetAwaiter();
		}

		public DiscordSocketClient Client;
		public CommandService Commands;

		public string CommandsPrefix;
		public bool AllowCommandsByPrefix = true;
		public bool AllowCommandsByMention = true;
		public bool DisableCommands;
		public readonly List<ulong> DebugChannels = new List<ulong>();

		public string Token { get; }
		public LogSeverity LogLevel { get; }
		public Assembly Assembly { get; }
		public IServiceProvider ServiceProvider { get; }

		public ActivityType ActivityType { get; private set; }
		public string ActivityText { get; private set; }
		public string StreamUrl { get; private set; }
		public UserStatus UserStatus { get; private set; }
		public bool Debugging { get; private set; }

		public bool IsRunning { get; private set; }

		private void Initialize()
		{
			Client = new DiscordSocketClient(new DiscordSocketConfig
			{
				LogLevel = LogLevel,
			});

			Client.MessageReceived += this.CommandHandler;

			Commands = new CommandService(new CommandServiceConfig
			{
				CaseSensitiveCommands = false,
				DefaultRunMode = RunMode.Async,
				LogLevel = LogLevel,
			});

			Commands.AddTypeReader(typeof(Emote), new EmoteTypeReader());
		}

		public async Task LoadModules(Assembly assembly, IServiceProvider services = null)
		{
			await Commands.AddModulesAsync(assembly, services);
		}

		public async Task SetGame(ActivityType type, string text, string streamUrl = null)
		{
			this.ActivityType = type;
			this.ActivityText = text;
			if (!string.IsNullOrEmpty(streamUrl))
			{
				this.StreamUrl = streamUrl;
			}

			if (Client?.LoginState == LoginState.LoggedIn && !string.IsNullOrEmpty(this.ActivityText) && (this.ActivityType == ActivityType.Streaming && !string.IsNullOrEmpty(this.StreamUrl)))
			{
				await Client.SetGameAsync(this.ActivityText, this.StreamUrl, this.ActivityType);
			}
		}

		public async Task SetStatus(UserStatus status)
		{
			this.UserStatus = status;

			if (Client?.LoginState == LoginState.LoggedIn)
			{
				await Client.SetStatusAsync(this.UserStatus);
			}
		}

		public async Task Start()
		{
			this.IsRunning = true;

			// Status & Game
			await this.SetGame(this.ActivityType, this.ActivityText, this.StreamUrl);
			await this.SetStatus(this.UserStatus);

			// Login
			await Client.LoginAsync(TokenType.Bot, this.Token);
			await Client.StartAsync();
		}

		public async Task Stop()
		{
			if (this.IsRunning)
			{
				this.IsRunning = false;

				await Client.LogoutAsync();
				await Client.StopAsync();
			}
		}

		public async Task Reload()
		{
			await this.Stop();
			Client.Dispose();

			await this.Start();
		}

		private async Task CommandHandler(SocketMessage socketMsg)
		{
			if (!(socketMsg is SocketUserMessage msg) || (!this.Debugging && DebugChannels.Contains(msg.Channel.Id)) || (this.Debugging && !DebugChannels.Contains(msg.Channel.Id)))
			{
				return;
			}

			int argPos = 0;

			if (DisableCommands || !AllowCommandsByPrefix || msg.Author.IsBot || !(msg.HasStringPrefix(CommandsPrefix, ref argPos) || (msg.HasMentionPrefix(Client.CurrentUser, ref argPos) && AllowCommandsByMention)))
			{
				return;
			}

			SocketCommandContext context = new SocketCommandContext(Client, msg);

			await Commands.ExecuteAsync(context, argPos, null);
		}

		public void AddDebugChannels(params ulong[] channelsIds)
		{
			foreach (ulong channelId in channelsIds)
			{
				DebugChannels.Add(channelId);
			}
		}

		[Conditional("DEBUG")]
		public void CheckDebugMode()
		{
			this.Debugging = true;
		}
	}
}