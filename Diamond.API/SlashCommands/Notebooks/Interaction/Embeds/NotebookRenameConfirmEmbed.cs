using Diamond.API.SlashCommands.Notebooks.Interaction.Buttons;
using Diamond.Data.Models.Notebooks;

using Discord;

namespace Diamond.API.SlashCommands.Notebooks.Interaction.Embeds
{
	public class NotebookRenameConfirmEmbed : BaseNotebookEmbed
	{
		public NotebookRenameConfirmEmbed(IInteractionContext context, Notebook notebook, string newName, int notebooksListStartingIndex)
			: base(NotebookRenamedEmbed.AUTHOR_TITLE, context)
		{
			this.Title = "Are you sure?";
			this.Description = $"Are you sure you want to rename the notebook '**{notebook.Name}**' to '**{newName}**'?";
			_ = this.AddButton(new ButtonNotebookRenameConfirmAttribute(notebook.Id).GetButtonBuilder())
				.AddButton(new ButtonNotebookRenameCancelAttribute(notebook.Id).GetButtonBuilder());
		}
	}
}
