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
		internal static Forms.LpLauncher LpLauncherForm;

		// Little Programs
		internal static GradientGenerator LP_GradientGeneratorForm;

		// Discord
		internal static DiscordSocketClient Client;
		internal static CommandService Command;

		// Other Items
		internal static Settings Settings = new Settings();
	}
}