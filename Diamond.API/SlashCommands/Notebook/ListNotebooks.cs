using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.Helpers;
using Diamond.Data;
using Diamond.Data.Models.Notebooks;

using Discord;
using Discord.Interactions;

namespace Diamond.API.SlashCommands.SCNotebook
{
	public partial class SCNotebook
	{
		private const int ITEMS_PER_PAGE = 5;

		[DSlashCommand("list", "List every notebook you have.")]
		public async Task ListNotebooksCommandAsync(
			[ShowEveryone] bool showEveryone = false
		)
		{
			await this.DeferAsync(!showEveryone);

			await this.SendNotebooksListEmbedAsync(0, showEveryone);
		}

		[ComponentInteraction($"{MultipageEmbedIds.BUTTON_MULTIPAGE_FIRST}:*", true)]
		public async Task OnFirstPageClickHandlerAsync(bool showEveryone)
		{
			await this.DeferAsync();

			await this.SendNotebooksListEmbedAsync(0, showEveryone);
		}

		[ComponentInteraction($"{MultipageEmbedIds.BUTTON_MULTIPAGE_PREVIOUS}:*,*", true)]
		public async Task OnPreviousPageClickHandlerAsync(int startingIndex, bool showEveryone)
		{
			await this.DeferAsync();

			await this.SendNotebooksListEmbedAsync(startingIndex - ITEMS_PER_PAGE, showEveryone);
		}

		[ComponentInteraction($"{MultipageEmbedIds.BUTTON_MULTIPAGE_NEXT}:*,*", true)]
		public async Task OnNextPageClickHandlerAsync(int startingIndex, bool showEveryone)
		{
			await this.DeferAsync();

			await this.SendNotebooksListEmbedAsync(startingIndex + ITEMS_PER_PAGE, showEveryone);
		}

		[ComponentInteraction($"{MultipageEmbedIds.BUTTON_MULTIPAGE_LAST}:*", true)]
		public async Task OnLastPageClickHandlerAsync(bool showEveryone)
		{
			await this.DeferAsync();

			await this.SendNotebooksListEmbedAsync(-1, showEveryone);
		}

		[ComponentInteraction($"{NotebookComponentIds.BUTTON_NOTEBOOKPAGES_GOBACK}:*", true)]
		public async Task OnBackButtonClickHandlerAsync(int startingIndex)
		{
			await this.DeferAsync();

			await this.SendNotebooksListEmbedAsync(startingIndex, false);
		}

		private async Task SendNotebooksListEmbedAsync(int startingIndex, bool showEveryone)
		{
			using DiamondContext db = new DiamondContext();

			Dictionary<string, Notebook> userNotebooks = Notebook.GetNotebooksMap(this.Context.User.Id, db);

			ListNotebooksMultipageEmbed embed = new ListNotebooksMultipageEmbed(this.Context, userNotebooks.Values.ToList(), startingIndex, showEveryone);

			await embed.SendAsync();
		}

		private class ListNotebooksMultipageEmbed : MultipageEmbed<Notebook>
		{
			public ListNotebooksMultipageEmbed(IInteractionContext context, List<Notebook> userNotebooks, int startingIndex, bool showEveryone)
				: base("Notebooks List", "📔", context, userNotebooks, startingIndex, 5, showEveryone)
			{ }

			protected override void FillItems(IEnumerable<Notebook> notebooksList)
			{
				if (!this.ItemsList.Any())
				{
					this.Description = "You don't have any notebooks.";
					this.AddNavigationButtons(0);
					return;
				}

				foreach (Notebook notebook in notebooksList)
				{
					_ = this.AddButton(new ButtonBuilder($"#{notebook.Id}", $"{NotebookComponentIds.BUTTON_NOTEBOOK_OPEN}:{notebook.Id},{this.StartingIndex}", ButtonStyle.Success), 0);
					_ = this.AddField($"**#{notebook.Id}** :heavy_minus_sign: {notebook.Name}", notebook.Description ?? "*No description*");
				}
				this.AddNavigationButtons(1);
			}
		}
	}
}
