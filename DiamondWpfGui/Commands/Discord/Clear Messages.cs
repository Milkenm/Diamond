using Diamond.WPF.Utils;

using Discord;
using Discord.Commands;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Diamond.WPF.Commands
{
    public partial class DiscordModule : ModuleBase<SocketCommandContext>
    {
        [Name("Clear Messsage"), Command("clearmessages"), Alias("clear", "prune", "purge"), Summary("Deletes the first 'x' messages from the current channel.")]
        public async Task ClearMessage(int amount, bool onlyBots = false, bool keepMessage = false)
        {
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithAuthor("Clear Messages", Twemoji.GetEmojiUrlFromEmoji("🗑"));

            if (!onlyBots)
            {
                ICollection<IMessage> messages = (ICollection<IMessage>)await Context.Channel.GetMessagesAsync(amount + 1).FlattenAsync().ConfigureAwait(false);

                await ((ITextChannel)Context.Channel).DeleteMessagesAsync(messages).ConfigureAwait(false);

                embed.WithDescription("Deleted messages: " + messages.Count);
            }
            else
            {
                ICollection<IMessage> messages = (ICollection<IMessage>)await Context.Channel.GetMessagesAsync(500).FlattenAsync().ConfigureAwait(false);
                List<IMessage> delMessages = new List<IMessage>();

                foreach (IMessage msg in messages)
                {
                    if (msg.Author.IsBot)
                    {
                        delMessages.Add(msg);
                    }
                }

                await ((ITextChannel)Context.Channel).DeleteMessagesAsync(delMessages).ConfigureAwait(false);

                embed.WithDescription("**Deleted Messages:** " + (delMessages.Count > 0 ? delMessages.Count.ToString() : "0\n❌ **Error:** No messages found!"));
            }

            IUserMessage reply = await ReplyAsync(embed: Embeds.FinishEmbed(embed, Context)).ConfigureAwait(false);

            if (!keepMessage)
            {
                await Task.Delay(3000).ConfigureAwait(false);
                await reply.DeleteAsync().ConfigureAwait(false);
            }
        }
    }
}