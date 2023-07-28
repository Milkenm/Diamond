using Diamond.API.Helpers;

namespace Diamond.API.SlashCommands.Notebooks.Components
{
	public class ButtonNotebookPagesGotoListAttribute : DefaultComponentInteractionAttribute
	{
		public const string BUTTON_ID = "button_notebookpages_goto_list:*";

		public ButtonNotebookPagesGotoListAttribute()
			: base(BUTTON_ID)
		{ }

		public ButtonNotebookPagesGotoListAttribute(long? notebookPageId)
			: base(BUTTON_ID, notebookPageId)
		{ }
	}
}
