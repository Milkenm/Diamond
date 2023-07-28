using Diamond.API.Helpers;

namespace Diamond.API.SlashCommands.Notebooks.Components
{
	public class ButtonNotebookRenameConfirmAtribute : DefaultComponentInteractionAttribute
	{
		public const string BUTTON_ID = "button_notebook_rename_confirm:*";

		public ButtonNotebookRenameConfirmAtribute()
			: base(BUTTON_ID)
		{ }

		public ButtonNotebookRenameConfirmAtribute(long notebookId)
			: base(BUTTON_ID, notebookId)
		{ }
	}
}
