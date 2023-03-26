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
		private SlashCommandsManager SlashCommandsManager;

		private static AppWindow Instance;
		public static AppWindow GetInstance() => Instance ?? new AppWindow();
		public AppWindow()
		{
			InitializeComponent();
			Instance = this;
		}

		public DBot Bot { get; private set; }

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			// Initialize database
			Database db = new Database(Utils.GetInstallationFolder() + @"\DiamondDB.db");

			// Initialize bot
			Bot = new DBot(db);
			Bot.Initialize();
			SlashCommandsManager = new SlashCommandsManager(Bot.Client);
			// Bot events
			Bot.Client.Log += MainPanelPage.GetInstance().Log;
			Bot.Client.Ready += Client_Ready;

			// Windows events
			Closing += new CancelEventHandler((se, ev) =>
			{
				Bot.StopAsync().GetAwaiter();
			});
		}

		private async Task Client_Ready()
		{
			await SlashCommandsManager.RemoveAllCommands();
			await SlashCommandsManager.AddSlashCommandsAsync(new AvatarCommand(), new EmojiCommand(), new CalculateCommand(), new RandomNumberCommand(), new DownloadEmojis(), new NsfwCommand());
		}
	}
}
