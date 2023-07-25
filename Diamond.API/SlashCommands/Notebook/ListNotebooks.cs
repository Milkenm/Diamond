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

			await this.SendNotebooksListEmbedAsync(startingIndex - 1, showEveryone);
		}

		[ComponentInteraction($"{MultipageEmbedIds.BUTTON_MULTIPAGE_NEXT}:*,*", true)]
		public async Task OnNextPageClickHandlerAsync(int startingIndex, bool showEveryone)
		{
			await this.DeferAsync();

			await this.SendNotebooksListEmbedAsync(startingIndex + 1, showEveryone);
		}

		[ComponentInteraction($"{MultipageEmbedIds.BUTTON_MULTIPAGE_LAST}:*", true)]
		public async Task OnLastPageClickHandlerAsync(bool showEveryone)
		{
			await this.DeferAsync();

			await this.SendNotebooksListEmbedAsync(-1, showEveryone);
		}

		private async Task SendNotebooksListEmbedAsync(int startingIndex, bool showEveryone)
		{
			using DiamondContext db = new DiamondContext();

			Dictionary<string, Notebook> userNotebooks = Notebook.GetNotebooksMap(this.Context.User.Id, db);

			NotebooksListMultipageEmbed embed = new NotebooksListMultipageEmbed(this.Context, userNotebooks.Values.ToList(), startingIndex, showEveryone);

			await embed.SendAsync();
		}

		private class NotebooksListMultipageEmbed : MultipageEmbed<Notebook>
		{
			public NotebooksListMultipageEmbed(IInteractionContext context, List<Notebook> userNotebooks, int startingIndex, bool showEveryone)
				: base("Notebooks List", "📔", context, userNotebooks, startingIndex, 5, userNotebooks.Any() ? 1 : 0, showEveryone)
			{ }

			protected override void FillItems(IEnumerable<Notebook> notebooksList)
			{
				if (!this.ItemsList.Any())
				{
					this.Description = "You don't have any notebooks.";
					return;
				}

				foreach (Notebook notebook in notebooksList)
				{
					this.AddButton(new ButtonBuilder($"#{notebook.Id}", $"{NotebookComponentIds.BUTTON_NOTEBOOK_OPEN}:{notebook.Id}", ButtonStyle.Primary), 0);

					_ = this.AddField($"**#{notebook.Id}** :heavy_minus_sign: {notebook.Name}", notebook.Description ?? "*No description*");
				}
			}
		}
	}
}
