using Discord;

using System;
using System.IO;
using System.Windows.Forms;

using static DiamondGui.Enums;
using static DiamondGui.Functions;
using static DiamondGui.Static;
using static ScriptsLib.Controls.Tweaks.SlTextBox;
using static ScriptsLib.Tools;

namespace DiamondGui.Forms
{
	public partial class Options : Form
	{
		#region Options

		public Options()
		{
			InitializeComponent();
		}

		private void Options_Load(object sender, EventArgs e)
		{
			textBox_token.Text = settings.BotToken;
			comboBox_logType.SelectedIndex = settings.LogTypeIndex;
			textBox_botUrl.Text = settings.BotUrl;
			textBox_discordUrl.Text = settings.DiscordUrl;
			textBox_adminId.Text = settings.AdminId.ToString();
			textBox_botPrefix.Text = settings.BotPrefix;
			checkBox_disableCommands.Checked = settings.DisableCommands;
			checkBox_allowPrefix.Checked = settings.AllowPrefix;
			checkBox_allowMention.Checked = settings.AllowMention;

			comboBox_activity.SelectedIndex = settings.ActivityIndex;
			textBox_activityName.Text = settings.Game;
			textBox_streamUrl.Text = settings.StreamUrl;

			textBox_domain.Text = settings.Domain;

			textBox_riotApi.Text = settings.RiotAPI;

			textBox_binaryPath.Text = settings.BinaryFilePath;
			textBox_binaryName.Text = settings.BinaryFileName;
		}

		private void Options_FormClosing(object sender, FormClosingEventArgs e)
		{
			CloseForm(this, e);
		}

		#endregion Options

		// CheckBox 'Reveal Token'
		private void checkBox_revealToken_CheckedChanged(object sender, EventArgs e)
		{
			OptionsReveal(checkBox_revealToken, textBox_token);
		}

		// Button 'Save'
		private void Save(object sender, EventArgs e)
		{
			// Save Settings
			settings.BotToken = optionsForm.textBox_token.Text;
			settings.LogTypeIndex = optionsForm.comboBox_logType.SelectedIndex;
			settings.Game = optionsForm.textBox_activityName.Text;
			settings.ActivityIndex = optionsForm.comboBox_activity.SelectedIndex;
			settings.StreamUrl = optionsForm.textBox_streamUrl.Text;
			settings.BotUrl = optionsForm.textBox_botUrl.Text;
			settings.DiscordUrl = optionsForm.textBox_discordUrl.Text;
			settings.Domain = optionsForm.textBox_domain.Text;
			settings.AdminId = Convert.ToUInt64(optionsForm.textBox_adminId.Text);
			settings.BotPrefix = optionsForm.textBox_botPrefix.Text;
			settings.AllowMention = optionsForm.checkBox_allowMention.Checked;
			settings.AllowPrefix = optionsForm.checkBox_allowPrefix.Checked;
			settings.DisableCommands = optionsForm.checkBox_disableCommands.Checked;
			settings.RiotAPI = optionsForm.textBox_riotApi.Text;
			if (Directory.Exists(textBox_binaryPath.Text))
			{
				settings.BinaryFilePath = textBox_binaryPath.Text;
			}
			settings.BinaryFileName = textBox_binaryName.Text;

			// Refresh
			LoadAPIKeys();
			diamondCore.SetGame(ActivityType.Playing, "lel");

			// Save Settings & Close
			settings.Save();
			optionsForm.Hide();
		}

		// Button 'Cancel'
		private void button_cancel_Click(object sender, EventArgs e)
		{
			// TextBox
			optionsForm.textBox_token.Text = settings.BotToken;
			optionsForm.textBox_activityName.Text = settings.Game;
			optionsForm.textBox_streamUrl.Text = settings.StreamUrl;
			optionsForm.textBox_botUrl.Text = settings.BotUrl;
			optionsForm.textBox_discordUrl.Text = settings.DiscordUrl;
			optionsForm.textBox_domain.Text = settings.Domain;
			optionsForm.textBox_adminId.Text = settings.AdminId.ToString();
			OnlyNumbersTextBox(optionsForm.textBox_adminId);
			optionsForm.textBox_botPrefix.Text = settings.BotPrefix;

			// ComboBox
			optionsForm.comboBox_logType.SelectedIndex = settings.LogTypeIndex;
			optionsForm.comboBox_activity.SelectedIndex = settings.ActivityIndex;

			// CheckBox
			optionsForm.checkBox_allowMention.Checked = settings.AllowMention;
			optionsForm.checkBox_disableCommands.Checked = settings.DisableCommands;
			optionsForm.checkBox_allowPrefix.Checked = settings.AllowPrefix;
			optionsForm.checkBox_allowMention.Checked = settings.AllowMention;

			// API Keys
			optionsForm.textBox_riotApi.Text = settings.RiotAPI;

			// File System
			optionsForm.textBox_binaryPath.Text = settings.BinaryFilePath;
			optionsForm.textBox_binaryName.Text = settings.BinaryFileName;

			Hide();
		}

		// LinkLabel 'Discord Dev'
		private void linkLabel_discordDev_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			OptionsOpenLink(Link.DiscordDev);
		}

		// LinkLabel 'Riot Dev'
		private void linkLabel_riotDev_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			OptionsOpenLink(Link.RiotDev);
		}

		// CheckBox 'Disable Commands'
		private void checkBox_disableCommands_CheckedChanged(object sender, EventArgs e)
		{
			checkBox_allowPrefix.Enabled = !checkBox_disableCommands.Checked;
			checkBox_allowMention.Enabled = !checkBox_disableCommands.Checked;
		}

		// CheckBox 'Reveal Riot API'
		private void checkBox_riotApi_CheckedChanged(object sender, EventArgs e)
		{
			OptionsReveal(checkBox_riotApi, textBox_riotApi);
		}

		private void OptionsReveal(CheckBox cb, TextBox tb)
		{
			tb.PasswordChar = cb.Checked ? '•' : '\0';
		}

		internal static void OptionsOpenLink(Link Link)
		{
			switch (Link)
			{
				case Link.DiscordDev:
					ExecuteCmdCommand("START https://discordapp.com/developers/applications/");
					break;

				case Link.RiotDev:
					ExecuteCmdCommand("START https://developer.riotgames.com/");
					break;
			}
		}

		private void button_searchPath_Click(object sender, EventArgs e)
		{
			using (FolderBrowserDialog dialog = new FolderBrowserDialog())
			{
				DialogResult result = dialog.ShowDialog();

				if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
				{
					textBox_binaryPath.Text = dialog.SelectedPath;
				}
			}
		}
	}
}