#region Usings
using System;
using System.Windows.Forms;

using static DiamondGui.Controls;
using static DiamondGui.Core;
using static DiamondGui.Functions;
using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
	public partial class Main : Form
    {
        public Main()
        {
			try
			{
				InitializeComponent();
				MainForm = this;
				this.Load += new EventHandler((_Sender, _Event) => MainLoad());
				this.FormClosing += new FormClosingEventHandler((_Sender, _Event) => MainClosing());
			}
			catch (Exception _Exception)
			{
				ShowException(_Exception, "Main.Main()");
			}
        }







		private void button_start_Click(object sender, EventArgs e)
        {
			ButtonStart();
        }

		private void button_revealToken_Click(object sender, EventArgs e)
		{
			ButtonRevealToken();
		}

		private void Main_Load(object sender, EventArgs e)
		{

		}
	}
}
