using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

using Diamond.API;
using Diamond.API.Bot;
using Diamond.API.SlashCommands.Discord;
using Diamond.API.SlashCommands.Moderation;
using Diamond.API.SlashCommands.NSFW;
using Diamond.API.SlashCommands.Tools;
using Diamond.GUI.Pages;

using ScriptsLibV2;
using ScriptsLibV2.Util;

namespace Diamond.GUI
{
	/// <summary>
	/// Interaction logic for AppWindow.xaml
	/// </summary>
	public partial class AppWindow : Window
	{
		public AppWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			// Initialize database
			Database db = new Database(Utils.GetInstallationFolder() + @"\DiamondDB.db");

			// Initialize bot
			DiamondBot bot = new DiamondBot(db);
			bot.Initialize();
			// Bot events
			bot.Client.Ready += new Func<Task>(async () =>
			{
				SlashCommandsManager slashCommandsManager = new SlashCommandsManager(bot.Client);
				await Task.Run(async () =>
				{
					await slashCommandsManager.AddSlashCommandsAsync(new AvatarCommand(), new EmojiCommand(), new CalculateCommand(), new RandomNumberCommand(), new DownloadEmojis(), new NsfwCommand(), new RandomPasswordCommand());
				});
			});

			// Windows events
			Closing += new CancelEventHandler((se, ev) =>
			{
				bot.StopAsync().GetAwaiter();
			});

			// Set frames
			frame_main.Navigate(new MainPanelPage(bot));
			frame_logs.Navigate(new LogsPanelPage());
			frame_remote.Navigate(new RemotePanelPage());
			frame_settings.Navigate(new SettingsPanelPage(bot));
		}
	}
}
