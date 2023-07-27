using Diamond.Data;
using Diamond.Data.Models.Notebooks;

using Discord;

namespace Diamond.API.SlashCommands.Notebooks.Embeds
{
	/*public class DeleteNotebookConfirmEmbed : NotebookEmbed
	{
		public DeleteNotebookConfirmEmbed(IInteractionContext context, Notebook notebook, int notebooksListStartingIndex)
			: base(DeletedNotebookEmbed.AUTHOR_TITLE, context)
		{
			this.Title = "Are you sure?";
			this.Description = $"Are you sure you want to delete the notebook '**{notebook.Name}**'?\n\n:warning: **__If you delete it, it cannot be recovered and all pages will be lost.__**";
			this.Component = new ComponentBuilder()
				.WithButton("Delete permanently", $"{NotebookComponentIds.BUTTON_NOTEBOOK_DELETE_CONFIRM}:{notebook.Id},{notebooksListStartingIndex}", ButtonStyle.Danger, row: 0)
				.WithButton("Cancel", $"{NotebookComponentIds.BUTTON_NOTEBOOKPAGES_GOTO_LIST}:{notebook.Id},{notebooksListStartingIndex}", ButtonStyle.Secondary, row: 0)
				.Build();
		}

		public DeleteNotebookConfirmEmbed(IInteractionContext context, long notebookId, int notebooksListStartingIndex)
			: this(context, SearchNotebookById(notebookId), notebooksListStartingIndex)
		{ }

		private static Notebook SearchNotebookById(long notebookId)
		{
			using DiamondContext db = new DiamondContext();
			Notebook notebook = Notebook.GetNotebook(notebookId, db);

			return notebook;
		}
	}*/
}
