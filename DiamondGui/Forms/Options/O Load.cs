#region Usings
using System;

using static DiamondGui.Functions;
using static DiamondGui.Static;
using static ScriptsLib.Controls.Tweaks.SlTextBox;
#endregion Usings



namespace DiamondGui
{
	internal static partial class Options
	{
		internal static void OptionsLoad()
		{
			try
			{
				// TextBox
				OptionsForm.textBox_token.Text = Settings.BotToken;
				OptionsForm.textBox_activityName.Text = Settings.Game;
				OptionsForm.textBox_streamUrl.Text = Settings.StreamUrl;
				OptionsForm.textBox_botUrl.Text = Settings.BotUrl;
				OptionsForm.textBox_discordUrl.Text = Settings.DiscordUrl;
				OptionsForm.textBox_domain.Text = Settings.Domain;
				OptionsForm.textBox_adminId.Text = Settings.AdminId.ToString();
				OnlyNumbersTextBox(OptionsForm.textBox_adminId);
				OptionsForm.textBox_botPrefix.Text = Settings.BotPrefix;

				// ComboBox
				OptionsForm.comboBox_logType.SelectedIndex = Settings.LogTypeIndex;
				OptionsForm.comboBox_activity.SelectedIndex = Settings.ActivityIndex;

				// CheckBox
				OptionsForm.checkBox_allowMention.Checked = Settings.AllowMention;
				OptionsForm.checkBox_disableCommands.Checked = Settings.DisableCommands;
				OptionsForm.checkBox_allowPrefix.Checked = Settings.AllowPrefix;
				OptionsForm.checkBox_allowMention.Checked = Settings.AllowMention;

				// API Keys
				OptionsForm.textBox_riotApi.Text = Settings.RiotAPI;
			}
			catch (Exception _Exception) { ShowException(_Exception); }
		}
	}
}