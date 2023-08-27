using Diamond.Data;
using Diamond.Data.Models.Notebooks;

using Discord;

namespace Diamond.API.SlashCommands.Notebooks.Interaction
{
    public class PageDeleteConfirmEmbed : BaseNotebookEmbed
    {
        public PageDeleteConfirmEmbed(IInteractionContext context, NotebookPage page)
            : base("Confirm Delete Page", context)
        {
            Title = "Are you sure?";
            Description = $"Are you sure you want to delete the page '**{page.Title}**'?\n\n:warning: **__Deleted pages cannot be recovered.__**";
            _ = AddButton(new ButtonPageDeleteConfirmAttribute(page.Id).GetButtonBuilder())
                .AddButton(new ButtonPageDeleteCancelAttribute(page.Id).GetButtonBuilder());
        }

        public PageDeleteConfirmEmbed(IInteractionContext context, long pageId)
            : this(context, SearchPageById(pageId))
        { }

        private static NotebookPage SearchPageById(long pageId)
        {
            using DiamondContext db = new DiamondContext();
            NotebookPage page = NotebookPage.GetNotebookPage(pageId, db);

            return page;
        }
    }
}
