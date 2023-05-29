using Discord.Interactions;

namespace Diamond.API.Attributes
{
	public class DSlashCommandAttribute : SlashCommandAttribute
	{
		public DSlashCommandAttribute(string name, string description, bool ignoreGroupNames = false, RunMode runMode = RunMode.Default)
			: base(name,
#if DEBUG
				  "[DEBUG]: " +
#endif
				  description, ignoreGroupNames, runMode)
		{ }
	}
}
