namespace Diamond.Data.Exceptions.NotebookExceptions
{
	public class NotebookNotFoundException : NotebookException
	{
		public NotebookNotFoundException(string notebookName)
			: base($"A notebook named '{notebookName}' could not be found.")
		{ }

		public NotebookNotFoundException(long? notebookId)
			: base($"A notebook with ID '{notebookId}' could not be found.")
		{ }
	}
}
