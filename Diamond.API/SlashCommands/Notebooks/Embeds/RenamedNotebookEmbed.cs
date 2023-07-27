using Diamond.Data.Models.Notebooks;

using Discord;

namespace Diamond.API.SlashCommands.Notebooks.Embeds
{
	/*public class RenamedNotebookEmbed : NotebookEmbed
	{
		public const string AUTHOR_TITLE = "Rename Notebook";

		public RenamedNotebookEmbed(IInteractionContext context, Notebook notebook, string oldName, int notebooksListStartingIndex)
			: base(AUTHOR_TITLE, context)
		{
			this.Title = "Notebook renamed!";
			this.Description = $"The notebook '**{oldName}**' was renamed to '**{notebook.Name}**'.";
			this.Component = new ComponentBuilder()
				.WithButton(new ButtonBuilder("Go back", $"{NotebookComponentIds.BUTTON_NOTEBOOK_RENAME_GOBACK}:{notebook.Id},{notebooksListStartingIndex}"))
				.Build();
		}
	}*/
}
