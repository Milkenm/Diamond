using System;
using System.Threading.Tasks;

using Diamond.API.Bot;
using Diamond.API.Data;
using Diamond.API.SlashCommands.Vote.Embeds;

using Discord.Interactions;
using Discord.WebSocket;

using static Diamond.API.Data.PollsContext;

namespace Diamond.API.SlashCommands.Vote;

public class Vote : InteractionModuleBase<SocketInteractionContext>
{
	private readonly DiamondBot _bot;
	private readonly PollsContext _pollsDb;

	private static bool _initializedEvents = false;

	public Vote(DiamondBot bot, PollsContext pollsDb)
	{
		_bot = bot;
		_pollsDb = pollsDb;

		if (!_initializedEvents)
		{
			_bot.Client.SelectMenuExecuted += new Func<SocketMessageComponent, Task>(async (menu) =>
			{
				EditorVoteEmbed.SelectMenuHandlerAsync(menu, _pollsDb, _bot.Client).GetAwaiter();
				VoteEmbed.SelectMenuHandlerAsync(menu, _pollsDb, _bot.Client).GetAwaiter();
			});
			_bot.Client.ButtonExecuted += new Func<SocketMessageComponent, Task>((button) =>
			{
				EditorVoteEmbed.ButtonHandlerAsync(button, _bot.Client, _pollsDb).GetAwaiter();
				PublishedVoteEmbed.ButtonHandlerAsync(button, _pollsDb).GetAwaiter();
				VoteEmbed.ButtonHandlerAsync(button, _pollsDb, _bot.Client).GetAwaiter();

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

		await new EditorVoteEmbed(Context.Interaction, _pollsDb, poll, deferId).SendAsync(true);
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

		_pollsDb.Add(newPoll);
		await _pollsDb.SaveChangesAsync();

		return newPoll;
	}
}