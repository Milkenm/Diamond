namespace Diamond.Data.Exceptions.SavedNotebookPageExceptions
{
	public class SavedNotebookPageNotFoundException : SavedNotebookPageException
	{
		public SavedNotebookPageNotFoundException(long savedPageId)
			: base("A saved notebook page with ID '{savedPageId}' could not be found.")
		{ }
	}
}
