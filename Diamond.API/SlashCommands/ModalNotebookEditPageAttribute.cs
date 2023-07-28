using Diamond.API.Helpers;

namespace Diamond.API.SlashCommands
{

	public partial class NotebookComponentIds
	{
		public class ModalNotebookEditPageAttribute : DefaultComponentInteractionAttribute
		{
			public ModalNotebookEditPageAttribute()
				: base("moda_notebook_edit_page:*")
			{ }

			public ModalNotebookEditPageAttribute(long notebookId)
				: base("modal_notebook_edit_page:*", notebookId)
			{ }
		}
	}
}
