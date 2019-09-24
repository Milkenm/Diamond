#region Usings
using System;
#endregion Usings



namespace DiamondGui
{
	internal static partial class Functions
	{
		internal static void ShowException(Exception Exception, string Title)
		{
			ScriptsLib.Tools.ShowException(Exception, Title);
		}
	}
}
