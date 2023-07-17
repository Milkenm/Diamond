namespace Diamond.Data.Exceptions.NotebookExceptions
{
	public class NotebookAlreadyExistsException : NotebookException
	{
		public NotebookAlreadyExistsException(string notebookName)
			: base($"A notebook called '{notebookName}' already exists.")
		{ }
	}
}
