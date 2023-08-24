using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.SlashCommands.Notebooks.Commands.Autocompleters;
using Diamond.Data;

using Discord.Interactions;

using ScriptsLibV2.Extensions;

namespace Diamond.API.SlashCommands.Notebooks
{
    public partial class Notebooks
	{
		[DSlashCommand("create-page", "Creates a new page on your notebook.")]
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
				// TODO
				/*await new PageEditorModal(this.Context)
				{
					PageContent = content,
					Notebook = notebookName,
				}.SendAsync();*/
				return;
			}

			await CreateOrUpdatePageAsync(this.Context, null, title, content, notebookName, db);
		}
	}
}
