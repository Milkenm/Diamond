using Diamond.API.SlashCommands.Notebooks.Interaction.Buttons;
using Diamond.Data.Models.Notebooks;

using Discord;

namespace Diamond.API.SlashCommands.Notebooks.Interaction.Embeds
{
	public class NotebookRenamedEmbed : BaseNotebookEmbed
	{
		public const string AUTHOR_TITLE = "Rename Notebook";

		public NotebookRenamedEmbed(IInteractionContext context, Notebook notebook, string oldName)
			: base(AUTHOR_TITLE, context)
		{
			this.Title = "Notebook renamed!";
			this.Description = $"The notebook '**{oldName}**' was renamed to '**{notebook.Name}**'.";
			_ = this.AddButton(new ButtonNotebookOpenAttribute(notebook.Id).GetButtonBuilder());
		}
	}
}
