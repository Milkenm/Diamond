using System;

using Discord;

namespace Diamond.API.SlashCommands.Notebooks.Embeds
{
	/*public class DeletedNotebookEmbed : NotebookEmbed
	{
		public const string AUTHOR_TITLE = "Delete Notebook";

		public DeletedNotebookEmbed(IInteractionContext context, string notebookName, int notebooksListStartingIndex = 0)
			: base(AUTHOR_TITLE, context)
		{
			this.AddBackButton(notebooksListStartingIndex);

			this.Title = "Notebook deleted";
			this.Description = $"The notebook '**{notebookName}**' was deleted.";
		}

		public DeletedNotebookEmbed(IInteractionContext context, Exception exception, int notebooksListStartingIndex = 0)
			: base(AUTHOR_TITLE, context)
		{
			this.AddBackButton(notebooksListStartingIndex);

			this.Title = "There was a problem deleting the notebook.";
			this.Description = exception.Message;
		}

		private void AddBackButton(int notebooksListStartingIndex)
		{
			this.Component = new ComponentBuilder()
				.WithButton("Go back", $"{NotebookComponentIds.BUTTON_NOTEBOOK_DELETE_GOBACK}:{notebooksListStartingIndex}", ButtonStyle.Primary, row: 0)
			.Build();
		}
	}*/
}
