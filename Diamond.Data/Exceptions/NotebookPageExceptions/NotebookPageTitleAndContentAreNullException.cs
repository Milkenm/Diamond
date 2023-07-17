namespace Diamond.Data.Exceptions.NotebookPageExceptions
{
	public class NotebookPageTitleAndContentAreNullException : NotebookPageException
	{
		public NotebookPageTitleAndContentAreNullException()
			: base("The title and content of the page cannot be null.")
		{ }
	}
}
