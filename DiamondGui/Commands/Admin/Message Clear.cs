using Discord;
using Discord.Commands;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondGui.Commands
{
	public class Message_Clear : ModuleBase<SocketCommandContext>
	{
		[Command("message_clear"), Alias("mc"), Summary("Message Clear")]
		public async Task CMD_Message(int amount, bool onlyBots = false)
		{
			IEnumerable<IMessage> messages = await Context.Channel.GetMessagesAsync(amount + 1).FlattenAsync().ConfigureAwait(false);

			if (!onlyBots)
			{
				await ((ITextChannel)Context.Channel).DeleteMessagesAsync(messages).ConfigureAwait(false);
			}
			else
			{
				List<IMessage> delMessages = new List<IMessage>();

				foreach (IMessage msg in messages)
				{
					if (msg.Author.IsBot)
					{
						delMessages.Add(msg);
					}
				}

				await ((ITextChannel)Context.Channel).DeleteMessagesAsync(delMessages).ConfigureAwait(false);
			}

			IUserMessage reply = await ReplyAsync($"{amount} messages deleted.").ConfigureAwait(false);
			await Task.Delay(3000).ConfigureAwait(false);
			await reply.DeleteAsync().ConfigureAwait(false);
		}
	}
}