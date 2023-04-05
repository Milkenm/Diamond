using System;
using System.IO;
using System.Windows;

using Diamond.API;
using Diamond.API.Bot;
using Diamond.API.Data;
using Diamond.API.Stuff;
using Diamond.GUI.Pages;

using Discord.WebSocket;

using Microsoft.Extensions.DependencyInjection;

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
			Database db = new Database(Path.Join(Utils.GetInstallationFolder() + @"\Data\DiamondDB.db"));

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
				.AddSingleton<DiamondContext>()
				.AddSingleton<CsgoItemsContext>()
				.AddSingleton<PollsContext>()
				.AddSingleton<CsgoBackpack>()
				.BuildServiceProvider();
		}

		private void Application_Startup(object sender, StartupEventArgs e)
		{
			AppWindow app = _serviceProvider.GetRequiredService<AppWindow>();
			app.Show();
		}
	}
}
