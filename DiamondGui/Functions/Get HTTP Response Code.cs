#region Usings

using System.Net;

#endregion Usings

// # = #
// https://stackoverflow.com/questions/3614034/system-net-webexception-http-status-code/21159116
// # = #

namespace DiamondGui
{
	internal static partial class Functions
	{
		internal static int? GetHttpResponseCode(WebException ex)
		{
			return ex.Response is HttpWebResponse resp ? (int)resp.StatusCode : (int?)null;
		}
	}
}