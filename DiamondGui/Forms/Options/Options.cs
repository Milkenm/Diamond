#region Usings
using System;
using System.Windows.Forms;
using static DiamondGui.Functions;
using static DiamondGui.Options;
#endregion Usings



namespace DiamondGui.Forms
{
	public partial class Options : Form
	{
		#region Options
		public Options()
		{
			InitializeComponent();
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
		private void button_save_Click(object sender, EventArgs e)
		{
			OptionsSave();
		}

		// Button 'Cancel'
		private void button_cancel_Click(object sender, EventArgs e)
		{
			OptionsLoad();
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
			try
			{
				checkBox_allowPrefix.Enabled = !checkBox_disableCommands.Checked;
				checkBox_allowMention.Enabled = !checkBox_disableCommands.Checked;
			}
			catch (Exception ex) { ShowException(ex); }
		}

		// CheckBox 'Reveal Riot API'
		private void checkBox_riotApi_CheckedChanged(object sender, EventArgs e)
		{
			OptionsReveal(checkBox_riotApi, textBox_riotApi);
		}
	}
}
