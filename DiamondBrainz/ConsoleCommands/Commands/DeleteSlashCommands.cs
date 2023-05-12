using System;
using System.Collections.Generic;

using Discord.Rest;
using Discord.WebSocket;

using Microsoft.Extensions.DependencyInjection;

namespace Diamond.API.ConsoleCommands.Commands
{
	[CommandInfo("delete-slash-commands")]
	public class DeleteSlashCommands : IConsoleCommand
	{
		private readonly IServiceProvider _serviceProvider;

		private readonly DiscordSocketClient _client;

		public DeleteSlashCommands(IServiceProvider _serviceProvider)
		{
			this._serviceProvider = _serviceProvider;

			this._client = _serviceProvider.GetRequiredService<DiscordSocketClient>();
		}

		public Func<string[], string> Function => new Func<string[], string>((str) =>
		{
			IReadOnlyCollection<RestGlobalCommand> commands = this._client.Rest.GetGlobalApplicationCommands().GetAwaiter().GetResult();

			int removedCommands = 0;
			foreach (RestGlobalCommand cmd in commands)
			{
				cmd.DeleteAsync().GetAwaiter();
				removedCommands++;
			}
			return $"Removed {removedCommands} commands.";
		});
	}
}
