#region Usings
using System.Windows.Forms;
using static DiamondGui.Functions;
using static DiamondGui.LpLauncher;
#endregion Usings



namespace DiamondGui.Forms
{
	public partial class LpLauncher : Form
	{
		public LpLauncher()
		{
			InitializeComponent();
		}

		private void button_gradientGenerator_Click(object sender, System.EventArgs e)
		{
			ShowLp(LittleProgram.GradientGenerator);
		}

		private void LpLauncher_FormClosing(object sender, FormClosingEventArgs e)
		{
			CloseForm(this, e);
		}
	}
}
