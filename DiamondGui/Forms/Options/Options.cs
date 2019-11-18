#region Usings
using System;
using System.Windows.Forms;
using static DiamondGui.Main;
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







		private void checkBox_revealToken_CheckedChanged(object sender, EventArgs e)
		{
			RevealToken(); // CheckBox 'Reveal Token'
		}

		private void button_save_Click(object sender, EventArgs e)
		{
			OptionsSave(); // Button 'Save'
		}

		private void button_cancel_Click(object sender, EventArgs e)
		{
			Hide(); // Button 'Cancel'
		}

		private void linkLabel_discordDev_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			OpenLink(Link.DiscordDev); // LinkLabel 'Discord Dev'
		}

		private void linkLabel_riotDev_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			OpenLink(Link.RiotDev); // LinkLabel 'Riot Dev'
		}
	}
}
