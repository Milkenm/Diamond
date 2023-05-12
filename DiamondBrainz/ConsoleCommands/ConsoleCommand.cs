using System;

namespace Diamond.API.ConsoleCommands;
public interface IConsoleCommand
{
	public Func<string[], string> Function { get; }

	public string RunCommand(string[] args) => this.Function.Invoke(args);
}
