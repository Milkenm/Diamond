using System;

namespace Diamond.API.ConsoleCommands.Attributes;
public class CommandInfoAttribute : Attribute
{
    public string CommandName { get; private set; }

    public CommandInfoAttribute(string commandName)
    {
        CommandName = commandName;
    }
}
