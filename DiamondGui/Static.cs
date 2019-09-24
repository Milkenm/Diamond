#region Usings
using DiamondGui.Properties;

using Discord.Commands;
using Discord.WebSocket;
#endregion Usings



namespace DiamondGui
{
	internal static class Static
    {
		internal static Main MainForm;

        internal static DiscordSocketClient Client;
		internal static CommandService Command;

		internal static Settings Settings = new Settings();
    }
}
