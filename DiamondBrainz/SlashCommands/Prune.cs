using System.Collections.Generic;
using System.Threading.Tasks;

using Discord;
using Discord.Interactions;

namespace Diamond.API.SlashCommands
{
	public class Prune : InteractionModuleBase<SocketInteractionContext>
	{
		[SlashCommand("prune", "Delete messages from a channel.")]
		public async Task PruneCommandAsync(
			[Summary("amount", "The amount of messages to delete.")] int amount,
			[Summary("only-delete-bot-messages", "Only delete messages from bots in the last \"amount\" messages.")] bool onlyDeleteBotMessages = false,
			[Summary("show-everyone", "Show the command output to everyone.")] bool showEveryone = true
		)
		{
			await DeferAsync(!showEveryone);

			amount++;

			DefaultEmbed embed = new DefaultEmbed("Prune", "🔥️", Context.Interaction);

			IUserMessage responseMessage = await GetOriginalResponseAsync();

			ICollection<IMessage> messages = (ICollection<IMessage>)await Context.Channel.GetMessagesAsync(amount).FlattenAsync();
			List<IMessage> messagesToDeleteList = new List<IMessage>();

			if (!onlyDeleteBotMessages)
			{
				foreach (IMessage message in messages)
				{
					if (message.Id != responseMessage.Id)
					{
						messagesToDeleteList.Add(message);
					}
				}
			}
			else
			{
				foreach (IMessage msg in messages)
				{
					if (msg.Author.IsBot && msg.Id != responseMessage.Id)
					{
						messagesToDeleteList.Add(msg);
					}
				}
			}

			if (messagesToDeleteList.Count > 0)
			{
				await ((ITextChannel)Context.Channel).DeleteMessagesAsync(messagesToDeleteList);

				embed.WithDescription("Messages deleted!");
				embed.AddField("🗑 Amount of Messages", messagesToDeleteList.Count, true);
			}
			else
			{
				embed.WithDescription("No messages to delete.");
			}

			await embed.SendAsync();
		}
	}
}
