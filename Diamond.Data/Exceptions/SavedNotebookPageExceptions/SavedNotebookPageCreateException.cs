namespace Diamond.Data.Exceptions.SavedNotebookPageExceptions
{
	public class SavedNotebookPageCreateException : SavedNotebookPageException
	{
		public SavedNotebookPageCreateException(Exception innerException)
			: base("There was a problem creating the saved notebook page.", innerException)
		{ }
	}
}
