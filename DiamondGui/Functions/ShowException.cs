#region Usings
using System;
using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
    internal static partial class Functions
    {
        internal static void ShowException(Exception Exception, string Title)
        {
			Settings.Exceptions += 1; Settings.Save();

            ScriptsLib.Tools.ShowException(Exception, Title);
        }
    }
}
