using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

using Diamond.API.Data;

using Newtonsoft.Json;

using ScriptsLibV2.Extensions;

using static Diamond.API.Data.DiamondContext;

using MessageBox = System.Windows.Forms.MessageBox;

namespace Diamond.GUI.Pages;
/// <summary>
/// Interaction logic for SettingsPanelPage.xaml
/// </summary>
public partial class SettingsPanelPage : Page
{
	private readonly AppWindow _appWindow;

	public SettingsPanelPage(AppWindow appWindow)
	{
		this._appWindow = appWindow;

		this.InitializeComponent();
	}

	private void Page_Initialized(object sender, EventArgs e)
	{
		using DiamondContext db = new DiamondContext();

#if DEBUG
		this.checkBox_ignoreDebugChannel.Visibility = Visibility.Collapsed;
#else
		this.checkBox_ignoreDebugChannel.IsChecked = Convert.ToBoolean(db.GetSetting(ConfigSetting.IgnoreDebugChannels, false.ToString()));
#endif
		this.passwordBox_token.Password = db.GetSetting(ConfigSetting.Token);
		this.passwordBox_openaiApiKey.Password = db.GetSetting(ConfigSetting.OpenAI_API_Key);
		this.passwordBox_nightapiApiKey.Password = db.GetSetting(ConfigSetting.NightAPI_API_Key);
		this.passwordBox_riotApiKey.Password = db.GetSetting(ConfigSetting.RiotAPI_Key);
		this.textBox_debugGuildId.Text = db.GetSetting(ConfigSetting.DebugGuildID);
		foreach (string debugChannelId in db.GetSetting(ConfigSetting.DebugChannelsID, string.Empty).Split(','))
		{
			if (debugChannelId.IsEmpty()) continue;
			this.listBox_debugChannels.Items.Add(debugChannelId);
		}
	}

	private async void ButtonSave_Click(object sender, RoutedEventArgs e)
	{
		using DiamondContext db = new DiamondContext();

		SettingsJSON settingsJson = this.GetSettingsObject();

		db.SetSetting(ConfigSetting.Token, settingsJson.Token).Wait();
		db.SetSetting(ConfigSetting.OpenAI_API_Key, settingsJson.OpenaiApiKey).Wait();
		db.SetSetting(ConfigSetting.NightAPI_API_Key, settingsJson.NightapiApiKey).Wait();
		db.SetSetting(ConfigSetting.RiotAPI_Key, settingsJson.RiotApiKey).Wait();
		db.SetSetting(ConfigSetting.DebugGuildID, settingsJson.DebugGuildId).Wait();
#if RELEASE
			db.SetSetting(ConfigSetting.IgnoreDebugChannels, settingsJson.IgnoreDebugChannels).Wait();
#endif
		db.SetSetting(ConfigSetting.DebugChannelsID, string.Join(",", settingsJson.DebugChannelsId)).Wait();

		await db.SaveAsync();

		this._appWindow.ToggleUI(db.AreSettingsValid());
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
				this.passwordBox_token.Password = settingsObject.Token;
				this.passwordBox_openaiApiKey.Password = settingsObject.OpenaiApiKey;
				this.passwordBox_nightapiApiKey.Password = settingsObject.NightapiApiKey;
				this.passwordBox_riotApiKey.Password = settingsObject.RiotApiKey;
				this.textBox_debugGuildId.Text = settingsObject.DebugGuildId;
				this.checkBox_ignoreDebugChannel.IsChecked = settingsObject.IgnoreDebugChannels;
				this.listBox_debugChannels.Items.Clear();
				foreach (string debugChannelId in settingsObject.DebugChannelsId)
				{
					_ = this.listBox_debugChannels.Items.Add(debugChannelId);
				}
			}
			catch (Exception ex)
			{
				_ = MessageBox.Show($"There was a problem loading the settings file.\nError: {ex.Message}", "Error loading settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
			SettingsJSON settingsJson = this.GetSettingsObject();

			string jsonString = JsonConvert.SerializeObject(settingsJson, Formatting.Indented);

			File.WriteAllText(saveFileDialog.FileName, jsonString);
		}
	}

	private void button_addDebugChannel_Click(object sender, RoutedEventArgs e)
	{
		string text = this.textBox_debugChannelId.Text;
		if (text.IsEmpty()) return;
		if (ulong.TryParse(text, out _))
		{
			_ = this.listBox_debugChannels.Items.Add(text);
			this.textBox_debugChannelId.Clear();
		}
	}

	private SettingsJSON GetSettingsObject()
	{
		List<string> debugChannelIds = new List<string>();
		foreach (string debugChannelId in this.listBox_debugChannels.Items)
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
		if (this.listBox_debugChannels.SelectedIndex == -1) return;

		int selectedIndex = this.listBox_debugChannels.SelectedIndex;
		this.listBox_debugChannels.Items.RemoveAt(selectedIndex);
		if (selectedIndex > 0)
		{
			this.listBox_debugChannels.SelectedIndex = selectedIndex - 1;
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
