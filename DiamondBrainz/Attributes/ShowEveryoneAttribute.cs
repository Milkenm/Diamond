using Discord.Interactions;

namespace Diamond.API.Attributes
{
	public class ShowEveryoneAttribute : SummaryAttribute
	{
		public ShowEveryoneAttribute() : base("show-everyone", "Show the command output to everyone.") { }
	}
}
