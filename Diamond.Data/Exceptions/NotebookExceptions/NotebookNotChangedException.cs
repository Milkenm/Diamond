namespace Diamond.Data.Exceptions.NotebookExceptions
{
	public class NotebookNotChangedException : NotebookException
	{
		public NotebookNotChangedException()
			: base("There were no changed made to the notebook.")
		{ }
	}
}
