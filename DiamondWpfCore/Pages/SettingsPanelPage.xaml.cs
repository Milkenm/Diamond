using System.Windows;
using System.Windows.Controls;

using Diamond.API.Data;

using static Diamond.API.Data.DiamondDatabase;

namespace Diamond.GUI.Pages;
/// <summary>
/// Interaction logic for SettingsPanelPage.xaml
/// </summary>
public partial class SettingsPanelPage : Page
{
	private readonly DiamondDatabase _database;
	private readonly AppWindow _appWindow;

	public SettingsPanelPage(DiamondDatabase database, AppWindow appWindow)
	{
		this.InitializeComponent();

		this._database = database;
		this._appWindow = appWindow;
	}

	private void Page_Loaded(object sender, RoutedEventArgs e)
	{
		this.passwordBox_token.Password = this._database.GetSetting(ConfigSetting.Token);
		this.passwordBox_openaiApiKey.Password = this._database.GetSetting(ConfigSetting.OpenAI_API_Key);
		this.passwordBox_nightapiApiKey.Password = this._database.GetSetting(ConfigSetting.NightAPI_API_Key);
		this.textBox_debugGuildId.Text = this._database.GetSetting(ConfigSetting.DebugGuildID);
		this.textBox_debugChannelId.Text = this._database.GetSetting(ConfigSetting.DebugChannelID);
	}

	private void ButtonSave_Click(object sender, RoutedEventArgs e)
	{
		this._database.SetSetting(ConfigSetting.Token, this.passwordBox_token.Password);
		this._database.SetSetting(ConfigSetting.OpenAI_API_Key, this.passwordBox_openaiApiKey.Password);
		this._database.SetSetting(ConfigSetting.NightAPI_API_Key, this.passwordBox_nightapiApiKey.Password);
		this._database.SetSetting(ConfigSetting.DebugGuildID, this.textBox_debugGuildId.Text);
		this._database.SetSetting(ConfigSetting.DebugChannelID, this.textBox_debugChannelId.Text);

		this._appWindow.ToggleUI(this._database.AreSettingsValid());
	}
}
