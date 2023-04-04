using Discord;

using static Diamond.API.Data.PollsContext;

namespace Diamond.API.SlashCommands.Vote.Embeds;
public abstract class BaseVoteEmbed : DefaultEmbed
{
	public BaseVoteEmbed(IDiscordInteraction interaction, Poll poll) : base($"Poll", "🗳️", interaction)
	{
		Title = poll.Title;
		Description = poll.Description;
		if (poll.ImageUrl != null)
		{
			ImageUrl = poll.ImageUrl;
		}
		if (poll.ThumbnailUrl != null)
		{
			ThumbnailUrl = poll.ThumbnailUrl;
		}
	}
}
