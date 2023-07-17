namespace Diamond.Data.Exceptions.NotebookExceptions
{
	public class NotebookCreateException : NotebookException
	{
		public NotebookCreateException(Exception innerException)
			: base("There was a problem creating the notebook.", innerException)
		{ }
	}
}
