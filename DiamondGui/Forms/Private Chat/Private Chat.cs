#region Usings
using System;
using System.Windows.Forms;
using static DiamondGui.Functions;
using static DiamondGui.PrivateChat;
#endregion Usings



namespace DiamondGui.Forms
{
	public partial class PrivateChat : Form
	{
		public PrivateChat()
		{
			InitializeComponent();
		}

		private void button_startChat_Click(object sender, EventArgs e)
		{
			OpenUserTab();
		}

		private void PrivateChat_FormClosing(object sender, FormClosingEventArgs e)
		{
			CloseForm(this, e);
		}
	}
}