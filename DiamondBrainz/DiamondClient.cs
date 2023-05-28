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

using static Diamond.API.Data.DiamondContext;

using SUtils = ScriptsLibV2.Util.Utils;

namespace Diamond.API
{
	public delegate void BotLogEvent(string message, bool isError);

	public class DiamondClient : DiscordSocketClient
	{
		private readonly IServiceProvider _serviceProvider;
		private readonly InteractionService _interactionService;

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

			// Ignore debug channel if debug is disabled and ignore normal channels if debug is enabled
			bool isDebugChannel = Utils.IsDebugChannel(db.GetSetting(ConfigSetting.DebugChannelsID), socketInteraction.ChannelId);
#if DEBUG
			if (!isDebugChannel) return;
#elif RELEASE
				if (isDebugChannel) return;
#endif

			SocketInteractionContext context = new SocketInteractionContext(this, socketInteraction);
			await this._interactionService.ExecuteCommandAsync(context, this._serviceProvider);
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

			await this._interactionService.AddModulesAsync(SUtils.GetAssemblyByName("DiamondAPI"), this._serviceProvider);
			string? debugGuildIdString = db.GetSetting(ConfigSetting.DebugGuildID);
			ulong? debugGuildId = !debugGuildIdString.IsEmpty() ? Convert.ToUInt64(debugGuildIdString) : null;

#if DEBUG
			// Register commands only to dev guild
			if (debugGuildId.HasValue)
			{
				await this._interactionService.RegisterCommandsToGuildAsync((ulong)debugGuildId);
				OnLog?.Invoke($"Registered {this._interactionService.SlashCommands.Count} commands to dev guild.", false);
			}
#else
				// Register commands globally (to every guild)
				await _interactionService.RegisterCommandsGloballyAsync(true);
				OnLog?.Invoke($"Registered {_interactionService.SlashCommands.Count} commands to {this.Guilds.Count} guilds.", false);
#endif
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
