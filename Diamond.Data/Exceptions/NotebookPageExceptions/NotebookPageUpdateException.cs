namespace Diamond.Data.Exceptions.NotebookPageExceptions
{
	public class NotebookPageUpdateException : NotebookPageException
	{
		public NotebookPageUpdateException(Exception innerException)
			: base("There was a problem updating the notebook page.", innerException)
		{ }
	}
}
