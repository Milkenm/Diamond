using Diamond.Brainz;

using System;
using System.Threading.Tasks;
using System.Windows;

namespace Diamond.WPFCore.GUI
{
	public partial class Main : Window
	{
		public Bot bot;

		public Main()
		{
			this.InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\";
			bot = new Bot(desktopPath + "bot_config.json", desktopPath + "db_config.json");
			bot.Client.Log += new Func<Discord.LogMessage, Task>(async (msg) => await Task.Run(() => this.Dispatcher.Invoke(() => listBox_log.Items.Add(msg))));
		}

		private void Button_start_Click(object sender, RoutedEventArgs e)
		{
			if (!bot.IsRunning)
			{
				bot.StartAsync();
				button_start.Content = "Stop";
			}
			else
			{
				bot.StopAsync();
				button_start.Content = "Start";
			}
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (bot.IsRunning)
			{
				bot.StopAsync();
			}
		}

		private void button_settings_Click(object sender, RoutedEventArgs e)
		{
			Settings s = new Settings(bot);
			s.Show();
		}
	}
}
