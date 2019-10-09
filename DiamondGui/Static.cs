#region Usings
using DiamondGui.Properties;

using Discord.Commands;
using Discord.WebSocket;
#endregion Usings



namespace DiamondGui
{
	internal static class Static
    {
		// Forms
		internal static Main MainForm;
		internal static Options OptionsForm;
		internal static Statistics StatisticsForm;

		// Discord
        internal static DiscordSocketClient Client;
		internal static CommandService Command;

		// Other Items
		internal static Settings Settings = new Settings();
    }
}
