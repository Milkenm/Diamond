using Diamond.API.Helpers;
using Diamond.Data.Models.Polls;

using Discord;

using ScriptsLibV2.Extensions;

namespace Diamond.API.SlashCommands.VotePoll
{
	public abstract class BasePollEmbed : DefaultEmbed
	{
		public BasePollEmbed(IInteractionContext context, DbPoll poll) : base($"Poll", "🗳️", context)
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