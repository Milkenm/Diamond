namespace Diamond.Data.Exceptions.NotebookExceptions
{
	public class NotebookDeleteException : NotebookException
	{
		public NotebookDeleteException(Exception innerException)
			: base("There was a problem deleting the notebook.", innerException)
		{ }
	}
}
