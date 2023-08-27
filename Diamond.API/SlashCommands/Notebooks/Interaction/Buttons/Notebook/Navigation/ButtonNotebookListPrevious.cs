using System.Threading.Tasks;

using Diamond.API.Helpers;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Notebooks.Interaction
{
	public class ButtonNotebookListPreviousAttribute : DefaultComponentInteractionAttribute
	{
		public const string COMPONENT_ID = "button_notebook_list_previous:*";

		public ButtonNotebookListPreviousAttribute()
			: base(COMPONENT_ID)
		{ }
	}

	public class ButtonNotebookListPreviousHandler : InteractionModuleBase<SocketInteractionContext>
	{
		[ButtonNotebookListPrevious]
		public async Task OnButtonNotebookListPreviousClickHandlerAsync(int startingIndex)
		{
			await this.DeferAsync();

			await Notebooks.SendNotebooksListEmbedAsync(this.Context, startingIndex - Notebooks.ITEMS_PER_PAGE, false);
		}
	}
}
