using Diamond.API.SlashCommands.Notebooks.Interaction.Buttons;

using Discord;

namespace Diamond.API.SlashCommands.Notebooks.Interaction.Embeds
{
	public class NoNotebooksEmbed : BaseNotebookEmbed
	{
		public NoNotebooksEmbed(IInteractionContext context)
			: base("No Notebooks", context)
		{
			this.Title = "You don't have any notebooks";
			this.Description = "You can create one with `/notebooks create <name> [description]` or by clicking the button below.";
			_ = this.AddButton(new ButtonNotebookEditorAttribute().GetButtonBuilder());
		}
	}
}
