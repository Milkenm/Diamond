using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.SlashCommands.Notebooks.Autocompleters;
using Diamond.API.SlashCommands.Notebooks.Embeds;
using Diamond.API.SlashCommands.Notebooks.Modals;
using Diamond.Data;
using Diamond.Data.Exceptions.NotebookExceptions;
using Diamond.Data.Models.Notebooks;

using Discord.Interactions;

using ScriptsLibV2.Extensions;

namespace Diamond.API.SlashCommands.Notebooks
{
	public partial class Notebooks
	{
		/*[DSlashCommand("create-page", "Creates a new page on your notebook.")]
		public async Task CreatePageCommandAsync(
			[Summary("title", "Sets the page title.")] string? title = null,
			[Summary("content", "Sets the content of the page.")] string? content = null,
			[Summary("notebook", "Adds the page to the selected notebook."), Autocomplete(typeof(NotebookNameAutocompleter))] string? notebookName = null
		)
		{
			await this.DeferAsync(true);

			using DiamondContext db = new DiamondContext();

			if (title.IsEmpty())
			{
				// Show the editor modal if the title of the page is empty
				await new NotebookPageEditorModal(this.Context)
				{
					PageContent = content,
					Notebook = notebookName,
				}.SendAsync();
				return;
			}

			await this.SavePageAsync(title, content, notebookName, db);
		}

		[ModalInteraction(NotebookComponentIds.MODAL_NOTEBOOK_EDIT_PAGE, true)]
		public async Task PollAddOptionModalHandler(NotebookPageEditorModal modal)
		{
			await this.DeferAsync();

			using DiamondContext db = new DiamondContext();

			await this.SavePageAsync(modal.PageTitle, modal.PageContent, modal.Notebook, db);
		}

		private async Task SavePageAsync(string title, string content, string? notebookName, DiamondContext db)
		{
			try
			{
				await NotebookPage.CreateNotebookPageAsync(title, content, this.Context.User.Id, notebookName, db);

				_ = await new CreatedNotebookPageEmbed(this.Context, title, notebookName).SendAsync();
			}
			catch (NotebookException ex)
			{
				_ = await new CreatedNotebookPageEmbed(this.Context, ex).SendAsync();
			}
		}*/
	}
}
