using System;
using System.Collections.Generic;

using Discord.Rest;

namespace Diamond.API.ConsoleCommands
{
	[CommandInfo("delete-slash-commands")]
	public class DeleteSlashCommands : IConsoleCommand
	{
		private readonly DiamondClient _client;

		public DeleteSlashCommands(DiamondClient client)
		{
			this._client = client;
		}

		public Func<string[], string> Function => new Func<string[], string>((str) =>
		{
			IReadOnlyCollection<RestGlobalCommand> commands = this._client.Rest.GetGlobalApplicationCommands().GetAwaiter().GetResult();

			int removedCommands = 0;
			foreach (RestGlobalCommand cmd in commands)
			{
				_ = cmd.DeleteAsync().GetAwaiter();
				removedCommands++;
			}
			return $"Removed {removedCommands} commands.";
		});
	}
}
