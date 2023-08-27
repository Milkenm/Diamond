using System.Threading.Tasks;

using Diamond.API.Helpers;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Notebooks.Interaction
{
	public class ButtonPageListFirstAttribute : DefaultComponentInteractionAttribute
	{
		public const string COMPONENT_ID = "button_notebookpage_list_first:*";

		public ButtonPageListFirstAttribute()
			: base(COMPONENT_ID)
		{ }
	}

	public class ButtonPageListFirstHandler : InteractionModuleBase<SocketInteractionContext>
	{
		[ButtonPageListFirst]
		public async Task OnButtonPageListFirstClickHandlerAsync(long notebookId)
		{
			await this.DeferAsync();

			await Notebooks.SendPagesListEmbedAsync(this.Context, notebookId, 0, false);
		}
	}
}
