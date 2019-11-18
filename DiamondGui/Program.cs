#region Usings
using System;
using System.Windows.Forms;

using static DiamondGui.Functions;
#endregion Usings



namespace DiamondGui
{
	internal static class Program
	{
		[STAThread]
		private static void Main()
		{
			try
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new Forms.Main());
			}
			catch (Exception _Exception)
			{
				ShowException(_Exception);
			}
		}
	}
}
