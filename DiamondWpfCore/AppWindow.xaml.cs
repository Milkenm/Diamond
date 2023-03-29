using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

using Diamond.API.Bot;
using Diamond.GUI.Pages;

using Discord.Interactions;

using Microsoft.Extensions.DependencyInjection;

using ScriptsLibV2.Util;

namespace Diamond.GUI
{
	/// <summary>
	/// Interaction logic for AppWindow.xaml
	/// </summary>
	public partial class AppWindow : Window
	{
		private readonly DiamondBot _bot;
		private readonly IServiceProvider _serviceProvider;

		public AppWindow(DiamondBot bot, IServiceProvider serviceProvier)
		{
			InitializeComponent();

			_bot = bot;
			_serviceProvider = serviceProvier;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			// Initialize bot
			_bot.Initialize();
			InteractionService interactionService = new InteractionService(_bot.Client.Rest);
			_bot.Client.Ready += new Func<Task>(async () =>
			{
				await interactionService.AddModulesAsync(Utils.GetAssemblyByName("DiamondAPI"), _serviceProvider);
				await interactionService.RegisterCommandsGloballyAsync();
			});
			_bot.Client.SlashCommandExecuted += async (socketInteraction) =>
			{
				SocketInteractionContext context = new SocketInteractionContext(_bot.Client, socketInteraction);
				await interactionService.ExecuteCommandAsync(context, _serviceProvider);
			};

			// Windows events
			Closing += new CancelEventHandler((se, ev) =>
			{
				_bot.StopAsync().GetAwaiter();
			});

			// Set frames
			frame_main.Navigate(_serviceProvider.GetRequiredService<MainPanelPage>());
			frame_logs.Navigate(_serviceProvider.GetRequiredService<LogsPanelPage>());
			frame_remote.Navigate(_serviceProvider.GetRequiredService<RemotePanelPage>());
			frame_settings.Navigate(_serviceProvider.GetRequiredService<SettingsPanelPage>());
		}
	}
}
