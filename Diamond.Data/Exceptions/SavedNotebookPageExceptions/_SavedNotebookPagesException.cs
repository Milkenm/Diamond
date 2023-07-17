namespace Diamond.Data.Exceptions.SavedNotebookPageExceptions
{
	public class SavedNotebookPageException : Exception
	{
		public SavedNotebookPageException(string message)
			: base(message)
		{ }

		public SavedNotebookPageException(string message, Exception innerException)
			: base(message, innerException)
		{ }
	}
}
