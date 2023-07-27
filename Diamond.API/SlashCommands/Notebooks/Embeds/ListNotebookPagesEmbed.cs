using System.Collections.Generic;
using System.Linq;

using Diamond.API.Util;
using Diamond.Data;
using Diamond.Data.Models.Notebooks;

using Discord;

namespace Diamond.API.SlashCommands.Notebooks.Embeds
{
	/*public class ListNotebookPagesEmbed : MultipageNotebookEmbed<NotebookPage>
	{
		private readonly Notebook _notebook;

		public ListNotebookPagesEmbed(IInteractionContext context, Notebook notebook, int startingIndex, bool showEveryone, DiamondContext db)
			: base(context, showEveryone)
		{
			this._notebook = notebook;
			this.LoadNotebookPages(context, startingIndex, db);
		}

		public ListNotebookPagesEmbed(IInteractionContext context, long notebookId, int startingIndex, bool showEveryone, DiamondContext db)
			: this(context, Notebook.GetNotebook(notebookId, db), startingIndex, showEveryone, db)
		{ }

		public ListNotebookPagesEmbed(IInteractionContext context, long notebookId, int startingIndex, bool showEveryone)
			: base(context, showEveryone)
		{
			using DiamondContext db = new DiamondContext();

			this._notebook = Notebook.GetNotebook(notebookId, db);
			this.LoadNotebookPages(context, startingIndex, db);
		}

		private void LoadNotebookPages(IInteractionContext context, int startingIndex, DiamondContext db)
		{
			Dictionary<string, NotebookPage> notebookPages = NotebookPage.GetNotebookPages(this._notebook, context.User.Id, db);

			this.SetItemsList(notebookPages.Values.ToList(), startingIndex, 5);
		}

		protected override void FillItems(IEnumerable<NotebookPage> notebookPages)
		{
			this.SetAuthorInfoWithEmoji($"Notebook: {this._notebook.Name} ({notebookPages.Count()} {Utils.Plural("page", "", "s", notebookPages.Count())})", "📔");

			if (!notebookPages.Any())
			{
				this.Description = "This notebook doesn't have any pages.";
			}
			else
			{
				foreach (NotebookPage page in notebookPages)
				{
					_ = this.AddButton(new ButtonBuilder($"#{page.Id}", $"{NotebookComponentIds.BUTTON_NOTEBOOK_OPEN}:{page.Id}", ButtonStyle.Success), 0);
					_ = this.AddField($"**#{page.Id}** :heavy_minus_sign: {page.Title}", " ");
				}
			}

			_ = this.AddButton(new ButtonBuilder("Go back", $"{NotebookComponentIds.BUTTON_NOTEBOOKPAGES_GOBACK}:{this.StartingIndex}", ButtonStyle.Primary), 0);
			_ = this.AddButton(new ButtonBuilder("Delete Notebook", $"{NotebookComponentIds.BUTTON_NOTEBOOK_DELETE}:{this._notebook.Id},{this.StartingIndex}", ButtonStyle.Danger), 0);
			this.AddNavigationButtons(1);
		}
	}*/
}
