using Diamond.API.Helpers;

namespace Diamond.API.SlashCommands.Notebooks.Components
{
	public class ButtonNotebookConfirmDeleteAttribute : DefaultComponentInteractionAttribute
	{
		public const string BUTTON_ID = "button_notebook_delete_confirm:*";

		public ButtonNotebookConfirmDeleteAttribute()
			: base(BUTTON_ID)
		{ }

		public ButtonNotebookConfirmDeleteAttribute(long notebookId)
			: base(BUTTON_ID, notebookId)
		{ }
	}
}
