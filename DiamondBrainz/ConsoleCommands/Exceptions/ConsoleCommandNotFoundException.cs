using System;

namespace Diamond.API.ConsoleCommands
{
	public partial class ConsoleCommandsManager
	{
		public class ConsoleCommandNotFoundException : Exception
		{
			public ConsoleCommandNotFoundException(string commandName) : base($"The command '{commandName}' was not found.") { }
		}
	}
}