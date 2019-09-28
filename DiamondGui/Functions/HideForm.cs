#region Usings
using System.Windows.Forms;
#endregion Usings



namespace DiamondGui
{
	internal static partial class Functions
	{
		internal static void HideForm(Form Form, FormClosingEventArgs Event)
		{
			Event.Cancel = true;
			Form.Hide();
		}
	}
}