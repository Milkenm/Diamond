namespace Diamond.Data.Exceptions.NotebookPageExceptions
{
	public class NotebookPageLimitReachedException : NotebookPageException
	{
		public NotebookPageLimitReachedException(int limit)
			: base($"You reached the maximum limit ({limit}) of notebook pages.")
		{ }
	}
}
