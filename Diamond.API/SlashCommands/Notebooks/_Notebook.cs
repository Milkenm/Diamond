using System.Threading.Tasks;

using Diamond.API.SlashCommands.Notebooks.Embeds;
using Diamond.Data;

using Discord.Interactions;

using static Diamond.API.SlashCommands.NotebookComponentIds;

namespace Diamond.API.SlashCommands.Notebooks
{
	[Group("notebooks", "Notebook related commands.")]
	public partial class Notebooks : InteractionModuleBase<SocketInteractionContext>
	{
		/*[ButtonNotebookGotoList]
		public async Task ButtonGotoNotebooksListClickHandlerAsync(long? notebookId)
		{
			using DiamondContext db = new DiamondContext();

			await new ListNotebooksEmbed(this.Context, notebookId != null ? (long)notebookId : 0, false).SendAsync();
		}

		[ComponentInteraction($"{NotebookComponentIds.BUTTON_NOTEBOOKPAGES_GOTO_LIST}:*", true)]
		public async Task ButtonGotoNotebookPagesListClickHandlerAsync(long? notebookId)
		{
			using DiamondContext db = new DiamondContext();

			await new ListNotebookPagesEmbed(this.Context, notebookId != null ? (long)notebookId : 0, 0, false).SendAsync();
		}*/
	}
}
