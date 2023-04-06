using System;
using System.Windows;
using System.Windows.Controls;

using Diamond.API;
using Diamond.API.Bot;
using Diamond.API.Data;

using ScriptsLibV2.Extensions;

using MessageBox = System.Windows.MessageBox;

namespace Diamond.GUI.Pages;
/// <summary>
/// Interaction logic for SettingsPanelPage.xaml
/// </summary>
public partial class SettingsPanelPage : Page
{
	private readonly IServiceProvider _serviceProvider;
	private readonly DiamondDatabase _database;
	private readonly DiamondBot _bot;

	public SettingsPanelPage(DiamondDatabase database, DiamondBot bot)
	{
		InitializeComponent();

		_database = database;
		_bot = bot;
	}

	private void Page_Loaded(object sender, RoutedEventArgs e)
	{
		passwordBox_token.Password = Utils.GetSetting(_database, "Token", false);
		passwordBox_openaiApiKey.Password = Utils.GetSetting(_database, "OpenapiApiKey", false);
		passwordBox_nightapiApiKey.Password = Utils.GetSetting(_database, "NightapiApiKey", false);
		textBox_debugGuildId.Text = Utils.GetSetting(_database, "DebugGuildId", false);
		textBox_debugChannelId.Text = Utils.GetSetting(_database, "DebugChannelId", false);
	}

	private void ButtonSave_Click(object sender, RoutedEventArgs e)
	{
		if (passwordBox_token.Password.IsEmpty()) return;

		Utils.SetSetting(_database, "Token", passwordBox_token.Password);
		Utils.SetSetting(_database, "OpenaiApiKey", passwordBox_openaiApiKey.Password);
		Utils.SetSetting(_database, "NightapiApiKey", passwordBox_nightapiApiKey.Password);
		Utils.SetSetting(_database, "DebugGuildId", !textBox_debugGuildId.Text.IsEmpty() ? Convert.ToUInt64(textBox_debugGuildId.Text) : string.Empty);
		Utils.SetSetting(_database, "DebugChannelId", !textBox_debugChannelId.Text.IsEmpty() ? Convert.ToUInt64(textBox_debugChannelId.Text) : string.Empty);

		_bot.RefreshSettings().Wait();
		MessageBox.Show("Bot settings updated!");
	}
}
