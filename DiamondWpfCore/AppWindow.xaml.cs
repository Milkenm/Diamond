using System;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using Diamond.API;
using Diamond.API.Bot;
using Diamond.API.Data;
using Diamond.API.Stuff;
using Diamond.GUI.Pages;

using Discord;
using Discord.Interactions;

using Microsoft.Extensions.DependencyInjection;

namespace Diamond.GUI
{
	/// <summary>
	/// Interaction logic for AppWindow.xaml
	/// </summary>
	public partial class AppWindow : Window
	{
		private readonly DiamondBot _bot;
		private readonly DiamondDatabase _database;
		private readonly IServiceProvider _serviceProvider;

		public AppWindow(DiamondBot bot, DiamondDatabase database, IServiceProvider serviceProvier)
		{
			InitializeComponent();

			_bot = bot;
			_database = database;
			_serviceProvider = serviceProvier;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			MainPanelPage mainPanel = _serviceProvider.GetRequiredService<MainPanelPage>();
			LogsPanelPage logsPanel = _serviceProvider.GetRequiredService<LogsPanelPage>();

			// Load csgo items
			mainPanel.LogAsync("Loading CS:GO items...").GetAwaiter();
			_serviceProvider.GetRequiredService<CsgoBackpack>().LoadItems().GetAwaiter().OnCompleted(async () =>
			{
				await mainPanel.LogAsync("CS:GO items loaded!");
			});

			// Initialize bot
			_bot.Initialize();
			InteractionService interactionService = new InteractionService(_bot.Client.Rest);
			Lava lava = _serviceProvider.GetRequiredService<Lava>();
			_bot.Client.Ready += new Func<Task>(async () =>
			{
				await interactionService.AddModulesAsync(ScriptsLibV2.Util.Utils.GetAssemblyByName("DiamondAPI"), _serviceProvider);
				if (ScriptsLibV2.Util.Utils.IsDebugEnabled() && Utils.GetSetting(_database, "DebugGuildId") != null)
				{
					await interactionService.RegisterCommandsToGuildAsync(Convert.ToUInt64(Utils.GetSetting(_database, "DebugGuildId")));
				}
				else
				{
					await interactionService.RegisterCommandsGloballyAsync();
				}
			});
			// Listen for slash commands
			interactionService.SlashCommandExecuted += async (command, context, result) =>
			{
				if (!result.IsSuccess)
				{
					DefaultEmbed errorEmbed = new DefaultEmbed("Error", "🔥", context.Interaction)
					{
						Title = "Something bad happened... :(",
						Description = "This error was reported to the dev, hope to get it fixed soon..."
					};

					string contextProperties = GetObjectProperties(context);
					string interactionProperties = GetObjectProperties(context.Interaction);
					string slashCommandDataProperties = GetObjectProperties(context.Interaction.Data);

					await logsPanel.Log($"[{DateTimeOffset.Now}] Error running command '{command.Name}' (user: {context.User.Username}#{context.User.Discriminator}):\n{result.ErrorReason} ({result.Error.GetType()})\nCommand: {command}\n{contextProperties}\n{interactionProperties}\n{slashCommandDataProperties}");

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
			// Listen for modals/buttons/...
			_bot.Client.InteractionCreated += async (interaction) =>
			{
				// Ignore slash commands
				if (interaction.Type == InteractionType.ApplicationCommand) return;

				SocketInteractionContext context = new SocketInteractionContext(_bot.Client, interaction);

				// Execute the incoming command.
				await interactionService.ExecuteCommandAsync(context, _serviceProvider);
			};
			// Disconnect from LavaLink when the bot logs out
			_bot.Client.LoggedOut += async () =>
			{
				await lava.StopNodeAsync();
			};
			_bot.Client.SlashCommandExecuted += async (socketInteraction) =>
			{
				// Ignore debug channel if debug is disabled and ignore normal channels if debug is enabled
				if ((socketInteraction.ChannelId == Convert.ToUInt64(Utils.GetSetting(_database, "DebugChannelId")) && !ScriptsLibV2.Util.Utils.IsDebugEnabled()) || (socketInteraction.ChannelId != Convert.ToUInt64(Utils.GetSetting(_database, "DebugChannelId")) && ScriptsLibV2.Util.Utils.IsDebugEnabled())) return;

				SocketInteractionContext context = new SocketInteractionContext(_bot.Client, socketInteraction);
				await interactionService.ExecuteCommandAsync(context, _serviceProvider);
			};
			_bot.Client.Log += new Func<LogMessage, Task>((logMessage) => _serviceProvider.GetRequiredService<MainPanelPage>().LogAsync(logMessage.Message));

			// Windows events
			Closing += new CancelEventHandler((se, ev) =>
			{
				_bot.StopAsync().GetAwaiter();
			});

			// Set frames
			frame_main.Navigate(mainPanel);
			frame_logs.Navigate(_serviceProvider.GetRequiredService<LogsPanelPage>());
			frame_remote.Navigate(_serviceProvider.GetRequiredService<RemotePanelPage>());
			frame_settings.Navigate(_serviceProvider.GetRequiredService<SettingsPanelPage>());
			frame_lavalink.Navigate(_serviceProvider.GetRequiredService<LavalinkPanelPage>());

			// Check if settings are valid
			if (!Utils.AreSettingsValid(_serviceProvider.GetRequiredService<DiamondDatabase>()))
			{
				ToggleUI(false);
				tabControl_main.SelectedIndex = tabControl_main.Items.Count - 1;
			}
		}

		public void ToggleUI(bool enabled)
		{
			if (tabItem_main.IsEnabled == enabled) return;

			// Main
			DisableImage(image_main, !enabled);
			tabItem_main.IsEnabled = enabled;
			// Logs
			DisableImage(image_logs, !enabled);
			tabItem_logs.IsEnabled = enabled;
			// RCON
			DisableImage(image_rcon, !enabled);
			tabItem_rcon.IsEnabled = enabled;
			// Lavalink
			DisableImage(image_lavalink, !enabled);
			tabItem_lavalink.IsEnabled = enabled;
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

		private void Window_Closing(object sender, CancelEventArgs e)
		{
			_serviceProvider.GetRequiredService<Lava>().Dispose();
		}
	}
}
