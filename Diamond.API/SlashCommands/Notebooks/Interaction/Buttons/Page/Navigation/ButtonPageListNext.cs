using System.Threading.Tasks;

using Diamond.API.Helpers;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Notebooks.Interaction
{
	public class ButtonPageListNextAttribute : DefaultComponentInteractionAttribute
	{
		public const string COMPONENT_ID = "button_notebookpage_list_next:*,*";

		public ButtonPageListNextAttribute()
			: base(COMPONENT_ID)
		{ }
	}

	public class ButtonPageListNextHandler : InteractionModuleBase<SocketInteractionContext>
	{
		[ButtonPageListNext]
		public async Task OnButtonPageListNextClickHandlerAsync(long notebookId, int startingIndex)
		{
			await this.DeferAsync();

			await Notebooks.SendPagesListEmbedAsync(this.Context, notebookId, startingIndex + Notebooks.ITEMS_PER_PAGE, false);
		}
	}
}
