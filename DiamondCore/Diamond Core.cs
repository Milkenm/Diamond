using Diamond.Core.TypeReaders;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Diamond.Core
{
	public class DiamondCore
	{
		public DiamondCore(LogSeverity logLevel, Assembly assembly, IServiceProvider serviceProvider, string configPath = @"\bot_config.json")
		{
			this.LogLevel = logLevel;
			this.Assembly = assembly;
			this.ServiceProvider = serviceProvider;
			this.ConfigPath = configPath;

			this.Initialize();
		}

		public string ConfigPath { get; set; }
		public bool AllowCommandsByPrefix { get; set; } = true;
		public bool AllowCommandsByMention { get; set; } = true;
		public bool DisableCommands { get; set; }

		public LogSeverity LogLevel { get; }
		public Assembly Assembly { get; }
		public IServiceProvider ServiceProvider { get; }

		public Config Configuration { get; private set; }
		public DiscordSocketClient Client { get; private set; }
		public CommandService Commands { get; private set; }
		public ActivityType ActivityType { get; private set; }
		public string ActivityText { get; private set; }
		public string StreamUrl { get; private set; }
		public UserStatus UserStatus { get; private set; }
		public bool Debugging { get; private set; }
		public bool IsRunning { get; private set; }

		private void Initialize()
		{
			this.CheckDebugMode();
			this.LoadConfig(this.ReadConfigAsync());

			this.Client = new DiscordSocketClient(new DiscordSocketConfig
			{
				LogLevel = LogLevel,
			});

			this.Client.MessageReceived += this.CommandHandlerAsync;

			this.Commands = new CommandService(new CommandServiceConfig
			{
				CaseSensitiveCommands = false,
				DefaultRunMode = RunMode.Async,
				LogLevel = LogLevel,
			});

			this.Commands.AddTypeReader(typeof(Emote), new EmoteTypeReader());
			this.LoadModules(this.Assembly, this.ServiceProvider).GetAwaiter().GetResult();
		}

		private Config ReadConfigAsync()
		{
			string configJson = File.ReadAllText(this.ConfigPath);
			Config cfg = JsonConvert.DeserializeObject<Config>(configJson);

			return cfg;
		}

		private bool LoadConfig(Config config)
		{
			bool tokenChanged = this.Configuration?.Token != config.Token;

			this.Configuration = config;

			return tokenChanged;
		}

		public async Task ReloadConfigAsync()
		{
			bool tokenChanged = this.LoadConfig(this.ReadConfigAsync());
			if (tokenChanged)
			{
				await this.RestartAsync();
			}
		}

		public async Task SaveConfigAsync(Config config)
		{
			await Task.Run(() =>
			{
				string jsonString = JsonConvert.SerializeObject(config);
				File.WriteAllText(this.ConfigPath, jsonString);
			});
		}

		public async Task UpdateConfig(Config config)
		{
			if (string.IsNullOrEmpty(config.Token))
			{
				config.Token = this.Configuration.Token;
			}
			if (string.IsNullOrEmpty(config.Prefix))
			{
				config.Prefix = this.Configuration.Prefix;
			}
			if (config.DebugChannels == null)
			{
				config.DebugChannels = this.Configuration.DebugChannels;
			}

			await this.SaveConfigAsync(config);
			await this.ReloadConfigAsync();
		}

		public void SetupEvents(Func<Optional<CommandInfo>, ICommandContext, IResult, Task> commandExecuted, Func<SocketMessage, Task> messageReceived, Func<Cacheable<IUserMessage, ulong>, ISocketMessageChannel, SocketReaction, Task> reactionAdded, Func<Cacheable<IUserMessage, ulong>, ISocketMessageChannel, SocketReaction, Task> reactionRemoved)
		{
			this.Commands.CommandExecuted += commandExecuted;
			this.Client.MessageReceived += messageReceived;
			this.Client.ReactionAdded += reactionAdded;
			this.Client.ReactionRemoved += reactionRemoved;
		}

		public async Task LoadModules(Assembly assembly, IServiceProvider services = null)
		{
			await this.Commands.AddModulesAsync(assembly, services);
		}

		public async Task SetGameAsync(ActivityType type, string text, string streamUrl = null)
		{
			this.ActivityType = type;
			this.ActivityText = text;
			if (!string.IsNullOrEmpty(streamUrl))
			{
				this.StreamUrl = streamUrl;
			}

			if (this.Client?.LoginState == LoginState.LoggedIn && !string.IsNullOrEmpty(this.ActivityText) && (this.ActivityType == ActivityType.Streaming && !string.IsNullOrEmpty(this.StreamUrl)))
			{
				await this.Client.SetGameAsync(this.ActivityText, this.StreamUrl, this.ActivityType);
			}
		}

		public async Task SetStatusAsync(UserStatus status)
		{
			this.UserStatus = status;

			if (this.Client?.LoginState == LoginState.LoggedIn)
			{
				await this.Client.SetStatusAsync(this.UserStatus);
			}
		}

		public async Task StartAsync()
		{
			if (this.IsRunning == false)
			{
				await this.SetGameAsync(this.ActivityType, this.ActivityText, this.StreamUrl);
				await this.SetStatusAsync(this.UserStatus);

				await this.Client.LoginAsync(TokenType.Bot, this.Configuration.Token);
				await this.Client.StartAsync();

				this.IsRunning = true;
			}
		}

		public async Task StopAsync()
		{
			if (this.IsRunning)
			{
				await this.Client.LogoutAsync();
				await this.Client.StopAsync();

				this.IsRunning = false;
			}
		}

		public async Task RestartAsync()
		{
			if (this.IsRunning)
			{
				await this.StopAsync();
				await this.StartAsync();
			}
		}

		private async Task CommandHandlerAsync(SocketMessage socketMsg)
		{
			if (!(socketMsg is SocketUserMessage msg) || (!this.Debugging && this.Configuration.DebugChannels.Contains(msg.Channel.Id)) || (this.Debugging && !this.Configuration.DebugChannels.Contains(msg.Channel.Id)))
			{
				return;
			}

			int argPos = 0;
			if (this.DisableCommands || !this.AllowCommandsByPrefix || msg.Author.IsBot || !(msg.HasStringPrefix(this.Configuration.Prefix, ref argPos) || (msg.HasMentionPrefix(this.Client.CurrentUser, ref argPos) && this.AllowCommandsByMention)))
			{
				return;
			}

			SocketCommandContext context = new SocketCommandContext(this.Client, msg);

			await this.Commands.ExecuteAsync(context, argPos, null);
		}

		public void AddDebugChannels(params ulong[] channelsIds)
		{
			foreach (ulong channelId in channelsIds)
			{
				this.Configuration.DebugChannels.Add(channelId);
			}
		}

		[Conditional("DEBUG")]
		public void CheckDebugMode()
		{
			this.Debugging = true;
		}

		public class Config
		{
			[JsonProperty("token")] public string Token { get; set; }
			[JsonProperty("prefix")] public string Prefix { get; set; }
			[JsonProperty("debugChannels")] public List<ulong> DebugChannels { get; set; }
		}
	}
}