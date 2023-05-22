using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

using Diamond.API.Data;

using Newtonsoft.Json;

using ScriptsLibV2.Extensions;

using static Diamond.API.Data.DiamondDatabase;

using MessageBox = System.Windows.Forms.MessageBox;

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
#if DEBUG
		checkBox_ignoreDebugChannel.Visibility = Visibility.Collapsed;
#else
		this.checkBox_ignoreDebugChannel.IsChecked = Convert.ToBoolean(_database.GetSetting(ConfigSetting.IgnoreDebugChannels, false.ToString()));
#endif
		this.passwordBox_token.Password = this._database.GetSetting(ConfigSetting.Token);
		this.passwordBox_openaiApiKey.Password = this._database.GetSetting(ConfigSetting.OpenAI_API_Key);
		this.passwordBox_nightapiApiKey.Password = this._database.GetSetting(ConfigSetting.NightAPI_API_Key);
		this.passwordBox_riotApiKey.Password = this._database.GetSetting(ConfigSetting.RiotAPI_Key);
		this.textBox_debugGuildId.Text = this._database.GetSetting(ConfigSetting.DebugGuildID);
		foreach (string debugChannelId in this._database.GetSetting(ConfigSetting.DebugChannelsID, string.Empty).Split(','))
		{
			if (debugChannelId.IsEmpty()) continue;
			this.listBox_debugChannels.Items.Add(debugChannelId);
		}
	}

	private void ButtonSave_Click(object sender, RoutedEventArgs e)
	{
		SettingsJSON settingsJson = GetSettingsObject();

		this._database.SetSetting(ConfigSetting.Token, settingsJson.Token).Wait();
		this._database.SetSetting(ConfigSetting.OpenAI_API_Key, settingsJson.OpenaiApiKey).Wait();
		this._database.SetSetting(ConfigSetting.NightAPI_API_Key, settingsJson.NightapiApiKey).Wait();
		this._database.SetSetting(ConfigSetting.RiotAPI_Key, settingsJson.RiotApiKey).Wait();
		this._database.SetSetting(ConfigSetting.DebugGuildID, settingsJson.DebugGuildId).Wait();
#if RELEASE
		this._database.SetSetting(ConfigSetting.IgnoreDebugChannels, settingsJson.IgnoreDebugChannels).Wait();
#endif
		this._database.SetSetting(ConfigSetting.DebugChannelsID, string.Join(",", settingsJson.DebugChannelsId)).Wait();

		this._appWindow.ToggleUI(this._database.AreSettingsValid());
	}

	private void ButtonLoadJson_Click(object sender, RoutedEventArgs e)
	{
		// Create open dialog
		OpenFileDialog openFileDialog = new OpenFileDialog()
		{
			Filter = "JSON File (*.json)|*.json",
			DefaultExt = "json",
			CheckFileExists = true,
			CheckPathExists = true,
		};
		// Open file
		if (openFileDialog.ShowDialog() == DialogResult.OK)
		{
			string json = File.ReadAllText(openFileDialog.FileName);

			try
			{
				SettingsJSON settingsObject = JsonConvert.DeserializeObject<SettingsJSON>(json);

				// Load settings
				settingsObject.Token = this.passwordBox_token.Password;
				settingsObject.OpenaiApiKey = this.passwordBox_openaiApiKey.Password;
				settingsObject.NightapiApiKey = this.passwordBox_nightapiApiKey.Password;
				settingsObject.RiotApiKey = this.passwordBox_riotApiKey.Password;
				settingsObject.DebugGuildId = this.textBox_debugGuildId.Text;
				settingsObject.IgnoreDebugChannels = (bool)this.checkBox_ignoreDebugChannel.IsChecked;
				listBox_debugChannels.Items.Clear();
				foreach (string debugChannelId in settingsObject.DebugChannelsId)
				{
					listBox_debugChannels.Items.Add(debugChannelId);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"There was a problem loading the settings file.\nError: {ex.Message}", "Error loading settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}

	private void ButtonExportJson_Click(object sender, RoutedEventArgs e)
	{
		// Create save dialog
		SaveFileDialog saveFileDialog = new SaveFileDialog()
		{
			FileName = "settings.json",
			Filter = "JSON File (*.json)|*.json",
			DefaultExt = "json",
			CheckWriteAccess = true,
			CheckFileExists = true,
		};
		// Save file
		if (saveFileDialog.ShowDialog() == DialogResult.OK)
		{
			SettingsJSON settingsJson = GetSettingsObject();

			string jsonString = JsonConvert.SerializeObject(settingsJson, Formatting.Indented);

			File.WriteAllText(saveFileDialog.FileName, jsonString);
		}
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

	private SettingsJSON GetSettingsObject()
	{
		List<string> debugChannelIds = new List<string>();
		foreach (string debugChannelId in listBox_debugChannels.Items)
		{
			debugChannelIds.Add(debugChannelId);
		}

		return new SettingsJSON()
		{
			Token = this.passwordBox_token.Password,
			OpenaiApiKey = this.passwordBox_openaiApiKey.Password,
			NightapiApiKey = this.passwordBox_nightapiApiKey.Password,
			RiotApiKey = this.passwordBox_riotApiKey.Password,
			DebugGuildId = this.textBox_debugGuildId.Text,
			IgnoreDebugChannels = (bool)this.checkBox_ignoreDebugChannel.IsChecked,
			DebugChannelsId = debugChannelIds,
		};
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

	public class SettingsJSON
	{
		[JsonProperty("token")] public string Token { get; set; }
		[JsonProperty("openai_api_key")] public string OpenaiApiKey { get; set; }
		[JsonProperty("nightapi_api_key")] public string NightapiApiKey { get; set; }
		[JsonProperty("riot_api_key")] public string RiotApiKey { get; set; }
		[JsonProperty("debug_guild_id")] public string DebugGuildId { get; set; }
		[JsonProperty("ignore_debug_channels")] public bool IgnoreDebugChannels { get; set; }
		[JsonProperty("debug_channels_id")] public List<string> DebugChannelsId { get; set; }
	}
}
