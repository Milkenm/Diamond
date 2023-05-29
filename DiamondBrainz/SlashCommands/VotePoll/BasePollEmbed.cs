using Diamond.API.Data;
using Diamond.API.Util;

using Discord;

using ScriptsLibV2.Extensions;

namespace Diamond.API.SlashCommands.VotePoll
{
	public abstract class BasePollEmbed : DefaultEmbed
	{
		public BasePollEmbed(IInteractionContext context, Poll poll) : base($"Poll", "🗳️", context)
		{
			this.Title = poll.Title;
			this.Description = poll.Description;
			if (!poll.ImageUrl.IsEmpty())
			{
				this.ImageUrl = poll.ImageUrl;
			}
			if (!poll.ThumbnailUrl.IsEmpty())
			{
				this.ThumbnailUrl = poll.ThumbnailUrl;
			}
		}
	}
}