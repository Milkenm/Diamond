using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

using Diamond.API;
using Diamond.API.Data;

using Discord;

using Newtonsoft.Json;

using ScriptsLibV2.Extensions;

using static Diamond.API.Data.DiamondContext;

using MessageBox = System.Windows.Forms.MessageBox;
using SUtils = ScriptsLibV2.Util.Utils;

namespace Diamond.GUI.Pages;
/// <summary>
/// Interaction logic for SettingsPanelPage.xaml
/// </summary>
public partial class SettingsPanelPage : Page
{
	private readonly DiamondClient _client;
	private readonly AppWindow _appWindow;

	public SettingsPanelPage(DiamondClient client, AppWindow appWindow)
	{
		this._client = client;
		this._appWindow = appWindow;

		this.InitializeComponent();
	}

	private void Page_Initialized(object sender, EventArgs e)
	{
		using DiamondContext db = new DiamondContext();

		// Load options for activities comboBox
		_ = this.comboBox_activityType.Items.Add("None");
		foreach (ActivityType activity in Enum.GetValues(typeof(ActivityType)))
		{

			if (activity is ActivityType.CustomStatus or ActivityType.Competing) continue;
			_ = this.comboBox_activityType.Items.Add(activity);
		}

		SettingsJSON settings = new SettingsJSON()
		{
			Token = db.GetSetting(ConfigSetting.Token),
			DevToken = db.GetSetting(ConfigSetting.DevToken),
			ActivityType = db.GetSetting(ConfigSetting.ActivityType, string.Empty),
			ActivityText = db.GetSetting(ConfigSetting.ActivityText, string.Empty),
			ActivityStreamURL = db.GetSetting(ConfigSetting.ActivityStreamURL, string.Empty),
			OpenaiApiKey = db.GetSetting(ConfigSetting.OpenAI_API_Key),
			NightapiApiKey = db.GetSetting(ConfigSetting.NightAPI_API_Key),
			RiotApiKey = db.GetSetting(ConfigSetting.RiotAPI_Key),
			DebugGuildId = db.GetSetting(ConfigSetting.DebugGuildID),
			IgnoreDebugChannels = Convert.ToBoolean(db.GetSetting(ConfigSetting.IgnoreDebugChannels, false.ToString())),
			DebugChannelsId = db.GetSetting(ConfigSetting.DebugChannelsID, string.Empty).Split(',').ToList(),
		};
		this.LoadSettingsObject(settings);
	}

	private async void ButtonSave_Click(object sender, RoutedEventArgs e)
	{
		using DiamondContext db = new DiamondContext();

		SettingsJSON settingsJson = this.GetSettingsObject();

		// Bot stuff
		await db.SetSettingAsync(ConfigSetting.Token, settingsJson.Token);
		await db.SetSettingAsync(ConfigSetting.DevToken, settingsJson.DevToken);
		await db.SetSettingAsync(ConfigSetting.ActivityType, this.comboBox_activityType.SelectedItem.ToString());
		await db.SetSettingAsync(ConfigSetting.ActivityText, this.textBox_activityText.Text);
		await db.SetSettingAsync(ConfigSetting.ActivityStreamURL, this.textBox_activityStreamUrl.Text);
		// API keys
		await db.SetSettingAsync(ConfigSetting.OpenAI_API_Key, settingsJson.OpenaiApiKey);
		await db.SetSettingAsync(ConfigSetting.NightAPI_API_Key, settingsJson.NightapiApiKey);
		// Debug stuff
		await db.SetSettingAsync(ConfigSetting.RiotAPI_Key, settingsJson.RiotApiKey);
		await db.SetSettingAsync(ConfigSetting.DebugGuildID, settingsJson.DebugGuildId);
		if (!SUtils.IsDebugEnabled())
		{
			await db.SetSettingAsync(ConfigSetting.IgnoreDebugChannels, settingsJson.IgnoreDebugChannels);
		}
		await db.SetSettingAsync(ConfigSetting.DebugChannelsID, string.Join(",", settingsJson.DebugChannelsId));

		await db.SaveAsync();

		await Utils.SetClientActivityAsync(this._client);

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
				this.LoadSettingsObject(settingsObject);
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

	private void LoadSettingsObject(SettingsJSON settingsObject)
	{
		// Bot stuff
		this.passwordBox_token.Password = settingsObject.Token;
		this.passwordBox_devToken.Password = settingsObject.DevToken;
		if (settingsObject.ActivityType.IsEmpty())
		{
			this.comboBox_activityType.SelectedIndex = 0;
		}
		else
		{
			this.comboBox_activityType.SelectedItem = Enum.Parse(typeof(ActivityType), settingsObject.ActivityType);
		}
		this.textBox_activityText.Text = settingsObject.ActivityText;
		this.textBox_activityStreamUrl.Text = settingsObject.ActivityStreamURL;
		// API keys
		this.passwordBox_openaiApiKey.Password = settingsObject.OpenaiApiKey;
		this.passwordBox_nightapiApiKey.Password = settingsObject.NightapiApiKey;
		this.passwordBox_riotApiKey.Password = settingsObject.RiotApiKey;
		// Debug stuff
		this.textBox_debugGuildId.Text = settingsObject.DebugGuildId;
		if (SUtils.IsDebugEnabled())
		{
			this.checkBox_ignoreDebugChannel.Visibility = Visibility.Collapsed;
		}
		else
		{
			this.checkBox_ignoreDebugChannel.IsChecked = settingsObject.IgnoreDebugChannels;
		}
		this.listBox_debugChannels.Items.Clear();
		foreach (string debugChannelId in settingsObject.DebugChannelsId)
		{
			if (debugChannelId.IsEmpty()) continue;
			_ = this.listBox_debugChannels.Items.Add(debugChannelId);
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
			DevToken = this.passwordBox_devToken.Password,
			ActivityType = this.comboBox_activityType.SelectedItem.ToString(),
			ActivityText = this.textBox_activityText.Text,
			ActivityStreamURL = this.textBox_activityStreamUrl.Text,
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
		[JsonProperty("dev_token")] public string DevToken { get; set; }
		[JsonProperty("activity_type")] public string ActivityType { get; set; }
		[JsonProperty("activity_text")] public string ActivityText { get; set; }
		[JsonProperty("activity_stream_url")] public string ActivityStreamURL { get; set; }
		[JsonProperty("openai_api_key")] public string OpenaiApiKey { get; set; }
		[JsonProperty("nightapi_api_key")] public string NightapiApiKey { get; set; }
		[JsonProperty("riot_api_key")] public string RiotApiKey { get; set; }
		[JsonProperty("debug_guild_id")] public string DebugGuildId { get; set; }
		[JsonProperty("ignore_debug_channels")] public bool IgnoreDebugChannels { get; set; }
		[JsonProperty("debug_channels_id")] public List<string> DebugChannelsId { get; set; }
	}
}
