﻿using System;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using Diamond.API.APIs;
using Diamond.API.Data;
using Diamond.API.Util;
using Diamond.GUI.Pages;

using Discord;
using Discord.Interactions;
using Discord.WebSocket;

using Microsoft.Extensions.DependencyInjection;

using ScriptsLibV2.Extensions;

using static Diamond.API.Data.DiamondContext;

using Image = System.Windows.Controls.Image;
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
		private readonly IServiceProvider _serviceProvider;

		public AppWindow(DiscordSocketClient client, IServiceProvider serviceProvier)
		{
			this._client = client;
			this._serviceProvider = serviceProvier;

			this.InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			using DiamondContext db = new DiamondContext();

			MainPanelPage mainPanel = this._serviceProvider.GetRequiredService<MainPanelPage>();
			LogsPanelPage logsPanel = this._serviceProvider.GetRequiredService<LogsPanelPage>();

			// Disable guilds tab
			this.ToggleUIElement(this.image_guilds, this.tabItem_guilds, false);

			// Global exception handler
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler((s, e) =>
			{
				logsPanel.Log(e.ExceptionObject);
			});

			// Load CS:GO items
			CsgoBackpack csgoBackpack = this._serviceProvider.GetRequiredService<CsgoBackpack>();
			csgoBackpack.OnCheckingForUpdate += new CsgoItemsStateChanged(async () => await mainPanel.LogAsync("Checking for CS:GO items update..."));
			csgoBackpack.OnUpdateCancelled += new CsgoItemsStateChanged(async () => await mainPanel.LogAsync("No update needed for CS:GO items!"));
			csgoBackpack.OnUpdateStart += new CsgoItemsStateChanged(async () => await mainPanel.LogAsync("Downloading CS:GO items..."));
			csgoBackpack.OnUpdateEnd += new CsgoItemsStateChanged(async () => await mainPanel.LogAsync("CS:GO items loaded!"));
			csgoBackpack.LoadItems();

			// Initialize bot
			InteractionService interactionService = this._serviceProvider.GetRequiredService<InteractionService>();
			Lava lava = this._serviceProvider.GetRequiredService<Lava>();
			this._client.Ready += new Func<Task>(async () =>
			{
				using DiamondContext db = new DiamondContext();

				// Only run this code block once
				if (_botReady)
				{
					return;
				}
				_botReady = true;

				await interactionService.AddModulesAsync(SUtils.GetAssemblyByName("DiamondAPI"), this._serviceProvider);
				string? debugGuildIdString = db.GetSetting(ConfigSetting.DebugGuildID);
				ulong? debugGuildId = !debugGuildIdString.IsEmpty() ? Convert.ToUInt64(debugGuildIdString) : null;

#if DEBUG
				// Register commands only to dev guild
				if (debugGuildId.HasValue)
				{
					await interactionService.RegisterCommandsToGuildAsync((ulong)debugGuildId);
					await mainPanel.LogAsync($"Registered {interactionService.SlashCommands.Count} commands to dev guild.");
				}
#else
				// Register commands globally (to every guild)
				await interactionService.RegisterCommandsGloballyAsync(true);
				await mainPanel.LogAsync($"Registered {interactionService.SlashCommands.Count} commands to {_client.Guilds.Count} guilds.");
#endif
			});
			// Run interactions (slash commands/modals/buttons/...)
			this._client.InteractionCreated += async (socketInteraction) =>
			{
				using DiamondContext db = new DiamondContext();

				// Ignore debug channel if debug is disabled and ignore normal channels if debug is enabled
				bool isDebugChannel = Utils.IsDebugChannel(db.GetSetting(ConfigSetting.DebugChannelsID), socketInteraction.ChannelId);
#if DEBUG
				if (!isDebugChannel) return;
#elif RELEASE
				if (isDebugChannel) return;
#endif

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
			// Bot connect
			this._client.Connected += new Func<Task>(() =>
			{
				// Enable guilds tab
				this.Dispatcher.Invoke(async () =>
				{
					this.ToggleUIElement(this.image_guilds, this.tabItem_guilds, true);

					// Refesh guilds tab
					GuildsPanelPage guildsPanel = this._serviceProvider.GetRequiredService<GuildsPanelPage>();
					await guildsPanel.LoadGuildsAsync();
				});

				return Task.CompletedTask;
			});
			// Bot disconnect
			this._client.Disconnected += new Func<Exception, Task>((_) =>
			{
				// Disable guilds tab
				this.Dispatcher.Invoke(() =>
				{
					this.ToggleUIElement(this.image_guilds, this.tabItem_guilds, false);
				});

				return Task.CompletedTask;
			});
			// Bot log
			this._client.Log += new Func<LogMessage, Task>((logMessage) => this._serviceProvider.GetRequiredService<MainPanelPage>().LogAsync(logMessage.Message));

			// App events
			this.Closing += new CancelEventHandler(async (se, ev) =>
			{
				await this._client.StopAsync();
			});

			// Set frames
			this.frame_main.Navigate(mainPanel);
			this.frame_logs.Navigate(this._serviceProvider.GetRequiredService<LogsPanelPage>());
			this.frame_guilds.Navigate(this._serviceProvider.GetRequiredService<GuildsPanelPage>());
			this.frame_lavalink.Navigate(this._serviceProvider.GetRequiredService<LavalinkPanelPage>());
			this.frame_remote.Navigate(this._serviceProvider.GetRequiredService<RemotePanelPage>());
			this.frame_settings.Navigate(this._serviceProvider.GetRequiredService<SettingsPanelPage>());

			// Check if settings are valid
			if (!db.AreSettingsValid())
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
			this.ToggleUIElement(this.image_main, this.tabItem_main, enabled);
			// Logs
			this.ToggleUIElement(this.image_logs, this.tabItem_logs, enabled);
			// Lavalink
			this.ToggleUIElement(this.image_lavalink, this.tabItem_lavalink, enabled);
			// RCON
			this.ToggleUIElement(this.image_rcon, this.tabItem_rcon, enabled);
		}

		private void ToggleUIElement(Image image, TabItem tab, bool isEnabled)
		{
			DisableImage(image, !isEnabled);
			tab.IsEnabled = isEnabled;
		}

		private static void DisableImage(Image image, bool grayout)
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
					_ = sb.Append('\n');
				}
				string name = descriptor.Name;
				object value = descriptor.GetValue(obj);
				value ??= "<null>";
				_ = sb.Append($"\t{name}={value}");
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
