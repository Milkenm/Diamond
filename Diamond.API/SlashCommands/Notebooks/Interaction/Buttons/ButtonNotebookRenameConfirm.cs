using System.Threading.Tasks;

using Diamond.API.Helpers;
using Diamond.Data;
using Diamond.Data.Models.Notebooks;

using Discord;
using Discord.Interactions;

namespace Diamond.API.SlashCommands.Notebooks.Interaction.Buttons
{
	public class ButtonNotebookRenameConfirmAttribute : DefaultComponentInteractionAttribute
	{
		public override string ComponentLabel => "Rename Notebook";
		public override ButtonStyle? ButtonComponentStyle => ButtonStyle.Success;

		public const string COMPONENT_ID = "button_notebook_rename_confirm:*";

		public ButtonNotebookRenameConfirmAttribute()
			: base(COMPONENT_ID)
		{ }

		public ButtonNotebookRenameConfirmAttribute(long notebookId)
			: base(COMPONENT_ID, notebookId)
		{ }
	}

	public class ButtonNotebookRenameConfirmHandler : InteractionModuleBase<SocketInteractionContext>
	{
		[ButtonNotebookRenameConfirm]
		public async Task OnButtonNotebookRenameConfirmClickHandlerAsync(long notebookId, string newName)
		{
			await this.DeferAsync(true);

			using DiamondContext db = new DiamondContext();

			Notebook notebook = Notebook.GetNotebook(notebookId, db);
			notebook.Name = newName;

			await db.SaveAsync();
		}
	}
}
