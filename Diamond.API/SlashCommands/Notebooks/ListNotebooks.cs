using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.SlashCommands.Notebooks.Embeds;
using Diamond.Data;
using Diamond.Data.Models.Notebooks;

using Discord.Interactions;

using static Diamond.API.SlashCommands.NotebookComponentIds;

namespace Diamond.API.SlashCommands.Notebooks
{
	public partial class Notebooks
	{
		/*private const int ITEMS_PER_PAGE = 5;

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

		[ButtonNotebookPagesGotoList]
		public async Task OnBackButtonClickHandlerAsync(int startingIndex)
		{
			await this.DeferAsync();

			await this.SendNotebooksListEmbedAsync(startingIndex, false);
		}

		private async Task SendNotebooksListEmbedAsync(int startingIndex, bool showEveryone)
		{
			using DiamondContext db = new DiamondContext();

			Dictionary<string, Notebook> userNotebooks = Notebook.GetNotebooksMap(this.Context.User.Id, db);

			if (!await NotebookValidationUtils.HasNotebooks(this.Context, userNotebooks))
			{
				return;
			}

			await new ListNotebooksEmbed(this.Context, userNotebooks.Values.ToList(), startingIndex, showEveryone).SendAsync();
		}*/
	}
}
