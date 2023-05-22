using System.Collections.Generic;
using System.Threading.Tasks;

using Diamond.API.Attributes;

using Discord;
using Discord.Interactions;

namespace Diamond.API.SlashCommands.Moderation;
public partial class Moderation
{
	[RequireBotPermission(GuildPermission.ManageMessages)]
	[DefaultMemberPermissions(GuildPermission.ManageMessages)]
	[DSlashCommand("prune", "Delete messages from a channel.")]
	public async Task PruneCommandAsync(
		[Summary("amount", "The amount of messages to delete.")] int amount,
		[Summary("only-delete-bot-messages", "Only delete messages from bots in the last \"amount\" messages.")] bool onlyDeleteBotMessages = false,
		[ShowEveryone] bool showEveryone = true
	)
	{
		await this.DeferAsync(!showEveryone);
		DefaultEmbed embed = new DefaultEmbed("Prune", "🔥", this.Context.Interaction);

		amount++;

		IUserMessage responseMessage = await this.GetOriginalResponseAsync();

		ICollection<IMessage> messages = (ICollection<IMessage>)await this.Context.Channel.GetMessagesAsync(amount).FlattenAsync();
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
			await ((ITextChannel)this.Context.Channel).DeleteMessagesAsync(messagesToDeleteList);

			embed.WithDescription("Messages deleted!");
			embed.AddField("🗑 Amount of Messages", messagesToDeleteList.Count, true);
		}
		else
		{
			embed.WithDescription("No messages to delete.");
		}

		ComponentBuilder component = new ComponentBuilder()
			.WithButton("Delete", "delete_prune", ButtonStyle.Danger, Emoji.Parse("🗑️"));

		await embed.SendAsync(component.Build());
	}

	[ComponentInteraction("delete_prune")]
	public async Task PruneButtonAsync()
	{
		await this.DeferAsync();
		await (await this.Context.Interaction.GetOriginalResponseAsync()).DeleteAsync();
	}
}
