using System.Windows;
using System.Windows.Controls;

namespace Diamond.GUI.Pages;
/// <summary>
/// Interaction logic for SettingsPanelPage.xaml
/// </summary>
public partial class SettingsPanelPage : Page
{
	public SettingsPanelPage()
	{
		InitializeComponent();
	}

	private void Page_Loaded(object sender, RoutedEventArgs e)
	{
		passwordBox_token.Password = AppWindow.GetInstance().Bot.Token;
	}

	private void ButtonSave_Click(object sender, RoutedEventArgs e)
	{
		AppWindow.GetInstance().Bot.SetToken(passwordBox_token.Password);
		MessageBox.Show("Bot token updated!");
	}
}
