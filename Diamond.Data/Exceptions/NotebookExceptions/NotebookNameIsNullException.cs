namespace Diamond.Data.Exceptions.NotebookExceptions
{
	public class NotebookNameIsNullException : NotebookException
	{
		public NotebookNameIsNullException()
			: base("The name of the notebook cannot be null.")
		{ }
	}
}
