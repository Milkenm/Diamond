namespace Diamond.Data.Exceptions.NotebookPageExceptions
{
	public class NotebookPageNotChangedException : NotebookPageException
	{
		public NotebookPageNotChangedException()
			: base("There were no changed made to the notebook page.")
		{ }
	}
}
