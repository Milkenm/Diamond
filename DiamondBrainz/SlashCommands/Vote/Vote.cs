using System;
using System.Threading.Tasks;

using Diamond.API.Bot;
using Diamond.API.Data;
using Diamond.API.SlashCommands.Vote.Embeds;

using Discord.Interactions;
using Discord.WebSocket;

namespace Diamond.API.SlashCommands.Vote;

public class Vote : InteractionModuleBase<SocketInteractionContext>
{
	private readonly DiamondBot _bot;
	private readonly DiamondDatabase _database;

	private static bool _initializedEvents = false;

	public Vote(DiamondBot bot, DiamondDatabase database)
	{
		_bot = bot;
		_database = database;

		if (!_initializedEvents)
		{
			_bot.Client.SelectMenuExecuted += new Func<SocketMessageComponent, Task>(async (menu) =>
			{
				EditorVoteEmbed.SelectMenuHandlerAsync(menu, _database, _bot.Client).GetAwaiter();
				VoteEmbed.SelectMenuHandlerAsync(menu, _database, _bot.Client).GetAwaiter();
			});
			_bot.Client.ButtonExecuted += new Func<SocketMessageComponent, Task>((button) =>
			{
				EditorVoteEmbed.ButtonHandlerAsync(button, _bot.Client, _database).GetAwaiter();
				PublishedVoteEmbed.ButtonHandlerAsync(button, _database).GetAwaiter();
				VoteEmbed.ButtonHandlerAsync(button, _database, _bot.Client).GetAwaiter();

				return Task.CompletedTask;
			});
			_initializedEvents = true;
		}
	}

	[SlashCommand("poll", "Create a vote poll.")]
	public async Task VoteCommandAsync(
		[Summary("title", "The title of the poll.")][MinLength(1)][MaxLength(250)] string title,
		[Summary("description", "The description of the poll.")][MaxLength(4000)] string description,
		[Summary("image-url", "The URL for the main (bottom) image.")] string imageUrl = null,
		[Summary("thumbnail-url", "The URL for the thumbnail (top-right) image.")] string thumbnailUrl = null
	)
	{
		await DeferAsync(true);

		Poll poll = await CreatePollAsync(title, description,imageUrl, thumbnailUrl);

		ulong deferId = (await GetOriginalResponseAsync()).Id;

		await new EditorVoteEmbed(Context.Interaction, _database, poll, deferId).SendAsync(true);
	}

	private async Task<Poll> CreatePollAsync(string pollTitle, string pollDescription, string pollImageUrl, string pollThumbnailUrl)
	{
		Poll newPoll = new Poll()
		{
			DiscordMessageId = (await Context.Interaction.GetOriginalResponseAsync()).Id,
			DiscordUserId = Context.Interaction.User.Id,
			Title = pollTitle,
			Description = pollDescription,
			ImageUrl = pollImageUrl,
			ThumbnailUrl = pollThumbnailUrl,
			CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
			UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
		};

		_database.Add(newPoll);
		await _database.SaveChangesAsync();

		return newPoll;
	}
}