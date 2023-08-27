using System.Threading.Tasks;

using Diamond.API.Helpers;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Notebooks.Interaction
{
	public class ButtonPageListPageAttribute : DefaultComponentInteractionAttribute
	{
		public const string COMPONENT_ID = "button_notebookpage_list_page";

		public ButtonPageListPageAttribute()
			: base(COMPONENT_ID)
		{ }
	}

	public class ButtonPageListPageHandler : InteractionModuleBase<SocketInteractionContext>
	{
		[ButtonPageListPage]
		public async Task OnButtonPageListPageClickHandlerAsync()
		{
			// This isn't supposed to do anything
			await this.DeferAsync(true);
		}
	}
}
