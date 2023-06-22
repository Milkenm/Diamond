using System.Runtime.InteropServices;

namespace Diamond.API.Util
{
	public static partial class PInvoke
	{
		[DllImport("kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetPhysicallyInstalledSystemMemory(out long TotalMemoryInKilobytes);
	}
}
