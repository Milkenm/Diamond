using System.Threading.Tasks;

using Diamond.API.Helpers;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Notebooks.Interaction
{
	public class ButtonNotebookListLastAttribute : DefaultComponentInteractionAttribute
	{
		public const string COMPONENT_ID = "button_notebook_list_last";

		public ButtonNotebookListLastAttribute()
			: base(COMPONENT_ID)
		{ }
	}

	public class ButtonNotebookListLastHandler : InteractionModuleBase<SocketInteractionContext>
	{
		[ButtonNotebookListLast]
		public async Task OnButtonNotebookListLastClickHandlerAsync()
		{
			await this.DeferAsync();

			await Notebooks.SendNotebooksListEmbedAsync(this.Context, -1, false);
		}
	}
}
