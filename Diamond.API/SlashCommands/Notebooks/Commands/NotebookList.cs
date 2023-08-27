using System.Threading.Tasks;

using Diamond.API.Attributes;

namespace Diamond.API.SlashCommands.Notebooks
{
	public partial class Notebooks
	{
		[DSlashCommand("list", "List every notebook you have.")]
		public async Task ListNotebooksCommandAsync(
			[ShowEveryone] bool showEveryone = false
		)
		{
			await this.DeferAsync(!showEveryone);

			await SendNotebooksListEmbedAsync(this.Context, 0, showEveryone);
		}
	}
}
