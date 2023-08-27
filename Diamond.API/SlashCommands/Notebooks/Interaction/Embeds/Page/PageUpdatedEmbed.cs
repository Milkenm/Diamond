using System;

using Diamond.Data.Models.Notebooks;

using Discord;

namespace Diamond.API.SlashCommands.Notebooks.Interaction
{
    public class PageUpdatedEmbed : BaseNotebookEmbed
    {
        private PageUpdatedEmbed(IInteractionContext context, long pageId, long notebookId)
            : base("Update Page", context)
        {
            _ = AddButton(new ButtonPageOpenAttribute(pageId, notebookId).GetButtonBuilder("Go Back"));
        }

        public PageUpdatedEmbed(IInteractionContext context, NotebookPage page, string oldTitle, string oldContent, long notebookId)
            : this(context, page.Id, notebookId)
        {
            Title = "Page updated!";

            if (page.Title != oldTitle && page.Content == oldContent)
            {
                Description = $"You changed the title of the page to '**{page.Title}**'.";
            }
            else if (page.Content != oldContent && page.Title == oldTitle)
            {
                Description = $"You changed the content of the page.";
            }
            else if (page.Title != oldTitle && page.Content != oldContent)
            {
                Description = $"You changed the title of the page from '**{oldTitle}**' to '**{page.Title}**'.\nThe content of the page was also changed.";
            }
            else if (page.Title == oldTitle && page.Content == oldContent)
            {
                Description = $"Nah just kidding, nothing was changed.";
            }
        }

        public PageUpdatedEmbed(IInteractionContext context, Exception exception, long pageId, long notebookId)
            : this(context, pageId, notebookId)
        {
            Title = "Error";
            Description = exception.Message;
        }
    }
}
