using System.Threading.Tasks;

using Diamond.API.Helpers;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Notebooks.Interaction
{
	public class ButtonNotebookListFirstAttribute : DefaultComponentInteractionAttribute
	{
		public const string COMPONENT_ID = "button_notebook_list_first";

		public ButtonNotebookListFirstAttribute()
			: base(COMPONENT_ID)
		{ }
	}

	public class ButtonNotebookListFirstHandler : InteractionModuleBase<SocketInteractionContext>
	{
		[ButtonNotebookListFirst]
		public async Task OnButtonNotebookListFirstClickHandlerAsync()
		{
			await this.DeferAsync();

			await Notebooks.SendNotebooksListEmbedAsync(this.Context, 0, false);
		}
	}
}
