using System;

using Diamond.Data;
using Diamond.Data.Models.Notebooks;

using Discord;

namespace Diamond.API.SlashCommands.Notebooks.Interaction
{
    public class PageViewEmbed : BaseNotebookEmbed
    {
        private PageViewEmbed(IInteractionContext context, long? pageId, long notebookId)
            : base("Notebook Page", context)
        {
            if (pageId != null)
            {
                _ = AddButton(new ButtonPageEditorAttribute((long)pageId, notebookId).GetButtonBuilder("Edit Page", ButtonStyle.Primary));
                _ = AddButton(new ButtonPageDeleteAttribute((long)pageId).GetButtonBuilder());
            }
            _ = AddButton(new ButtonNotebookOpenAttribute(notebookId).GetButtonBuilder("Go Back", ButtonStyle.Secondary));
        }

        public PageViewEmbed(IInteractionContext context, long pageId, long notebookId)
            : this(context, (long?)pageId, notebookId)
        {
            using DiamondContext db = new DiamondContext();

            NotebookPage page = NotebookPage.GetNotebookPage(pageId, db);

            Title = page.Title;
            Description = $"```{page.Content}```";
        }

        public PageViewEmbed(IInteractionContext context, Exception exception, long notebookId)
            : this(context, (long?)null, notebookId)
        {
            Title = "Error";
            Description = exception.Message;
        }
    }
}
