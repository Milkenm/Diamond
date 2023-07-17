namespace Diamond.Data.Exceptions.NotebookPageExceptions
{
	public class NotebookPageException : Exception
	{
		public NotebookPageException(string message)
			: base(message)
		{ }

		public NotebookPageException(string message, Exception innerException)
			: base(message, innerException)
		{ }
	}
}
