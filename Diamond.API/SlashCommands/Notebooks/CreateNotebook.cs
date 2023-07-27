using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.SlashCommands.Notebooks.Embeds;
using Diamond.Data;
using Diamond.Data.Exceptions.NotebookExceptions;
using Diamond.Data.Models.Notebooks;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Notebooks
{
	public partial class Notebooks
	{
		/*[DSlashCommand("create", "Creates a new page on your notebook.")]
		public async Task CreateNotebookCommandAsync(
			[Summary("name", "Sets the name of the notebook.")] string notebookName,
			[Summary("description", "Set a description for the notebook.")] string? description = null
		)
		{
			await this.DeferAsync(true);

			using DiamondContext db = new DiamondContext();

			try
			{
				Notebook notebook = await Notebook.CreateNotebookAsync(notebookName, description, this.Context.User.Id, db);
				_ = await new CreatedNotebookEmbed(this.Context, notebook).SendAsync();
			}
			catch (NotebookException ex)
			{
				_ = await new CreatedNotebookEmbed(this.Context, ex).SendAsync();
				return;
			}
		}*/
	}
}
