﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Diamond.API.ConsoleCommands
{
	public partial class ConsoleCommandsManager
	{
		private readonly IServiceProvider _serviceProvider;

		private readonly Dictionary<string, IConsoleCommand> _commandsList = new Dictionary<string, IConsoleCommand>();

		public ConsoleCommandsManager(IServiceProvider serviceProvider)
		{
			this._serviceProvider = serviceProvider;

			// Load all commands
			Type consoleCommandType = typeof(IConsoleCommand);
			IEnumerable<Type> foundTypes = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(assembly => assembly.GetTypes())
				.Where(consoleCommandType.IsAssignableFrom);

			foreach (Type type in foundTypes)
			{
				try
				{
					IConsoleCommand consoleCommand = (IConsoleCommand)Activator.CreateInstance(type, args: this._serviceProvider);
					CommandInfoAttribute attribute = type.GetCustomAttribute<CommandInfoAttribute>();

					this._commandsList.Add(attribute.CommandName, consoleCommand);
				}
				catch { }
			}
		}

		public bool CommandExists(string commandName) => this._commandsList.ContainsKey(commandName);

		public string RunCommand(string commandName, string[] args)
		{
			if (!this.CommandExists(commandName))
			{
				throw new ConsoleCommandNotFoundException(commandName);
			}
			IConsoleCommand command = this._commandsList[commandName];
			return command.Function.Invoke(args);
		}
	}
}