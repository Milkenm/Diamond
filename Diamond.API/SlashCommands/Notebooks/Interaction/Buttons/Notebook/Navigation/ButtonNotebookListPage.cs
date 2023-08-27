using System.Threading.Tasks;

using Diamond.API.Helpers;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Notebooks.Interaction
{
	public class ButtonNotebookListPageAttribute : DefaultComponentInteractionAttribute
	{
		public const string COMPONENT_ID = "button_notebook_list_page";

		public ButtonNotebookListPageAttribute()
			: base(COMPONENT_ID)
		{ }
	}

	public class ButtonNotebookListPageHandler : InteractionModuleBase<SocketInteractionContext>
	{
		[ButtonNotebookListPage]
		public async Task OnButtonNotebookListPageClickHandlerAsync()
		{
			// This isn't supposed to do anything
			await this.DeferAsync(true);
		}
	}
}
