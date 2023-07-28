using Diamond.API.Helpers;

namespace Diamond.API.SlashCommands.Notebooks.Components
{
	public class ButtonNotebookRenameAtribute : DefaultComponentInteractionAttribute
	{
		public const string BUTTON_ID = "button_notebook_rename:*";

		public ButtonNotebookRenameAtribute()
			: base(BUTTON_ID)
		{ }

		public ButtonNotebookRenameAtribute(long notebookId)
			: base(BUTTON_ID, notebookId)
		{ }
	}
}
