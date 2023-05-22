using System;
using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.Data;
using Diamond.API.SlashCommands.Vote.Embeds;

using Discord.Interactions;
using Discord.WebSocket;

namespace Diamond.API.SlashCommands.Vote;

public class Vote : InteractionModuleBase<SocketInteractionContext>
{
	private readonly DiscordSocketClient _client;
	private readonly DiamondDatabase _database;

	private static bool _initializedEvents = false;

	public Vote(DiscordSocketClient client, DiamondDatabase database)
	{
		this._client = client;
		this._database = database;

		if (!_initializedEvents)
		{
			this._client.SelectMenuExecuted += new Func<SocketMessageComponent, Task>(async (menu) =>
			{
				EditorVoteEmbed.SelectMenuHandlerAsync(menu, this._database, this._client).GetAwaiter();
				VoteEmbed.SelectMenuHandlerAsync(menu, this._database, this._client).GetAwaiter();
			});
			this._client.ButtonExecuted += new Func<SocketMessageComponent, Task>((button) =>
			{
				EditorVoteEmbed.ButtonHandlerAsync(button, this._client, this._database).GetAwaiter();
				PublishedVoteEmbed.ButtonHandlerAsync(button, this._database).GetAwaiter();
				VoteEmbed.ButtonHandlerAsync(button, this._database, this._client).GetAwaiter();

				return Task.CompletedTask;
			});
			_initializedEvents = true;
		}
	}

	[DSlashCommand("poll", "Create a vote poll.")]
	public async Task VoteCommandAsync(
		[Summary("title", "The title of the poll.")][MinLength(1)][MaxLength(250)] string title,
		[Summary("description", "The description of the poll.")][MaxLength(4000)] string description,
		[Summary("image-url", "The URL for the main (bottom) image.")] string imageUrl = null,
		[Summary("thumbnail-url", "The URL for the thumbnail (top-right) image.")] string thumbnailUrl = null
	)
	{
		await this.DeferAsync(true);

		Poll poll = await this.CreatePollAsync(title, description, imageUrl, thumbnailUrl);

		ulong deferId = (await this.GetOriginalResponseAsync()).Id;

		await new EditorVoteEmbed(this.Context.Interaction, this._database, poll, deferId).SendAsync(true);
	}

	private async Task<Poll> CreatePollAsync(string pollTitle, string pollDescription, string pollImageUrl, string pollThumbnailUrl)
	{
		Poll newPoll = new Poll()
		{
			DiscordMessageId = (await this.Context.Interaction.GetOriginalResponseAsync()).Id,
			DiscordUserId = this.Context.Interaction.User.Id,
			Title = pollTitle,
			Description = pollDescription,
			ImageUrl = pollImageUrl,
			ThumbnailUrl = pollThumbnailUrl,
			CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
			UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
		};

		this._database.Add(newPoll);
		await this._database.SaveAsync();

		return newPoll;
	}
}