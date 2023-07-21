namespace Diamond.Data.Exceptions.NotebookExceptions
{
	public class NotebookLimitReachedException : NotebookException
	{
		public NotebookLimitReachedException(int limit)
			: base($"You reached the maximum limit ({limit}) of notebooks.")
		{ }
	}
}
