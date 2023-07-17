namespace Diamond.Data.Exceptions.NotebookExceptions
{
	public class NotebookUpdateException : NotebookException
	{
		public NotebookUpdateException(Exception innerException)
			: base("There was a problem updating the notebook.", innerException)
		{ }
	}
}
