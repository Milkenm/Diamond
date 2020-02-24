#region Usings

using DiamondGui.LittlePrograms;
using DiamondGui.Properties;

using Discord.Commands;
using Discord.WebSocket;

#endregion Usings

namespace DiamondGui
{
	internal static class Static
	{
		// Forms
		internal static Forms.Main MainForm;

		internal static Forms.Options OptionsForm;
		internal static Forms.Statistics StatisticsForm;
		internal static Forms.LPLauncher LpLauncherForm;
		internal static Forms.PrivateChat PrivateChatForm;

		// Little Programs
		internal static GradientGenerator LP_GradientGeneratorForm;

		// Discord
		internal static DiscordSocketClient Client;

		internal static CommandService Command;

		// Settings
		internal static Settings Settings = new Settings();
	}
}