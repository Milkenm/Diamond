namespace Diamond.Data.Exceptions.NotebookPageExceptions
{
	public class NotebookPageTitleIsNullException : NotebookPageException
	{
		public NotebookPageTitleIsNullException()
			: base("The title of the page cannot be null.")
		{ }
	}
}
