namespace Diamond.Data.Exceptions.NotebookExceptions
{
	public class NotebookException : Exception
	{
		public NotebookException(string errorMessage)
			: base(errorMessage)
		{ }

		public NotebookException(string errorMessage, Exception innerException)
			: base(errorMessage, innerException)
		{ }
	}
}
