namespace Diamond.Data.Exceptions.NotebookPageExceptions
{
	public class NotebookPageCreateException : NotebookPageException
	{
		public NotebookPageCreateException(Exception innerException)
			: base("There was a problem creating the notebook page.", innerException)
		{ }
	}
}
