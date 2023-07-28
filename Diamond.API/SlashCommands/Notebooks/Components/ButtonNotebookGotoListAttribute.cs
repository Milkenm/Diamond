using Diamond.API.Helpers;

namespace Diamond.API.SlashCommands.Notebooks.Components
{
	public class ButtonNotebookGotoListAttribute : DefaultComponentInteractionAttribute
	{
		public const string BUTTON_ID = "button_notebook_goto_list:*";

		public ButtonNotebookGotoListAttribute()
			: base(BUTTON_ID)
		{ }

		public ButtonNotebookGotoListAttribute(long? notebookId)
			: base(BUTTON_ID, notebookId)
		{ }
	}
}
