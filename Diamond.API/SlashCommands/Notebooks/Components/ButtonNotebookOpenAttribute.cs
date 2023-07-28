using Diamond.API.Helpers;

namespace Diamond.API.SlashCommands.Notebooks.Components
{
	public class ButtonNotebookOpenAttribute : DefaultComponentInteractionAttribute
	{
		public const string BUTTON_ID = "button_notebook_open:*,*";

		public ButtonNotebookOpenAttribute()
			: base(BUTTON_ID)
		{ }

		public ButtonNotebookOpenAttribute(long notebookId, int startingIndex)
			: base(BUTTON_ID, notebookId, startingIndex)
		{ }
	}
}
