using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.Helpers;
using Diamond.Data;
using Diamond.Data.Models.Notebooks;

using Discord;
using Discord.Interactions;

namespace Diamond.API.SlashCommands.SCNotebook
{
	public partial class SCNotebook
	{
		[ComponentInteraction($"{NotebookComponentIds.BUTTON_NOTEBOOK_OPEN}:*", true)]
		public async Task OnOpenNotebookButtonClickHandlerAsync(long notebookId)
		{
			await this.DeferAsync();

			using DiamondContext db = new DiamondContext();

			Dictionary<string, NotebookPage> notebookPages = NotebookPage.GetNotebookPages(notebookId, this.Context.User.Id, db);

			PagesListMultipageEmbed embed = new PagesListMultipageEmbed(this.Context, notebookPages.Values.ToList(), 0, false);
			_ = embed.SendAsync();
		}

		private class PagesListMultipageEmbed : MultipageEmbed<NotebookPage>
		{
			public PagesListMultipageEmbed(IInteractionContext context, List<NotebookPage> notebookPages, int startingIndex, bool showEveryone)
				: base("Notebooks - Pages List", "📔", context, notebookPages, startingIndex, 5, 1, showEveryone)
			{ }

			protected override void FillItems(IEnumerable<NotebookPage> notebookPages)
			{
				if (!this.ItemsList.Any())
				{
					this.Description = "This notebook doesn't have any pages.";
					return;
				}

				foreach (NotebookPage page in notebookPages)
				{
					this.AddButton(new ButtonBuilder($"#{page.Id}", $"{NotebookComponentIds.BUTTON_NOTEBOOK_OPEN}:{page.Id}", ButtonStyle.Primary), 0);

					_ = this.AddField($"**#{page.Id}** :heavy_minus_sign: {page.Title}", " ");
				}
			}
		}
	}
}
