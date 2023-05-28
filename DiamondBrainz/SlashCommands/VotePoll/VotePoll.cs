using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.Data;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.VotePoll
{
	public partial class VotePoll : InteractionModuleBase<SocketInteractionContext>
	{
		private readonly DiamondClient _client;

		public VotePoll(DiamondClient client)
		{
			this._client = client;
		}

		[EnabledInDm(false)]
		[DSlashCommand("poll", "Create a vote poll.")]
		public async Task VoteCommandAsync(
			[Summary("title", "The title of the poll.")][MinLength(1)][MaxLength(250)] string title,
			[Summary("description", "The description of the poll.")][MaxLength(4000)] string description,
			[Summary("image-url", "The URL for the main (bottom) image.")] string imageUrl = null,
			[Summary("thumbnail-url", "The URL for the thumbnail (top-right) image.")] string thumbnailUrl = null
		)
		{
			await this.DeferAsync(true);

			using DiamondContext db = new DiamondContext();

			ulong responseMessageId = (await this.Context.Interaction.GetOriginalResponseAsync()).Id;
			ulong userId = this.Context.Interaction.User.Id;
			Poll poll = await VoteUtils.CreatePollAsync(db, title, description, imageUrl, thumbnailUrl, responseMessageId, userId);

			ulong deferId = (await this.GetOriginalResponseAsync()).Id;

			_ = await new EditorEmbed(this.Context, poll, deferId).SendAsync(true);
		}
	}
}