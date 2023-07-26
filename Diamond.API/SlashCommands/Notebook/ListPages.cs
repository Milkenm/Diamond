using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.Helpers;
using Diamond.API.Util;
using Diamond.Data;
using Diamond.Data.Models.Notebooks;

using Discord;
using Discord.Interactions;

namespace Diamond.API.SlashCommands.SCNotebook
{
	public partial class SCNotebook
	{
		[ComponentInteraction($"{NotebookComponentIds.BUTTON_NOTEBOOK_OPEN}:*,*", true)]
		public async Task OnOpenNotebookButtonClickHandlerAsync(long notebookId, int notebooksListStartingIndex)
		{
			await this.DeferAsync();

			using DiamondContext db = new DiamondContext();

			Dictionary<string, NotebookPage> notebookPages = NotebookPage.GetNotebookPages(notebookId, this.Context.User.Id, db);
			Notebook notebook = Notebook.GetNotebook(notebookId, db);

			NotebookPagesListMultipageEmbed embed = new NotebookPagesListMultipageEmbed(this.Context, notebook, notebookPages.Values.ToList(), 0, notebooksListStartingIndex, false);
			_ = embed.SendAsync();
		}

		private class NotebookPagesListMultipageEmbed : MultipageEmbed<NotebookPage>
		{
			private readonly int _notebooksListStartingIndex;
			private readonly Notebook _notebook;

			public NotebookPagesListMultipageEmbed(IInteractionContext context, Notebook notebook, List<NotebookPage> notebookPages, int startingIndex, int notebooksListStartingIndex, bool showEveryone)
				: base($"Notebook: {notebook.Name} ({notebookPages.Count} {Utils.Plural("page", "", "s", notebookPages.Count)})", "📔", context, notebookPages, startingIndex, 5, showEveryone)
			{
				this._notebooksListStartingIndex = notebooksListStartingIndex;
				this._notebook = notebook;
			}

			protected override void FillItems(IEnumerable<NotebookPage> notebookPages)
			{
				if (!this.ItemsList.Any())
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

				_ = this.AddButton(new ButtonBuilder("Go back", $"{NotebookComponentIds.BUTTON_NOTEBOOKPAGES_GOBACK}:{this._notebooksListStartingIndex}", ButtonStyle.Primary), 0);
				this.AddButton(new ButtonBuilder("Delete Notebook", $"{NotebookComponentIds.BUTTON_NOTEBOOK_DELETE}:{this._notebook.Id}", ButtonStyle.Danger), 0);
				this.AddNavigationButtons(1);
			}
		}
	}
}
