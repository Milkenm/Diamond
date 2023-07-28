using Discord;

namespace Diamond.API.SlashCommands.Notebooks.Embeds
{
	public class NoNotebooksEmbed : BaseNotebookEmbed
	{
		public NoNotebooksEmbed(IInteractionContext context)
			: base("No Notebooks", context)
		{
			this.Title = "You don't have any notebooks";
			this.Description = "You can create one with `/notebooks create <name> [description]`.";
		}
	}
}
