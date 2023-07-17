namespace Diamond.Data.Exceptions.NotebookPageExceptions
{
	public class NotebookPageDeleteException : NotebookPageException
	{
		public NotebookPageDeleteException(Exception innerException)
			: base("There was a problem deleting the notebook page.", innerException)
		{ }
	}
}
