using Diamond.Data.Models.Notebooks;

using Discord;

namespace Diamond.API.SlashCommands.Notebooks.Interaction
{
    public class NotebookRenamedEmbed : BaseNotebookEmbed
    {
        public const string AUTHOR_TITLE = "Rename Notebook";

        public NotebookRenamedEmbed(IInteractionContext context, Notebook notebook, string oldName)
            : base(AUTHOR_TITLE, context)
        {
            Title = "Notebook renamed!";
            Description = $"The notebook '**{oldName}**' was renamed to '**{notebook.Name}**'.";
            _ = AddButton(new ButtonNotebookOpenAttribute(notebook.Id).GetButtonBuilder());
        }
    }
}
