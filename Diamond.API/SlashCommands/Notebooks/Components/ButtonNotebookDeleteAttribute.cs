using Diamond.API.Helpers;

namespace Diamond.API.SlashCommands.Notebooks.Components
{
	public class ButtonNotebookDeleteAttribute : DefaultComponentInteractionAttribute
	{
		public const string BUTTON_ID = "button_notebook_delete:*";

		public ButtonNotebookDeleteAttribute()
			: base(BUTTON_ID)
		{ }

		public ButtonNotebookDeleteAttribute(long notebookId)
			: base(BUTTON_ID, notebookId)
		{ }
	}
}
