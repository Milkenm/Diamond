using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.SlashCommands.Notebooks.Commands.Autocompleters;
using Diamond.API.SlashCommands.Notebooks.Interaction.Embeds;
using Diamond.API.Util;
using Diamond.Data;
using Diamond.Data.Models.Notebooks;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Notebooks
{
	public partial class Notebooks
	{
		[DSlashCommand("list-pages", "List the pages from a notebook.")]
		public async Task ListPagesCommandHandlerAsync(
			[Summary("notebook", "The notebook to list the pages of."), Autocomplete(typeof(NotebookNameAutocompleter))] string notebookName,
			[Summary("page", "The list page to show.")][MinValue(1)] int page = 1,
			[ShowEveryone] bool showEveryone = false
		)
		{
			await this.DeferAsync(!showEveryone);

			using DiamondContext db = new DiamondContext();

			// Convert to index
			page -= 1;

			List<Notebook> userNotebooks = Notebook.GetUserNotebooks(this.Context.User.Id, db);

			if (!await NotebookValidationUtils.HasNotebooks(this.Context, userNotebooks))
			{
				return;
			}

			Notebook notebook = Utils.Search(GetNotebooksMap(userNotebooks), notebookName).First().Item;

			_ = await new PageListEmbed(this.Context, notebook, showEveryone, db).SendAsync();
		}
	}
}
