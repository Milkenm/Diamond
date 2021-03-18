using System.Windows;

namespace Diamond.WPFCore.GUI
{
	/// <summary>
	/// Interaction logic for Settings.xaml
	/// </summary>
	public partial class Settings : Window
	{
		private Main main;

		public Settings(Main m)
		{
			InitializeComponent();

			main = m;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			textBox_token.Text = main.brainz.GetBotToken();
		}

		private void SaveSettings(object sender, RoutedEventArgs e)
		{
			if (textBox_token.Text != null)
			{
				main.brainz.SetBotToken(textBox_token.Text);
			}

			this.Close();
		}
	}
}
