#region Usings
using System;
using System.Windows.Forms;
using static DiamondGui.Functions;
#endregion Usings



namespace DiamondGui
{
    static class Program
    {
        [STAThread] static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Main());
            }
            catch (Exception _Exception)
            {
                ShowException(_Exception);
            }
        }
    }
}
