using System;
using System.Runtime.InteropServices;

namespace ChillinRoomGMG
{
	internal static class WinAPI
	{
		[DllImport("user32.dll")]
		internal static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

		[DllImport("user32.dll")]
		internal static extern bool SetForegroundWindow(IntPtr hWnd);

		[DllImport("user32.dll")]
		internal static extern bool ShowWindow(IntPtr hWnd, WindowShowStyle nCmdShow);

		internal enum WindowShowStyle : uint
		{
			Hide = 0,
			ShowNormal = 1,
			ShowMinimized = 2,
			Maximize = 3,
			ShowNormalNoActivate = 4,
			Show = 5,
			Minimize = 6,
			ShowMinNoActivate = 7,
			ShowNoActivate = 8,
			Restore = 9,
			ShowDefault = 10,
			ForceMinimized = 11
		}

		[DllImport("user32.dll")]
		internal static extern bool HideCaret(IntPtr hWnd);

		[DllImport("user32.dll")]
		internal static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

		[DllImport("user32.dll")]
		internal static extern bool ReleaseCapture();

		[DllImport("user32.dll")]
		internal static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

		[StructLayout(LayoutKind.Sequential)]
		internal struct LASTINPUTINFO
		{
			public uint cbSize;
			public int dwTime;
		}
	}
}