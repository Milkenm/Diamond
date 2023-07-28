using System.Threading.Tasks;

using Diamond.API.SlashCommands.Notebooks.Components;
using Diamond.API.SlashCommands.Notebooks.Embeds;
using Diamond.API.SlashCommands.Notebooks.Exceptions;
using Diamond.Data;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Notebooks
{
	[Group("notebooks", "Notebook related commands.")]
	public partial class Notebooks : InteractionModuleBase<SocketInteractionContext>
	{
		[ButtonNotebookGotoList]
		public async Task ButtonGotoNotebooksListClickHandlerAsync(long? notebookId)
		{
			await this.DeferAsync();

			using DiamondContext db = new DiamondContext();

			try
			{
				_ = await new ListNotebooksEmbed(this.Context, notebookId, false).SendAsync();
			}
			catch (NoNotebooksException)
			{
				_ = await new NoNotebooksEmbed(this.Context).SendAsync();
			}
		}

		/*[ComponentInteraction($"{NotebookComponentIds.BUTTON_NOTEBOOKPAGES_GOTO_LIST}:*", true)]
		public async Task ButtonGotoNotebookPagesListClickHandlerAsync(long? notebookId)
		{
			using DiamondContext db = new DiamondContext();

			await new ListNotebookPagesEmbed(this.Context, notebookId != null ? (long)notebookId : 0, 0, false).SendAsync();
		}*/
	}
}
