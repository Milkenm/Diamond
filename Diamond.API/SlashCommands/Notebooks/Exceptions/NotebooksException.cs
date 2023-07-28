using System;

namespace Diamond.API.SlashCommands.Notebooks.Exceptions
{
	public class NotebooksException : Exception
	{
		public NotebooksException(string message)
			: base(message)
		{ }
	}
}
