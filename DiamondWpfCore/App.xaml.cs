using System;
using System.Windows;

using Diamond.API;
using Diamond.API.Bot;
using Diamond.GUI.Pages;

using Discord.WebSocket;

using Microsoft.Extensions.DependencyInjection;

using ScriptsLibV2;
using ScriptsLibV2.Util;

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
			Database db = new Database(Utils.GetInstallationFolder() + @"\DiamondDB.db");

			_serviceProvider = new ServiceCollection()
				.AddSingleton(this)
				.AddSingleton(db)
				.AddSingleton<AppSettings>()
				.AddSingleton<AppFolder>()
				.AddSingleton<DiamondBot>()
				.AddSingleton<AppWindow>()
				.AddSingleton<OpenAIAPI>()
				.AddSingleton<DiscordSocketClient>()
				.AddSingleton<MainPanelPage>()
				.AddSingleton<LogsPanelPage>()
				.AddSingleton<RemotePanelPage>()
				.AddSingleton<SettingsPanelPage>()
				.BuildServiceProvider();
		}

		private void Application_Startup(object sender, StartupEventArgs e)
		{
			AppWindow app = _serviceProvider.GetRequiredService<AppWindow>();
			app.Show();
		}
	}
}
