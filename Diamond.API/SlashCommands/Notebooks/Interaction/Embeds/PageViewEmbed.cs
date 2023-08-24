using System;

using Diamond.API.SlashCommands.Notebooks.Interaction.Buttons;
using Diamond.Data;
using Diamond.Data.Models.Notebooks;

using Discord;

namespace Diamond.API.SlashCommands.Notebooks.Interaction.Embeds
{
	public class PageViewEmbed : BaseNotebookEmbed
	{
		private PageViewEmbed(IInteractionContext context, long? pageId, long notebookId)
			: base("Notebook Page", context)
		{
			if (pageId != null)
			{
				_ = this.AddButton(new ButtonPageEditorAttribute((long)pageId, notebookId).GetButtonBuilder("Edit Page"));
			}
			_ = this.AddButton(new ButtonNotebookOpenAttribute(notebookId).GetButtonBuilder("Go Back", ButtonStyle.Secondary));
		}

		public PageViewEmbed(IInteractionContext context, long pageId, long notebookId)
			: this(context, (long?)pageId, notebookId)
		{
			using DiamondContext db = new DiamondContext();

			NotebookPage page = NotebookPage.GetNotebookPage(pageId, db);

			this.Title = page.Title;
			this.Description = $"```{page.Content}```";
		}

		public PageViewEmbed(IInteractionContext context, Exception exception, long notebookId)
			: this(context, (long?)null, notebookId)
		{
			this.Title = "Error";
			this.Description = exception.Message;
		}
	}
}
