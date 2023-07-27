using System;

using Diamond.Data.Models.Notebooks;

using Discord;

namespace Diamond.API.SlashCommands.Notebooks.Embeds
{
	/*public class CreatedNotebookEmbed : NotebookEmbed
	{
		public const string AUTHOR_TITLE = "Create Notebook";

		public CreatedNotebookEmbed(IInteractionContext context, Notebook notebook)
			: base(AUTHOR_TITLE, context)
		{
			this.Title = "Notebook created!";
			this.Description = $"You created a notebook called '**{notebook.Name}**'.";
			this.AddBackButton(notebook.Id);
		}

		public CreatedNotebookEmbed(IInteractionContext context, Exception exception)
			: base(AUTHOR_TITLE, context)
		{
			this.Title = "Error";
			this.Description = exception.Message;
			this.AddBackButton(null);
		}

		private void AddBackButton(long? notebookId)
		{
			this.Component = new ComponentBuilder()
				.WithButton("Go Back", $"{NotebookComponentIds.BUTTON_NOTEBOOK_GOTO_LIST}{(notebookId != null ? $":{notebookId}" : "")}", ButtonStyle.Primary)
				.Build();
		}
	}*/
}
