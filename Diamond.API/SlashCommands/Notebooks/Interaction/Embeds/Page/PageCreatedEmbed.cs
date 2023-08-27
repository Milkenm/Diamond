using System;

using Diamond.Data.Models.Notebooks;

using Discord;

namespace Diamond.API.SlashCommands.Notebooks.Interaction
{
    public class PageCreatedEmbed : BaseNotebookEmbed
    {
        private PageCreatedEmbed(IInteractionContext context, long? notebookId)
            : base("Create Notebook Page", context)
        {
            _ = notebookId == null
                ? AddButton(new ButtonNotebookGotoListAttribute().GetButtonBuilder())
                : AddButton(new ButtonNotebookOpenAttribute((long)notebookId).GetButtonBuilder("Go To Notebook"));
        }

        public PageCreatedEmbed(IInteractionContext context, NotebookPage page, Notebook notebook)
            : this(context, notebook.Id)
        {
            Title = "Notebook page created";
            Description = $"Created the page '**{page.Title}**'{(notebook != null ? $" and added it to the '**{notebook.Name}**' notebook" : "")}.";
        }

        public PageCreatedEmbed(IInteractionContext context, Exception exception)
            : this(context, (long?)null)
        {
            Title = "Error";
            Description = exception.Message;
        }
    }
}
