using Diamond.Brainz;

using System;
using System.Windows;
using System.Windows.Media.Imaging;

using static Diamond.Core.DiamondCore;

namespace Diamond.WPFCore.GUI
{
	/// <summary>
	/// Interaction logic for Settings.xaml
	/// </summary>
	public partial class Settings : Window
	{
		private readonly Bot Bot;

		public Settings(Bot bot)
		{
			this.InitializeComponent();

			Bot = bot;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			textBox_token.Text = Bot.Configuration.Token;
		}

		private void SaveSettings(object sender, RoutedEventArgs e)
		{
			Config cfg = new Config();
			cfg.Token = textBox_token.Text;
			Bot.UpdateConfig(cfg).GetAwaiter();

			this.Close();
		}
	}
}
