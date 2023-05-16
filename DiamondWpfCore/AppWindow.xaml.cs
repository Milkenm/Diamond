using System;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using Diamond.API;
using Diamond.API.APIs;
using Diamond.API.Data;
using Diamond.GUI.Pages;

using Discord;
using Discord.Interactions;
using Discord.WebSocket;

using Microsoft.Extensions.DependencyInjection;

using static Diamond.API.Data.DiamondDatabase;

using SUtils = ScriptsLibV2.Util.Utils;

namespace Diamond.GUI
{
	/// <summary>
	/// Interaction logic for AppWindow.xaml
	/// </summary>
	public partial class AppWindow : Window
	{
		private static bool _botReady = false;

		private readonly DiscordSocketClient _client;
		private readonly DiamondDatabase _database;
		private readonly IServiceProvider _serviceProvider;

		public AppWindow(DiscordSocketClient client, DiamondDatabase database, IServiceProvider serviceProvier)
		{
			this._client = client;
			this._database = database;
			this._serviceProvider = serviceProvier;

			this.InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			MainPanelPage mainPanel = this._serviceProvider.GetRequiredService<MainPanelPage>();
			LogsPanelPage logsPanel = this._serviceProvider.GetRequiredService<LogsPanelPage>();

			// Global exception handler
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler((s, e) =>
			{
				logsPanel.Log(e.ExceptionObject);
			});

			// Load csgo items
			mainPanel.LogAsync("Loading CS:GO items...").GetAwaiter();
			this._serviceProvider.GetRequiredService<CsgoBackpack>().LoadItems().GetAwaiter().OnCompleted(async () =>
			{
				await mainPanel.LogAsync("CS:GO items loaded!");
			});

			// Initialize bot
			InteractionService interactionService = this._serviceProvider.GetRequiredService<InteractionService>();
			Lava lava = this._serviceProvider.GetRequiredService<Lava>();
			this._client.Ready += new Func<Task>(async () =>
			{
				if (_botReady)
				{
					return;
				}

				_botReady = true;

				await interactionService.AddModulesAsync(SUtils.GetAssemblyByName("DiamondAPI"), this._serviceProvider);
				if (SUtils.IsDebugEnabled() && this._database.GetSetting(ConfigSetting.DebugGuildID) != null)
				{
					await interactionService.RegisterCommandsToGuildAsync(Convert.ToUInt64(this._database.GetSetting(ConfigSetting.DebugGuildID)));
				}
				else
				{
					await interactionService.RegisterCommandsGloballyAsync();
				}
			});
			// Run interactions (slash commands/modals/buttons/...)
			this._client.InteractionCreated += async (socketInteraction) =>
			{
				// Ignore debug channel if debug is disabled and ignore normal channels if debug is enabled
				bool isDebugChannel = Utils.IsDebugChannel(_database.GetSetting(DiamondDatabase.ConfigSetting.DebugChannelsID), socketInteraction.ChannelId);
				if ((isDebugChannel && !SUtils.IsDebugEnabled()) || (!isDebugChannel && SUtils.IsDebugEnabled()))
				{
					return;
				}

				SocketInteractionContext context = new SocketInteractionContext(this._client, socketInteraction);
				await interactionService.ExecuteCommandAsync(context, this._serviceProvider);
			};
			// Handle interaction exceptions
			interactionService.InteractionExecuted += async (command, context, result) =>
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
								errorEmbed = new DefaultEmbed("Error", "🛑", context.Interaction)
								{
									Title = "No permission",
									Description = result.ErrorReason,
								};
							}
							break;
						case InteractionCommandError.UnknownCommand:
							{
								errorEmbed = new DefaultEmbed("Error", "🤔", context.Interaction)
								{
									Title = "Unknown command",
									Description = $"That command was not found.",
								};
							}
							break;
						default:
							{
								errorEmbed = new DefaultEmbed("Error", "🔥", context.Interaction)
								{
									Title = "Something bad happened... :(",
									Description = "This error was reported to the dev, hope to get it fixed soon...",
								};
								logsPanel.Log($"Error running '{(command != null ? command.Name : "unknown")}' (user: {context.User.Username}#{context.User.Discriminator}):\n{result.ErrorReason} ({result.Error.GetType()})\nInteraction: {command}\n{contextProperties}\n{interactionProperties}\n{slashCommandDataProperties}");
							}
							break;
					}

					if (!context.Interaction.HasResponded)
					{
						await context.Interaction.DeferAsync(true);
					}
					await context.Interaction.ModifyOriginalResponseAsync((og) =>
					{
						og.Embed = errorEmbed.Build();
					});
				}
			};
			// Disconnect from LavaLink when the bot logs out
			this._client.LoggedOut += async () =>
			{
				await lava.StopNodeAsync();
			};
			this._client.Log += new Func<LogMessage, Task>((logMessage) => this._serviceProvider.GetRequiredService<MainPanelPage>().LogAsync(logMessage.Message));

			// Windows events
			Closing += new CancelEventHandler((se, ev) =>
			{
				this._client.StopAsync().GetAwaiter();
			});

			// Set frames
			this.frame_main.Navigate(mainPanel);
			this.frame_logs.Navigate(this._serviceProvider.GetRequiredService<LogsPanelPage>());
			this.frame_lavalink.Navigate(this._serviceProvider.GetRequiredService<LavalinkPanelPage>());
			this.frame_remote.Navigate(this._serviceProvider.GetRequiredService<RemotePanelPage>());
			this.frame_settings.Navigate(this._serviceProvider.GetRequiredService<SettingsPanelPage>());

			// Check if settings are valid
			if (!this._serviceProvider.GetRequiredService<DiamondDatabase>().AreSettingsValid())
			{
				this.ToggleUI(false);
				this.tabControl_main.SelectedIndex = this.tabControl_main.Items.Count - 1;
			}
		}

		public void ToggleUI(bool enabled)
		{
			if (this.tabItem_main.IsEnabled == enabled)
			{
				return;
			}

			// Main
			DisableImage(this.image_main, !enabled);
			this.tabItem_main.IsEnabled = enabled;
			// Logs
			DisableImage(this.image_logs, !enabled);
			this.tabItem_logs.IsEnabled = enabled;
			// Lavalink
			DisableImage(this.image_lavalink, !enabled);
			this.tabItem_lavalink.IsEnabled = enabled;
			// RCON
			DisableImage(this.image_rcon, !enabled);
			this.tabItem_rcon.IsEnabled = enabled;
		}

		private static void DisableImage(System.Windows.Controls.Image image, bool grayout)
		{
			if (grayout)
			{
				// Get the source bitmap                        
				if (image.Source is BitmapSource bitmapImage)
				{
					ImageBrush OpacityMask = new ImageBrush(bitmapImage);
					image.Source = new FormatConvertedBitmap(bitmapImage, PixelFormats.Gray8, null, 0);
					// Reuse the opacity mask from the original image as FormatConvertedBitmap does not keep transparency info
					image.OpacityMask = OpacityMask;
				}
			}
			else
			{
				image.Source = ((FormatConvertedBitmap)image.Source).Source;
			}
		}

		private static string GetObjectProperties(object obj)
		{
			StringBuilder sb = new StringBuilder();
			foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj))
			{
				if (sb.Length > 0)
				{
					sb.Append('\n');
				}
				string name = descriptor.Name;
				object value = descriptor.GetValue(obj);
				value ??= "<null>";
				sb.Append($"\t{name}={value}");
			}
			return $"{obj.GetType()}:\n{sb}";
		}

		private async void Window_Closing(object sender, CancelEventArgs e)
		{
			// Logout
			await this._client.TakeLifeAsync();
			// Stop Lavalink
			this._serviceProvider.GetRequiredService<Lava>().Dispose();
		}
	}
}
