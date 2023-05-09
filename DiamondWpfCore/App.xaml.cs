using System;
using System.Windows;

using Diamond.API;
using Diamond.API.APIs;
using Diamond.API.Bot;
using Diamond.API.Data;
using Diamond.API.Stuff;
using Diamond.GUI.Pages;

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
			_serviceProvider = new ServiceCollection()
				.AddSingleton(this)
				.AddSingleton<DiamondBot>()
				.AddSingleton<AppWindow>()
				.AddSingleton<OpenAIAPI>()
				.AddSingleton<MainPanelPage>()
				.AddSingleton<LogsPanelPage>()
				.AddSingleton<RemotePanelPage>()
				.AddSingleton<SettingsPanelPage>()
				.AddSingleton<LavalinkPanelPage>()
				.AddSingleton<DiamondDatabase>()
				.AddSingleton<CsgoBackpack>()
				.AddSingleton<OpenMeteoGeocoding>()
				.AddSingleton<OpenMeteoWeather>()
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
