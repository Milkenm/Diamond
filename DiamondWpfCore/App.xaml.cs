using System;
using System.Windows;

using Diamond.API;
using Diamond.API.APIs;
using Diamond.API.Bot;
using Diamond.API.Data;
using Diamond.API.Stuff;
using Diamond.GUI.Pages;

using Discord.Interactions;

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
			DiamondDatabase database = new DiamondDatabase();
			DiamondBot bot = new DiamondBot(database);

			_serviceProvider = new ServiceCollection()
				// Bot stuff
				.AddSingleton(this)
				.AddSingleton(bot)
				.AddSingleton(new InteractionService(bot.Client.Rest))
				.AddSingleton(database)
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
			AppWindow app = _serviceProvider.GetRequiredService<AppWindow>();
			app.Show();
		}
	}
}
