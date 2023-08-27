using Diamond.Data.Models.Notebooks;

using Discord;

namespace Diamond.API.SlashCommands.Notebooks.Interaction
{
    public class NotebookRenameConfirmEmbed : BaseNotebookEmbed
    {
        public NotebookRenameConfirmEmbed(IInteractionContext context, Notebook notebook, string newName, int notebooksListStartingIndex)
            : base(NotebookRenamedEmbed.AUTHOR_TITLE, context)
        {
            Title = "Are you sure?";
            Description = $"Are you sure you want to rename the notebook '**{notebook.Name}**' to '**{newName}**'?";
            _ = AddButton(new ButtonNotebookRenameConfirmAttribute(notebook.Id).GetButtonBuilder())
                .AddButton(new ButtonNotebookRenameCancelAttribute(notebook.Id).GetButtonBuilder());
        }
    }
}
