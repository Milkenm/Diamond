using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.SlashCommands.Notebooks.Interaction.Embeds;
using Diamond.API.Util;
using Diamond.Data;
using Diamond.Data.Models.Notebooks;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Notebooks
{
	public partial class Notebooks
	{
		[DSlashCommand("rename", "Changes the name of a notebook.")]
		public async Task RenameNotebookCommandHandlerAsync(
			[Summary("notebook", "The notebook you wish to rename.")] string notebookName,
			[Summary("new-name", "The new name for the notebook.")] string newName
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

			_ = await new NotebookRenameConfirmEmbed(this.Context, notebook, newName, 0).SendAsync();
		}
	}
}
