using System.Windows;
using System.Windows.Controls;

using Diamond.API.Bot;

namespace Diamond.GUI.Pages;
/// <summary>
/// Interaction logic for SettingsPanelPage.xaml
/// </summary>
public partial class SettingsPanelPage : Page
{
	private readonly DiamondBot _bot;

	public SettingsPanelPage(DiamondBot bot)
	{
		InitializeComponent();

		_bot = bot;
	}

	private void Page_Loaded(object sender, RoutedEventArgs e)
	{
		passwordBox_token.Password = _bot.Token;
	}

	private void ButtonSave_Click(object sender, RoutedEventArgs e)
	{
		_bot.SetToken(passwordBox_token.Password);
		MessageBox.Show("Bot token updated!");
	}
}
