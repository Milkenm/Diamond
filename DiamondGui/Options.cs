#region Usings
using System;
using System.Windows.Forms;

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
		#endregion Options







		private void checkBox_revealToken_CheckedChanged(object sender, EventArgs e) => RevealToken(); // CheckBox 'Reveal Token'

		private void button_save_Click(object sender, EventArgs e) => OptionsSave(); // Button 'Save'

		private void button_cancel_Click(object sender, EventArgs e) => this.Hide(); // Button 'Cancel'
	}
}
