using System.Threading.Tasks;

using Diamond.API.Helpers;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Notebooks.Interaction
{
	public class ButtonPageListPreviousAttribute : DefaultComponentInteractionAttribute
	{
		public const string COMPONENT_ID = "button_notebookpage_list_previous:*,*";

		public ButtonPageListPreviousAttribute()
			: base(COMPONENT_ID)
		{ }
	}

	public class ButtonPageListPreviousHandler : InteractionModuleBase<SocketInteractionContext>
	{
		[ButtonPageListPrevious]
		public async Task OnButtonPageListPreviousClickHandlerAsync(long notebookId, int startingIndex)
		{
			await this.DeferAsync();

			await Notebooks.SendPagesListEmbedAsync(this.Context, notebookId, startingIndex - Notebooks.ITEMS_PER_PAGE, false);
		}
	}
}
