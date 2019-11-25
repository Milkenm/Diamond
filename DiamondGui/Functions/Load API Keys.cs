#region Usings
using ScriptsLib.Network.APIs;

using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
	internal static partial class Functions
	{
		internal static void LoadAPIKeys()
		{
			RiotAPI.ApiKey = Settings.RiotAPI;
		}
	}
}
