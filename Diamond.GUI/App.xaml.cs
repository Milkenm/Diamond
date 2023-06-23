using System;
using System.Windows;

using Diamond.API;
using Diamond.API.APIs.Csgo;
using Diamond.API.APIs.LeagueOfLegends;
using Diamond.API.APIs.Minecraft;
using Diamond.API.APIs.OpenAi;
using Diamond.API.APIs.Pokemon;
using Diamond.API.APIs.Weather;
using Diamond.API.ConsoleCommands;
using Diamond.API.Helpers;
using Diamond.GUI.Pages;

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
			this._serviceProvider = new ServiceCollection()
				// Bot stuff
				.AddSingleton(this)
				.AddSingleton<DiamondClient>()
				// Tabs (Windows)
				.AddSingleton<AppWindow>()
				.AddSingleton<MainPanelPage>()
				.AddSingleton<LogsPanelPage>()
				.AddSingleton<RemotePanelPage>()
				.AddSingleton<SettingsPanelPage>()
				.AddSingleton<LavalinkPanelPage>()
				.AddSingleton<GuildsPanelPage>()
				// APIs
				.AddSingleton<OpenAIAPI>()
				.AddSingleton<CsgoBackpack>()
				.AddSingleton<OpenMeteoGeocoding>()
				.AddSingleton<OpenMeteoWeather>()
				.AddSingleton<LeagueOfLegendsDataDragonAPI>()
				.AddSingleton<LeagueOfLegendsAPI>()
				.AddSingleton<McsrvstatAPI>()
				.AddSingleton<PokemonAPI>()
				// Lavalink
				.AddSingleton<Lava>()
				// Console commands
				.AddSingleton<ConsoleCommandsManager>()
				.BuildServiceProvider();
		}

		private void Application_Startup(object sender, StartupEventArgs e)
		{
			AppWindow app = this._serviceProvider.GetRequiredService<AppWindow>();
			app.Show();
		}
	}
}
