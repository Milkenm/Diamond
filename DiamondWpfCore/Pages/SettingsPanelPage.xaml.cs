using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

using Diamond.API;
using Diamond.API.Bot;

using ScriptsLibV2.Extensions;

using MessageBox = System.Windows.MessageBox;

namespace Diamond.GUI.Pages;
/// <summary>
/// Interaction logic for SettingsPanelPage.xaml
/// </summary>
public partial class SettingsPanelPage : Page
{
	private readonly IServiceProvider _serviceProvider;
	private readonly AppSettings _appSettings;
	private readonly DiamondBot _bot;

	public SettingsPanelPage(AppSettings appSettings, DiamondBot bot)
	{
		InitializeComponent();

		_appSettings = appSettings;
		_bot = bot;
	}

	private void Page_Loaded(object sender, RoutedEventArgs e)
	{
		passwordBox_token.Password = _appSettings.Settings.Token;
		textBox_cachePath.Text = _appSettings.Settings.CacheFolderPath;
		passwordBox_openaiApiKey.Password = _appSettings.Settings.OpenaiApiKey;
		passwordBox_nightapiApiKey.Password = _appSettings.Settings.NightapiApiKey;
		textBox_debugGuildId.Text = _appSettings.Settings.DebugGuildId != null ? _appSettings.Settings.DebugGuildId.ToString() : string.Empty;
		textBox_debugChannelId.Text = _appSettings.Settings.DebugChannelId != null ? _appSettings.Settings.DebugChannelId.ToString() : string.Empty;
	}

	private void ButtonSave_Click(object sender, RoutedEventArgs e)
	{
		if (!passwordBox_token.Password.IsEmpty())
		{
			_appSettings.Settings.Token = passwordBox_token.Password;
			_appSettings.Settings.CacheFolderPath = textBox_cachePath.Text;
			_appSettings.Settings.OpenaiApiKey = passwordBox_openaiApiKey.Password;
			_appSettings.Settings.NightapiApiKey = passwordBox_nightapiApiKey.Password;
			_appSettings.Settings.DebugGuildId = !textBox_debugGuildId.Text.IsEmpty() ? Convert.ToUInt64(textBox_debugGuildId.Text) : null;
			_appSettings.Settings.DebugChannelId = !textBox_debugChannelId.Text.IsEmpty() ? Convert.ToUInt64(textBox_debugChannelId.Text) : null;

			_bot.RefreshSettings().Wait();
			MessageBox.Show("Bot settings updated!");
		}
	}

	private void ButtonCache_Click(object sender, RoutedEventArgs e)
	{
		FolderBrowserDialog folderDialog = new FolderBrowserDialog();
		DialogResult result = folderDialog.ShowDialog();
		if (result == DialogResult.OK && textBox_cachePath.Text != folderDialog.SelectedPath)
		{
			textBox_cachePath.Text = folderDialog.SelectedPath;
		}
	}
}
