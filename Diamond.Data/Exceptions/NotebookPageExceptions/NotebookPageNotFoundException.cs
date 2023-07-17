namespace Diamond.Data.Exceptions.NotebookPageExceptions
{
	public class NotebookPageNotFoundException : NotebookPageException
	{
		public NotebookPageNotFoundException(string title)
			: base($"A notebook page with titled '{title}' could not be found.")
		{ }

		public NotebookPageNotFoundException(long? pageId)
			: base($"A notebook page with ID '{pageId}' could not be found.")
		{ }
	}
}
