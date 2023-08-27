using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.SlashCommands.Notebooks.Commands.Autocompleters;
using Diamond.API.SlashCommands.Notebooks.Interaction;
using Diamond.API.Util;
using Diamond.Data;
using Diamond.Data.Models.Notebooks;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Notebooks
{
	public partial class Notebooks
	{
		[DSlashCommand("delete", "Delete a notebook.")]
		public async Task DeleteNotebookCommandAsync(
			[Summary("notebook-name", "The name of the notebook to delete."), Autocomplete(typeof(NotebookNameAutocompleter))] string notebookName
		)
		{
			await this.DeferAsync(true);

			using DiamondContext db = new DiamondContext();

			List<Notebook> userNotebooks = Notebook.GetUserNotebooks(this.Context.User.Id, db);

			if (!await NotebookValidationUtils.HasNotebooks(this.Context, userNotebooks))
			{
				return;
			}

			Notebook notebook = Utils.Search(GetNotebooksMap(userNotebooks), notebookName).First().Item;

			_ = await new NotebookDeleteConfirmEmbed(this.Context, notebook).SendAsync();
		}
	}
}
