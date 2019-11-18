#region Usings
using static DiamondGui.EventsModule;
using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
	internal static partial class Discord
	{
		internal static void LoadEvents()
		{
			Client.MessageReceived += MessageReceived;
		}
	}
}
