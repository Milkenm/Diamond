using System.Collections.Generic;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

using ScriptsLibV2.Util;

namespace Diamond.Brainz.Commands
{
	public partial class ModerationModule : ModuleBase<SocketCommandContext>
	{
		[Name("Clear Messsages"), Command("clearmessages"), Alias("clear", "prune", "purge", "cm"), Summary("Deletes the lastest 'x' messages from the current channel.")]
		public async Task ClearMessages(int amount, bool onlyBots = false, bool keepMessage = false)
		{
			amount++;

			EmbedBuilder embed = new EmbedBuilder();
			embed.WithAuthor("Clear Messages", TwemojiUtils.GetEmojiUrlFromEmoji("🗑"));

			if (!onlyBots)
			{
				ICollection<IMessage> messages = (ICollection<IMessage>)await this.Context.Channel.GetMessagesAsync(amount).FlattenAsync().ConfigureAwait(false);

				await ((ITextChannel)this.Context.Channel).DeleteMessagesAsync(messages).ConfigureAwait(false);

				embed.WithDescription("Deleted messages: " + messages.Count);
			}
			else
			{
				ICollection<IMessage> messages = (ICollection<IMessage>)await this.Context.Channel.GetMessagesAsync(amount).FlattenAsync().ConfigureAwait(false);
				List<IMessage> delMessages = new List<IMessage>();

				foreach (IMessage msg in messages)
				{
					if (msg.Author.IsBot)
					{
						delMessages.Add(msg);
					}
				}

				await ((ITextChannel)this.Context.Channel).DeleteMessagesAsync(delMessages).ConfigureAwait(false);

				embed.WithDescription("**Deleted Messages:** " + (delMessages.Count > 0 ? delMessages.Count.ToString() : "0\n❌ **Error:** No messages found!"));
			}

			IUserMessage reply = await this.ReplyAsync(embed: embed.FinishEmbed(this.Context)).ConfigureAwait(false);

			if (!keepMessage)
			{
				await Task.Delay(3000).ConfigureAwait(false);
				await reply.DeleteAsync().ConfigureAwait(false);
			}
		}
	}
}