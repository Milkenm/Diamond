namespace Diamond.Data.Exceptions.NotebookPageExceptions
{
	public class NotebookPageContentIsNullException : NotebookPageException
	{
		public NotebookPageContentIsNullException()
			: base("The content of the page cannot be null.")
		{ }
	}
}
