using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.SlashCommands.Notebooks.Embeds;
using Diamond.API.Util;
using Diamond.Data;
using Diamond.Data.Models.Notebooks;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Notebooks
{
	public partial class Notebooks
	{
		/*[DSlashCommand("rename", "Changes the name of a notebook.")]
		public async Task RenameNotebookCommandHandlerAsync(
			[Summary("notebook", "The notebook you wish to rename.")] string notebookName,
			[Summary("new-name", "The new name for the notebook.")] string newName
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

			_ = await new RenameNotebookConfirmEmbed(this.Context, notebook, newName, 0).SendAsync();
		}

		[ComponentInteraction($"{NotebookComponentIds.BUTTON_NOTEBOOK_RENAME_CONFIRM}:*,*,*", true)]
		public async Task ButtonRenameConfirmClickHandlerAsync(long notebookId, string newName, int notebooksListStartingIndex)
		{
			await this.DeferAsync();

			using DiamondContext db = new DiamondContext();

			Notebook notebook = Notebook.GetNotebook(notebookId, db);
			notebook.Name = newName;

			await db.SaveAsync();
		}

		[ComponentInteraction($"{NotebookComponentIds.BUTTON_NOTEBOOK_RENAME_CANCEL}:*,*", true)]
		public async Task ButtonRenameCancelClickHandlerAsync(long notebookId, int notebooksListStartingIndex)
		{
			await this.DeferAsync();

			await new ListNotebookPagesEmbed(this.Context, notebookId, notebooksListStartingIndex, false).SendAsync();
		}

		[ComponentInteraction($"{NotebookComponentIds.BUTTON_NOTEBOOK_RENAME_GOBACK}:*,*", true)]
		public async Task ButtonRenameGoBackClickHandlerAsync(long notebookId, int notebooksListStartingIndex)
		{
			await this.DeferAsync();

			await new ListNotebookPagesEmbed(this.Context, notebookId, notebooksListStartingIndex, false).SendAsync();
		}*/
	}
}
