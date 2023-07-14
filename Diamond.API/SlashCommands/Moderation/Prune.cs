using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.Helpers;
using Diamond.API.Util;

using Discord;
using Discord.Interactions;

namespace Diamond.API.SlashCommands.Moderation
{
    public partial class Moderation
	{
		[RequireBotPermission(GuildPermission.ManageMessages)]
		[RequireUserPermission(GuildPermission.ManageMessages)]
		[DefaultMemberPermissions(GuildPermission.ManageMessages)]
		[DSlashCommand("prune", "Delete messages from a channel.")]
		public async Task PruneCommandAsync(
			[Summary("amount", "The amount of messages to delete.")] int amount,
			[Summary("only-delete-bot-messages", "Only delete messages from bots in the last \"amount\" messages.")] bool onlyDeleteBotMessages = false,
			[ShowEveryone] bool showEveryone = true
		)
		{
			await this.DeferAsync(!showEveryone);
			DefaultEmbed embed = new DefaultEmbed("Prune", "🔥", this.Context);

			amount++;

			IUserMessage responseMessage = await this.GetOriginalResponseAsync();

			ICollection<IMessage> messages = (ICollection<IMessage>)await this.Context.Channel.GetMessagesAsync(amount).FlattenAsync();
			List<IMessage> messagesToDeleteList = new List<IMessage>();

			if (!onlyDeleteBotMessages)
			{
				messagesToDeleteList.AddRange(messages.Where(message => message.Id != responseMessage.Id));
			}
			else
			{
				messagesToDeleteList.AddRange(messages.Where(msg => msg.Author.IsBot && msg.Id != responseMessage.Id));
			}
			// Ignore messages older than 14 days (Discord doesn't allow bots to delete old messages)
			_ = messagesToDeleteList.RemoveAll(msg => msg.CreatedAt < System.DateTimeOffset.Now.AddDays(-14));

			if (messagesToDeleteList.Count > 0)
			{
				await ((ITextChannel)this.Context.Channel).DeleteMessagesAsync(messagesToDeleteList);

				_ = embed.WithDescription("Messages deleted!");
				_ = embed.AddField("🗑 Amount of Messages", messagesToDeleteList.Count, true);
			}
			else
			{
				_ = embed.WithDescription("No messages to delete.");
			}

			ComponentBuilder component = new ComponentBuilder()
				.WithButton("Delete", "delete_prune", ButtonStyle.Danger, Emoji.Parse("🗑️"));

			_ = await embed.SendAsync(component.Build());
		}

		[ComponentInteraction("delete_prune", true)]
		public async Task PruneButtonAsync()
		{
			await this.DeferAsync();
			await Utils.DeleteResponseAsync(this.Context);
		}
	}
}