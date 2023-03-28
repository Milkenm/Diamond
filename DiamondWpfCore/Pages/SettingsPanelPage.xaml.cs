using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

using Diamond.API.Bot;

using ScriptsLibV2.Extensions;

using MessageBox = System.Windows.MessageBox;

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
		textBox_cachePath.Text = _bot.GetCacheFolder().Path;
	}

	private void ButtonSave_Click(object sender, RoutedEventArgs e)
	{
		if (!passwordBox_token.Password.IsEmpty())
		{
			_bot.SetToken(passwordBox_token.Password);
			MessageBox.Show("Bot token updated!");
		}
	}

	private void ButtonCache_Click(object sender, RoutedEventArgs e)
	{
		FolderBrowserDialog folderDialog = new FolderBrowserDialog();
		DialogResult result = folderDialog.ShowDialog();
		if (result == DialogResult.OK && textBox_cachePath.Text != folderDialog.SelectedPath)
		{
			textBox_cachePath.Text = folderDialog.SelectedPath;
			MessageBox.Show("Cache folder updated.");
		}
	}
}
