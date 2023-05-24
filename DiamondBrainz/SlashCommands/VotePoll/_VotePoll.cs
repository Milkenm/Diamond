using System;
using System.Threading.Tasks;

using Diamond.API.SlashCommands.VotePoll.Embeds;

using Discord.Interactions;
using Discord.WebSocket;

namespace Diamond.API.SlashCommands.VotePoll
{
	public partial class VotePoll : InteractionModuleBase<SocketInteractionContext>
	{
		private readonly DiscordSocketClient _client;

		private static bool _initializedEvents = false;

		public VotePoll(DiscordSocketClient client)
		{
			this._client = client;

			if (!_initializedEvents)
			{
				this._client.SelectMenuExecuted += new Func<SocketMessageComponent, Task>(async (menu) =>
				{
					EditorVoteEmbed.SelectMenuHandlerAsync(menu, this._client).GetAwaiter();
					VoteEmbed.SelectMenuHandlerAsync(menu, this._client).GetAwaiter();
				});
				this._client.ButtonExecuted += new Func<SocketMessageComponent, Task>((button) =>
				{
					EditorVoteEmbed.ButtonHandlerAsync(button, this._client).GetAwaiter();
					PublishedVoteEmbed.ButtonHandlerAsync(button).GetAwaiter();
					VoteEmbed.ButtonHandlerAsync(button, this._client).GetAwaiter();

					return Task.CompletedTask;
				});
				_initializedEvents = true;
			}
		}
	}
}
