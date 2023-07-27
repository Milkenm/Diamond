using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.SlashCommands.Notebooks.Autocompleters;
using Diamond.API.SlashCommands.Notebooks.Embeds;
using Diamond.API.Util;
using Diamond.Data;
using Diamond.Data.Models.Notebooks;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Notebooks
{
	public partial class Notebooks
	{
		/*[DSlashCommand("delete", "Delete a notebook.")]
		public async Task DeleteNotebookCommandAsync(
			[Summary("notebook-name", "The name of the notebook to delete."), Autocomplete(typeof(NotebookNameAutocompleter))] string notebookName
		)
		{
			await this.DeferAsync(true);

			using DiamondContext db = new DiamondContext();

			Dictionary<string, Notebook> userNotebooks = Notebook.GetNotebooksMap(this.Context.User.Id, db);

			if (!await NotebookValidationUtils.HasNotebooks(this.Context, userNotebooks))
			{
				return;
			}

			Notebook notebook = Utils.Search(userNotebooks, notebookName).First().Item;

			_ = await new DeleteNotebookConfirmEmbed(this.Context, notebook, 0).SendAsync();
		}

		[ComponentInteraction($"{NotebookComponentIds.BUTTON_NOTEBOOK_DELETE}:*,*", true)]
		public async Task OnDeleteNotebookButtonClickHandlerAsync(long notebookId, int notebooksListStartingIndex)
		{
			await this.DeferAsync(true);

			_ = await new DeleteNotebookConfirmEmbed(this.Context, notebookId, notebooksListStartingIndex).SendAsync();
		}

		[ComponentInteraction($"{NotebookComponentIds.BUTTON_NOTEBOOK_DELETE_CONFIRM}:*,*", true)]
		public async Task OnDeleteNotebookConfirmButtonClickHandlerAsync(long notebookId, int notebooksListStartingIndex)
		{
			await this.DeferAsync();

			using DiamondContext db = new DiamondContext();
			Notebook notebook = Notebook.GetNotebook(notebookId, db);

			try
			{
				await Notebook.DeleteNotebookAsync(notebook, db);
			}
			catch (Exception ex)
			{
				_ = await new DeletedNotebookEmbed(this.Context, ex, notebooksListStartingIndex).SendAsync();
				return;
			}

			_ = await new DeletedNotebookEmbed(this.Context, notebook.Name, notebooksListStartingIndex).SendAsync();
		}

		[ComponentInteraction($"{NotebookComponentIds.BUTTON_NOTEBOOK_DELETE_CANCEL}:*,*", true)]
		public async Task OnDeleteNotebookCancelButtonClickHandlerAsync(long notebookId, int notebooksListStartingIndex)
		{
			await this.DeferAsync();

			await new ListNotebookPagesEmbed(this.Context, notebookId, notebooksListStartingIndex, false).SendAsync();
		}

		[ComponentInteraction($"{NotebookComponentIds.BUTTON_NOTEBOOK_DELETE_GOBACK}:*", true)]
		public async Task OnButtonDeleteGoBackClickHandlerAsync(int notebooksListStartingIndex)
		{
			await this.DeferAsync();

			await new ListNotebooksEmbed(this.Context, notebooksListStartingIndex, false).SendAsync();
		}*/
	}
}
