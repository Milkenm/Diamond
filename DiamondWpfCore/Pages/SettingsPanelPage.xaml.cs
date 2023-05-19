using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using Diamond.API.Data;

using ScriptsLibV2.Extensions;

using static Diamond.API.Data.DiamondDatabase;

using SUtils = ScriptsLibV2.Util.Utils;

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
		this._database = database;
		this._appWindow = appWindow;

		this.InitializeComponent();
	}

	private void Page_Initialized(object sender, EventArgs e)
	{
		if (SUtils.IsDebugEnabled())
		{
			checkBox_ignoreDebugChannel.Visibility = Visibility.Collapsed;
		}
		else
		{
			this.checkBox_ignoreDebugChannel.IsChecked = Convert.ToBoolean(_database.GetSetting(ConfigSetting.IgnoreDebugChannels, false.ToString()));
		}

		this.passwordBox_token.Password = this._database.GetSetting(ConfigSetting.Token);
		this.passwordBox_openaiApiKey.Password = this._database.GetSetting(ConfigSetting.OpenAI_API_Key);
		this.passwordBox_nightapiApiKey.Password = this._database.GetSetting(ConfigSetting.NightAPI_API_Key);
		this.textBox_debugGuildId.Text = this._database.GetSetting(ConfigSetting.DebugGuildID);
		foreach (string debugChannelId in this._database.GetSetting(ConfigSetting.DebugChannelsID, string.Empty).Split(','))
		{
			if (debugChannelId.IsEmpty()) continue;
			this.listBox_debugChannels.Items.Add(debugChannelId);
		}
	}

	private void ButtonSave_Click(object sender, RoutedEventArgs e)
	{
		this._database.SetSetting(ConfigSetting.Token, this.passwordBox_token.Password);
		this._database.SetSetting(ConfigSetting.OpenAI_API_Key, this.passwordBox_openaiApiKey.Password);
		this._database.SetSetting(ConfigSetting.NightAPI_API_Key, this.passwordBox_nightapiApiKey.Password);
		this._database.SetSetting(ConfigSetting.RiotAPI_Key, this.passwordBox_riotApiKey.Password);
		this._database.SetSetting(ConfigSetting.DebugGuildID, this.textBox_debugGuildId.Text);
		List<string> debugChannelIds = new List<string>();
		foreach (string debugChannelId in listBox_debugChannels.Items)
		{
			debugChannelIds.Add(debugChannelId);
		}
		this._database.SetSetting(ConfigSetting.DebugChannelsID, string.Join(",", debugChannelIds));
		if (!SUtils.IsDebugEnabled())
		{
			this._database.SetSetting(ConfigSetting.IgnoreDebugChannels, checkBox_ignoreDebugChannel.IsChecked);
		}

		this._appWindow.ToggleUI(this._database.AreSettingsValid());
	}

	private void button_addDebugChannel_Click(object sender, RoutedEventArgs e)
	{
		string text = textBox_debugChannelId.Text;
		if (text.IsEmpty()) return;

		if (ulong.TryParse(text, out ulong channelId))
		{
			this.listBox_debugChannels.Items.Add(text);
			textBox_debugChannelId.Clear();
		}
	}

	private void button_removeDebugChannel_Click(object sender, RoutedEventArgs e)
	{
		if (listBox_debugChannels.SelectedIndex == -1) return;

		int selectedIndex = listBox_debugChannels.SelectedIndex;
		listBox_debugChannels.Items.RemoveAt(selectedIndex);
		if (selectedIndex > 0)
		{
			listBox_debugChannels.SelectedIndex = selectedIndex - 1;
		}
	}
}
