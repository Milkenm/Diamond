namespace Diamond.API.SlashCommands.Notebooks.Exceptions
{
	public class NoNotebooksException : NotebooksException
	{
		public NoNotebooksException()
			: base("You don't have any notebooks.")
		{ }
	}
}
