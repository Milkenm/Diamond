using System.Windows;
using System.Windows.Controls;

using Diamond.API;
using Diamond.API.Bot;
using Diamond.API.Data;

namespace Diamond.GUI.Pages;
/// <summary>
/// Interaction logic for SettingsPanelPage.xaml
/// </summary>
public partial class SettingsPanelPage : Page
{
	private readonly DiamondDatabase _database;
	private readonly DiamondBot _bot;
	private readonly AppWindow _appWindow;

	public SettingsPanelPage(DiamondDatabase database, DiamondBot bot, AppWindow appWindow)
	{
		InitializeComponent();

		_database = database;
		_bot = bot;
		_appWindow = appWindow;
	}

	private void Page_Loaded(object sender, RoutedEventArgs e)
	{
		passwordBox_token.Password = Utils.GetSetting(_database, Utils.GetSettingString(Utils.Setting.Token));
		passwordBox_openaiApiKey.Password = Utils.GetSetting(_database, Utils.GetSettingString(Utils.Setting.OpenAI_API_Key));
		passwordBox_nightapiApiKey.Password = Utils.GetSetting(_database, Utils.GetSettingString(Utils.Setting.NightAPI_API_Key));
		textBox_debugGuildId.Text = Utils.GetSetting(_database, Utils.GetSettingString(Utils.Setting.DebugGuildID));
		textBox_debugChannelId.Text = Utils.GetSetting(_database, Utils.GetSettingString(Utils.Setting.DebugChannelID));
	}

	private void ButtonSave_Click(object sender, RoutedEventArgs e)
	{
		Utils.SetSetting(_database, Utils.GetSettingString(Utils.Setting.Token), passwordBox_token.Password);
		Utils.SetSetting(_database, Utils.GetSettingString(Utils.Setting.OpenAI_API_Key), passwordBox_openaiApiKey.Password);
		Utils.SetSetting(_database, Utils.GetSettingString(Utils.Setting.NightAPI_API_Key), passwordBox_nightapiApiKey.Password);
		Utils.SetSetting(_database, Utils.GetSettingString(Utils.Setting.DebugGuildID), textBox_debugGuildId.Text);
		Utils.SetSetting(_database, Utils.GetSettingString(Utils.Setting.DebugChannelID), textBox_debugChannelId.Text);

		_bot.RefreshSettings().Wait();

		_appWindow.ToggleUI(Utils.AreSettingsValid(_database));
	}
}
