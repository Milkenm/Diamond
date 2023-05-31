using System;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

using Diamond.API.Data;
using Diamond.API.Util;

using Discord;
using Discord.Interactions;
using Discord.WebSocket;

using ScriptsLibV2.Extensions;

using SUtils = ScriptsLibV2.Util.Utils;

namespace Diamond.API
{
	public delegate void BotLogEvent(string message, bool isError);

	public class DiamondClient : DiscordSocketClient
	{
		private readonly IServiceProvider _serviceProvider;
		private readonly InteractionService _interactionService;

		public long LastLogin { get; private set; }
		public long Uptime
		{
			get => DateTimeOffset.UtcNow.ToUnixTimeSeconds() - this.LastLogin;
		}
		public bool IsReady { get; private set; }

		public event BotLogEvent OnLog;

		public DiamondClient(IServiceProvider serviceProvider)
		: base(new DiscordSocketConfig
		{
			LogLevel = LogSeverity.Info,
			GatewayIntents = GatewayIntents.All,
		})
		{
			this._serviceProvider = serviceProvider;

			this._interactionService = new InteractionService(this);
		}

		public void Initialize()
		{
			// Initialize bot
			this.Ready += this.DiamondClient_Ready;

			// Run interactions (slash commands/modals/buttons/...)
			this.InteractionCreated += this.DiamondClient_InteractionCreated;

			// Handle interaction exceptions
			this._interactionService.InteractionExecuted += this.InteractionService_InteractionExecuted;

			// Uptime updaters
			this.LoggedIn += this.DiamondClient_LoggedIn;
			this.LoggedOut += this.DiamondClient_LoggedOut;

			// Bot log
			this.Log += new Func<LogMessage, Task>((logMessage) =>
			{
				OnLog?.Invoke(logMessage.Message, false);
				return Task.CompletedTask;
			});
		}

		private async Task InteractionService_InteractionExecuted(ICommandInfo command, IInteractionContext context, IResult result)
		{
			if (!result.IsSuccess)
			{
				string contextProperties = GetObjectProperties(context);
				string interactionProperties = GetObjectProperties(context.Interaction);
				string slashCommandDataProperties = GetObjectProperties(context.Interaction.Data);

				DefaultEmbed errorEmbed;
				switch (result.Error)
				{
					case InteractionCommandError.UnmetPrecondition:
						{
							errorEmbed = new DefaultEmbed("Error", "🛑", context)
							{
								Title = "No permission",
								Description = result.ErrorReason,
							};
						}
						break;
					case InteractionCommandError.UnknownCommand:
						{
							errorEmbed = new DefaultEmbed("Error", "🤔", context)
							{
								Title = "Unknown command",
								Description = $"That command was not found.",
							};
						}
						break;
					default:
						{
							errorEmbed = new DefaultEmbed("Error", "🔥", context)
							{
								Title = "Something bad happened... :(",
								Description = "This error was reported to the dev, hope to get it fixed soon...",
							};
							OnLog?.Invoke($"Error running '{(command != null ? command.Name : "unknown")}' (user: {context.User.Username}#{context.User.Discriminator}):\n{result.ErrorReason} ({result.Error.GetType()})\nInteraction: {command}\n{contextProperties}\n{interactionProperties}\n{slashCommandDataProperties}", true);
						}
						break;
				}

				if (!context.Interaction.HasResponded)
				{
					await context.Interaction.DeferAsync(true);
				}
				_ = await context.Interaction.ModifyOriginalResponseAsync((og) =>
				{
					og.Embed = errorEmbed.Build();
				});
			}
		}

		private async Task DiamondClient_InteractionCreated(SocketInteraction socketInteraction)
		{
			using DiamondContext db = new DiamondContext();

			bool isDebugChannel = Utils.IsDebugChannel(db.GetSetting(ConfigSetting.DebugChannelsID), socketInteraction.ChannelId);
			bool ignoreDebugChannel = Convert.ToBoolean(db.GetSetting(ConfigSetting.IgnoreDebugChannels));

			// Ignore debug channel if debug is disabled and ignore normal channels if debug is enabled
			if ((SUtils.IsDebugEnabled() && !isDebugChannel) || (!SUtils.IsDebugEnabled() && ignoreDebugChannel && isDebugChannel)) return;

			SocketInteractionContext context = new SocketInteractionContext(this, socketInteraction);
			_ = await this._interactionService.ExecuteCommandAsync(context, this._serviceProvider);
		}

		private async Task DiamondClient_Ready()
		{
			using DiamondContext db = new DiamondContext();

			// Only run this code block once
			if (this.IsReady)
			{
				return;
			}
			this.IsReady = true;

			// Load command modules
			_ = await this._interactionService.AddModulesAsync(SUtils.GetAssemblyByName("DiamondAPI"), this._serviceProvider);

			if (!SUtils.IsDebugEnabled())
			{
				// Register commands globally (to every guild)
				_ = await this._interactionService.RegisterCommandsGloballyAsync(true);
				OnLog?.Invoke($"Registered {this._interactionService.SlashCommands.Count} commands to {this.Guilds.Count} guild{(this.Guilds.Count != 1 ? "s" : "")}.", false);
			}
			else
			{
				// Check if debug guild is found
				string? debugGuildIdString = db.GetSetting(ConfigSetting.DebugGuildID);
				ulong? debugGuildId = !debugGuildIdString.IsEmpty() ? Convert.ToUInt64(debugGuildIdString) : null;
				if (debugGuildId == null || this.GetGuild((ulong)debugGuildId) == null)
				{
					OnLog?.Invoke("Debug guild not found.", false);
					return;
				}
				// Register commands to debug guild
				_ = await this._interactionService.RegisterCommandsToGuildAsync((ulong)debugGuildId, true);
				OnLog?.Invoke($"Registered {this._interactionService.SlashCommands.Count} commands to debug guild.", false);
			}
		}

		private Task DiamondClient_LoggedIn()
		{
			this.LastLogin = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
			return Task.CompletedTask;
		}

		private Task DiamondClient_LoggedOut()
		{
			return Task.CompletedTask;
		}

		private static string GetObjectProperties(object obj)
		{
			StringBuilder sb = new StringBuilder();
			foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj))
			{
				if (sb.Length > 0)
				{
					_ = sb.Append('\n');
				}
				string name = descriptor.Name;
				object value = descriptor.GetValue(obj);
				value ??= "<null>";
				_ = sb.Append($"\t{name}={value}");
			}
			return $"{obj.GetType()}:\n{sb}";
		}
	}
}
