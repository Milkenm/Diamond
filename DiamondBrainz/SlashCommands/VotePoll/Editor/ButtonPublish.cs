using System.Threading.Tasks;

using Diamond.API.Data;
using Diamond.API.Util;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.VotePoll
{
	public partial class VotePoll
	{
		/// <summary>
		/// Deletes the <see cref="EditorEmbed"/> and sends a <see cref="PublishedEmbed"/>. Also updates the <see cref="Poll.DiscordMessageId"/> to the message ID of the <see cref="PublishedEmbed"/>.
		/// <para>(Called when a user clicks the "Publish" button on the <see cref="EditorEmbed"/>)</para>
		/// </summary>
		/// <param name="messageId">The ID of the message containing the clicked button.</param>
		/// <returns></returns>
		[ComponentInteraction("button_publish:*", true)]
		public async Task ButtonPublishHandlerAsync(ulong messageId)
		{
			await this.DeferAsync();
			DiamondContext db = new DiamondContext();

			await Utils.DeleteResponseAsync(this.Context);

			Poll poll = VoteUtils.GetPollByMessageId(db, messageId);
			PublishedEmbed publishedEmbed = new PublishedEmbed(this.Context, poll);

			ulong responseId = await publishedEmbed.SendAsync(sendAsNew: true);

			poll.IsPublished = true;
			poll.DiscordMessageId = responseId;
			await db.SaveAsync();
		}
	}
}
