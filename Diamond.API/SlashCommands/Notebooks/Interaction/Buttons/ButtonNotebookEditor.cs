using System.Threading.Tasks;

using Diamond.API.Helpers;
using Diamond.API.SlashCommands.Notebooks.Interaction.Modals;
using Diamond.Data;
using Diamond.Data.Models.Notebooks;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Notebooks.Interaction.Buttons
{
	public class ButtonNotebookEditorAttribute : DefaultComponentInteractionAttribute
	{
		public override string ComponentLabel => "Create Notebook";

		public const string COMPONENT_ID = "button_notebook_create:*";

		public ButtonNotebookEditorAttribute()
			: base(COMPONENT_ID, 0)
		{ }

		public ButtonNotebookEditorAttribute(long notebookId)
			: base(COMPONENT_ID, notebookId)
		{ }
	}

	public class ButtonNotebookEditorHandler : InteractionModuleBase<SocketInteractionContext>
	{
		[ButtonNotebookEditor]
		public async Task OnButtonNotebookEditorClickHandlerAsync(long notebookId)
		{
			NotebookEditorModal modal = new NotebookEditorModal();
			if (notebookId != 0)
			{
				using DiamondContext db = new DiamondContext();
				Notebook notebook = Notebook.GetNotebook(notebookId, db);

				modal.NotebookName = notebook.Name;
				modal.NotebookDescription = notebook.Description;
			}

			await this.Context.Interaction.RespondWithModalAsync(new ModalNotebookEditorAttribute(notebookId).ComponentIdWithData, modal);
		}
	}
}
