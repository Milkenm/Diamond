#region Usings
using System;
using System.Diagnostics;
using System.Windows.Forms;
#endregion Usings



namespace DiamondGui
{
	internal static partial class Debug
	{
		internal static void TestCodeSpeed(Action Code)
		{
			Stopwatch _StopWatch = new Stopwatch();

			_StopWatch.Start();
			Code.Invoke();
			_StopWatch.Stop();

			MessageBox.Show("Took: " + _StopWatch.ElapsedMilliseconds + " milliseconds.");
		}
	}
}
