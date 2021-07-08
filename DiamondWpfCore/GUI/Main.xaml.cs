using Diamond.Brainz.Data;

using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

using static Diamond.Brainz.Brainz;

namespace Diamond.WPFCore.GUI
{
	public partial class Main : Window
	{
		public Brainz.Brainz brainz;

		public Main()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			brainz = new Brainz.Brainz();
			brainz.Log += new LogEvent(async (msg) => await LogToListBox(msg).ConfigureAwait(false));
			brainz.Error += new ErrorEvent(async (ex) => await LogToListBox(ex.Message).ConfigureAwait(false));
			brainz.Start();
		}

		public async Task LogToListBox(object message)
		{
			await Task.Run(() => Dispatcher.Invoke(() => listBox_log.Items.Add(message))).ConfigureAwait(false);
		}

		private void Button_start_Click(object sender, RoutedEventArgs e)
		{
			if (GlobalData.DiamondCore != null)
			{
				if (!GlobalData.DiamondCore.IsRunning)
				{
					GlobalData.DiamondCore.Start();
					button_start.Content = "Stop";
				}
				else
				{
					GlobalData.DiamondCore.Stop();
					button_start.Content = "Start";
				}
			}
			else
			{
				string token = brainz.GetBotToken();
				if (token != null)
				{
					brainz.LoadCore();
				}
				else
				{
					MessageBox.Show("Token is not set.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			GlobalData.Bot.DataTablesManager(DataTableAction.Save);
			if (GlobalData.DiamondCore != null)
			{
				GlobalData.DiamondCore.Stop();
			}
		}

		private void button_settings_Click(object sender, RoutedEventArgs e)
		{
			Settings s = new Settings(this);
			s.Show();
		}
	}
}
