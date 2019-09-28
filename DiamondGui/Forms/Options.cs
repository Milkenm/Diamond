#region Usings
using System;
using System.Windows.Forms;
using static DiamondGui.Functions;
using static DiamondGui.Controls;
using static DiamondGui.Core;
#endregion Usings



namespace DiamondGui
{
	public partial class Options : Form
	{
		#region Options
		public Options() => InitializeComponent();

		private void Options_Load(object sender, EventArgs e) => OptionsLoad();

		private void Options_FormClosing(object sender, FormClosingEventArgs e) => HideForm(this, e);
		#endregion Options







		private void checkBox_revealToken_CheckedChanged(object sender, EventArgs e) => RevealToken(); // CheckBox 'Reveal Token'

		private void button_save_Click(object sender, EventArgs e) => OptionsSave(); // Button 'Save'

		private void button_cancel_Click(object sender, EventArgs e) => this.Hide(); // Button 'Cancel'

		private void linkLabel_discordDev_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => OpenLink(Link.DiscordDev); // LinkLabel 'Discord Dev'

		private void linkLabel_riotDev_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => OpenLink(Link.RiotDev); // LinkLabel 'Riot Dev'
	}
}
