using System;
using System.Windows;

using Diamond.API;
using Diamond.API.APIs;
using Diamond.API.Data;
using Diamond.GUI.Pages;

using Discord;
using Discord.Interactions;
using Discord.WebSocket;

using Microsoft.Extensions.DependencyInjection;

namespace Diamond.GUI
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private readonly IServiceProvider _serviceProvider;

		public App()
		{
			DiscordSocketClient client = new DiscordSocketClient(new DiscordSocketConfig()
			{
				LogLevel = LogSeverity.Info,
			});

			this._serviceProvider = new ServiceCollection()
				// Bot stuff
				.AddSingleton(this)
				.AddSingleton(client)
				.AddSingleton<DiamondDatabase>()
				.AddSingleton<InteractionService>()
				// Tabs (Windows)
				.AddSingleton<AppWindow>()
				.AddSingleton<MainPanelPage>()
				.AddSingleton<LogsPanelPage>()
				.AddSingleton<RemotePanelPage>()
				.AddSingleton<SettingsPanelPage>()
				.AddSingleton<LavalinkPanelPage>()
				// APIs
				.AddSingleton<OpenAIAPI>()
				.AddSingleton<CsgoBackpack>()
				.AddSingleton<OpenMeteoGeocoding>()
				.AddSingleton<OpenMeteoWeather>()
				// Lavalink
				.AddSingleton<Lava>()
				.BuildServiceProvider();
		}

		private void Application_Startup(object sender, StartupEventArgs e)
		{
			AppWindow app = this._serviceProvider.GetRequiredService<AppWindow>();
			app.Show();
		}
	}
}
