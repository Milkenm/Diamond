using DiamondGui.Forms;
using DiamondGui.Properties;
using Diamond.Core;

namespace DiamondGui
{
	internal static class Static
	{
		// Forms
		internal static Main mainForm;

		internal static Options optionsForm;
		internal static PrivateChat privateChatForm;

		// Discord
		internal static DiamondCore diamondCore;

		// Settings
		internal static Settings settings = new Settings();
	}
}