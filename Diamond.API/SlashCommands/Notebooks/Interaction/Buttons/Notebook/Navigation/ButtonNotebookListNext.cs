using System.Threading.Tasks;

using Diamond.API.Helpers;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Notebooks.Interaction
{
	public class ButtonNotebookListNextAttribute : DefaultComponentInteractionAttribute
	{
		public const string COMPONENT_ID = "button_notebook_list_next:*";

		public ButtonNotebookListNextAttribute()
			: base(COMPONENT_ID)
		{ }
	}

	public class ButtonNotebookListNextHandler : InteractionModuleBase<SocketInteractionContext>
	{
		[ButtonNotebookListNext]
		public async Task OnButtonNotebookListNextClickHandlerAsync(int startingIndex)
		{
			await this.DeferAsync();

			await Notebooks.SendNotebooksListEmbedAsync(this.Context, startingIndex + Notebooks.ITEMS_PER_PAGE, false);
		}
	}
}
