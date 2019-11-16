#region Usings
using DiamondGui.Forms;
using DiamondGui.LittlePrograms;
using DiamondGui.Properties;

using Discord.Commands;
using Discord.WebSocket;

using SharpLink;
#endregion Usings



namespace DiamondGui
{
	internal static class Static
    {
		// Forms
		internal static Main MainForm;
		internal static Options OptionsForm;
		internal static Statistics StatisticsForm;
		internal static LpLauncher LpLauncherForm;

		// Little Programs
		internal static GradientGenerator LP_GradientGeneratorForm;

		// Discord
        internal static DiscordSocketClient Client;
		internal static CommandService Command;

        // Lavalink
        internal static LavalinkManager LavalinkManager;

        // Other Items
        internal static Settings Settings = new Settings();
    }
}