using System.Runtime.InteropServices;

namespace Diamond.API.Util
{
	public static partial class PInvoke
	{
		[LibraryImport("kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static partial bool GetPhysicallyInstalledSystemMemory(out long TotalMemoryInKilobytes);
	}
}
