using System.Threading.Tasks;

using Diamond.API.Helpers;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Notebooks.Interaction.Buttons
{
	public class ButtonNotebookRenameAttribute : DefaultComponentInteractionAttribute
	{
		public override string ComponentLabel => "Rename Notebook";

		public const string COMPONENT_ID = "button_notebook_rename:*";

		public ButtonNotebookRenameAttribute()
			: base(COMPONENT_ID)
		{ }

		public ButtonNotebookRenameAttribute(long notebookId)
			: base(COMPONENT_ID, notebookId)
		{ }
	}

	public class ButtonNotebookRenameHandler : InteractionModuleBase<SocketInteractionContext>
	{
		[ButtonNotebookRename]
		public async Task OnButtonNotebookRenameClickHandlerAsync(long notebookId)
		{
			await this.DeferAsync(true);
		}
	}
}
