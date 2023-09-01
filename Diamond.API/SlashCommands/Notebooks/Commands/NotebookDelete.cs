using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.SlashCommands.Notebooks.Commands.Autocompleters;
using Diamond.API.SlashCommands.Notebooks.Interaction;
using Diamond.Data;
using Diamond.Data.Models.Notebooks;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Notebooks
{
	public partial class Notebooks
	{
		[DSlashCommand("delete", "Delete a notebook.")]
		public async Task DeleteNotebookCommandAsync(
			[Summary("notebook-name", "The name of the notebook to delete."), Autocomplete(typeof(NotebookNameAutocompleter))] long notebookId
		)
		{
			await this.DeferAsync(true);

			using DiamondContext db = new DiamondContext();

			Notebook notebook = Notebook.GetNotebook(notebookId, db);
			if (notebook.DiscordUserId != Context.User.Id)
			{
				return;
			}

			_ = await new NotebookDeleteConfirmEmbed(this.Context, notebook).SendAsync();
		}
	}
}
