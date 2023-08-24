using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.Data;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Notebooks
{
	public partial class Notebooks
	{
		[DSlashCommand("create", "Creates a new notebook.")]
		public async Task CreateNotebookCommandAsync(
			[Summary("name", "Sets the name of the notebook.")] string notebookName,
			[Summary("description", "Set a description for the notebook.")] string? description = null
		)
		{
			await this.DeferAsync(true);

			using DiamondContext db = new DiamondContext();

			await CreateOrUpdateNotebookAsync(this.Context, null, notebookName, description, db);
		}
	}
}
