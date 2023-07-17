namespace Diamond.Data.Exceptions.SavedNotebookPageExceptions
{
	public class SavedNotebookPageDeleteException : SavedNotebookPageException
	{
		public SavedNotebookPageDeleteException(Exception innerException)
			: base("There was a problem deleting the saved notebook page.", innerException)
		{ }
	}
}
