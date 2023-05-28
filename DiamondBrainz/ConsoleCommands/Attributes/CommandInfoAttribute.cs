using System;

namespace Diamond.API.ConsoleCommands
{
	public class CommandInfoAttribute : Attribute
	{
		public string CommandName { get; private set; }

		public CommandInfoAttribute(string commandName)
		{
			this.CommandName = commandName;
		}
	}
}