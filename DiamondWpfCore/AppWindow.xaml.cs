using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

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

			// Load csgo items
			mainPanel.LogAsync("Loading CS:GO items...").GetAwaiter();
			_serviceProvider.GetRequiredService<CsgoBackpack>().LoadItems().GetAwaiter().OnCompleted(async () =>
			{
				await mainPanel.LogAsync("CS:GO items loaded!");
			});

			// Initialize bot
			_bot.Initialize();
			InteractionService interactionService = new InteractionService(_bot.Client.Rest);
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
				interactionService.SlashCommandExecuted += async (command, context, result) =>
				{
					if (!result.IsSuccess)
					{
						DefaultEmbed errorEmbed = new DefaultEmbed("Error", "🔥", context.Interaction)
						{
							Description = "Something bad happened... :(",
						};
						errorEmbed.AddField("Cause", result.ErrorReason);

						await context.Interaction.ModifyOriginalResponseAsync((og) =>
						{
							og.Embed = errorEmbed.Build();
						});
					}
				};
			});
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
		}
	}
}
