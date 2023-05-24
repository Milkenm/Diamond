using Diamond.API.Data;

using Discord;

namespace Diamond.API.SlashCommands.VotePoll.Embeds;
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
