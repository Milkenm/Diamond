using System.Threading.Tasks;

using Diamond.API.Helpers;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Notebooks.Interaction
{
	public class ButtonPageListLastAttribute : DefaultComponentInteractionAttribute
	{
		public const string COMPONENT_ID = "button_notebookpage_list_last:*";

		public ButtonPageListLastAttribute()
			: base(COMPONENT_ID)
		{ }
	}

	public class ButtonPageListLastHandler : InteractionModuleBase<SocketInteractionContext>
	{
		[ButtonPageListLast]
		public async Task OnButtonPageListLastClickHandlerAsync(long notebookId)
		{
			await this.DeferAsync();

			await Notebooks.SendPagesListEmbedAsync(this.Context, notebookId, -1, false);
		}
	}
}
