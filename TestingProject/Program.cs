#region Usings
using System;
using System.Windows.Forms;
#endregion Usings



namespace TestingProject
{
	internal static class Program
    {
        [STAThread]
        internal static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GradientGenerator());
        }
    }
}
