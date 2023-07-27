using Diamond.Data.Models.Notebooks;

using Discord;

namespace Diamond.API.SlashCommands.Notebooks.Embeds
{
	/*public class RenameNotebookConfirmEmbed : NotebookEmbed
	{
		public RenameNotebookConfirmEmbed(IInteractionContext context, Notebook notebook, string newName, int notebooksListStartingIndex)
			: base(RenamedNotebookEmbed.AUTHOR_TITLE, context)
		{
			this.Title = "Are you sure?";
			this.Description = $"Are you sure you want to rename the notebook '**{notebook.Name}**' to '**{newName}**'?";
			this.Component = new ComponentBuilder()
				.WithButton("Rename", $"{NotebookComponentIds.BUTTON_NOTEBOOK_RENAME_CONFIRM}:{notebook.Id},{newName},{notebooksListStartingIndex}", ButtonStyle.Success, row: 0)
				.WithButton("Cancel", $"{NotebookComponentIds.BUTTON_NOTEBOOK_RENAME_CANCEL}:{notebook.Id},{notebooksListStartingIndex}", ButtonStyle.Secondary, row: 0)
				.Build();
		}
	}*/
}
