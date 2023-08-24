using System.Collections.Generic;
using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.Helpers;
using Diamond.API.SlashCommands.Notebooks.Interaction.Embeds;
using Diamond.Data;
using Diamond.Data.Models.Notebooks;

using Discord;
using Discord.Interactions;

namespace Diamond.API.SlashCommands.Notebooks
{
	public partial class Notebooks
	{
		private const int ITEMS_PER_PAGE = 5;

		[DSlashCommand("list", "List every notebook you have.")]
		public async Task ListNotebooksCommandAsync(
			[ShowEveryone] bool showEveryone = false
		)
		{
			await this.DeferAsync(!showEveryone);

			await SendNotebooksListEmbedAsync(this.Context, 0, showEveryone);
		}

		[ComponentInteraction($"{MultipageEmbedIds.BUTTON_MULTIPAGE_FIRST}:*", true)]
		public async Task OnFirstPageClickHandlerAsync(bool showEveryone)
		{
			await this.DeferAsync();

			await SendNotebooksListEmbedAsync(this.Context, 0, showEveryone);
		}

		[ComponentInteraction($"{MultipageEmbedIds.BUTTON_MULTIPAGE_PREVIOUS}:*,*", true)]
		public async Task OnPreviousPageClickHandlerAsync(int startingIndex, bool showEveryone)
		{
			await this.DeferAsync();

			await SendNotebooksListEmbedAsync(this.Context, startingIndex - ITEMS_PER_PAGE, showEveryone);
		}

		[ComponentInteraction($"{MultipageEmbedIds.BUTTON_MULTIPAGE_NEXT}:*,*", true)]
		public async Task OnNextPageClickHandlerAsync(int startingIndex, bool showEveryone)
		{
			await this.DeferAsync();

			await SendNotebooksListEmbedAsync(this.Context, startingIndex + ITEMS_PER_PAGE, showEveryone);
		}

		[ComponentInteraction($"{MultipageEmbedIds.BUTTON_MULTIPAGE_LAST}:*", true)]
		public async Task OnLastPageClickHandlerAsync(bool showEveryone)
		{
			await this.DeferAsync();

			await SendNotebooksListEmbedAsync(this.Context, -1, showEveryone);
		}

		public static async Task SendNotebooksListEmbedAsync(IInteractionContext context, int startingIndex, bool showEveryone)
		{
			using DiamondContext db = new DiamondContext();

			List<Notebook> userNotebooks = Notebook.GetUserNotebooks(context.User.Id, db);

			if (!await NotebookValidationUtils.HasNotebooks(context, userNotebooks))
			{
				return;
			}

			_ = await new NotebookListEmbed(context, userNotebooks, startingIndex, showEveryone).SendAsync();
		}
	}
}
