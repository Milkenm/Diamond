using Diamond.Data;
using Diamond.Data.Models.Notebooks;

using Discord;

namespace Diamond.API.SlashCommands.Notebooks.Interaction
{
    public class NotebookDeleteConfirmEmbed : BaseNotebookEmbed
    {
        public NotebookDeleteConfirmEmbed(IInteractionContext context, Notebook notebook)
            : base("Confirm Delete Notebook", context)
        {
            Title = "Are you sure?";
            Description = $"Are you sure you want to delete the notebook '**{notebook.Name}**'?\n\n:warning: **__If you delete the notebook, it cannot be recovered and all pages will be lost.__**";
            _ = AddButton(new ButtonNotebookDeleteConfirmAttribute(notebook.Id).GetButtonBuilder())
                .AddButton(new ButtonNotebookDeleteCancelAttribute(notebook.Id).GetButtonBuilder());
        }

        public NotebookDeleteConfirmEmbed(IInteractionContext context, long notebookId)
            : this(context, SearchNotebookById(notebookId))
        { }

        private static Notebook SearchNotebookById(long notebookId)
        {
            using DiamondContext db = new DiamondContext();
            Notebook notebook = Notebook.GetNotebook(notebookId, db);

            return notebook;
        }
    }
}
