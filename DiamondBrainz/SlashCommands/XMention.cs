using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace Diamond.API.SlashCommands;
public class XMention : InteractionModuleBase<SocketInteractionContext>
{
	[MessageCommand("X-Mention")]
	public async Task XmentionCommandAsync(IMessage message)
	{
		await this.DeferAsync(true);

		IGuildUser guildAuthor = (IGuildUser)message.Author;

		DefaultEmbed embed = new DefaultEmbed("X-Mention", "🔗", this.Context.Interaction, message.GetJumpUrl())
		{
			Title = guildAuthor.DisplayName,
			Description = Utils.GetMessageContent(message),
		};

		List<SelectMenuOptionBuilder> textChannels = new List<SelectMenuOptionBuilder>();
		foreach (SocketTextChannel textChannel in this.Context.Guild.TextChannels)
		{
			// Ignore current channel
			if (textChannel == this.Context.Channel)
			{
				continue;
			}
			// Check permissions
			ChannelPermissions userPermissions = (this.Context.User as SocketGuildUser).GetPermissions(textChannel);
			ChannelPermissions botPermissions = this.Context.Guild.CurrentUser.GetPermissions(textChannel);
			if (userPermissions.ViewChannel && userPermissions.SendMessages && botPermissions.ViewChannel && botPermissions.SendMessages)
			{
				textChannels.Add(new SelectMenuOptionBuilder(textChannel.Name, textChannel.Id.ToString()));
			}
		}

		ComponentBuilder component = new ComponentBuilder()
			.WithSelectMenu($"xmention_channel_selector:{message.Id}", textChannels, "Select a channel");

		await embed.SendAsync(component.Build());
	}

	[ComponentInteraction("xmention_channel_selector:*")]
	public async Task XChannelSelectionAsync(ulong messageId, string selectedChannelId)
	{
		await this.DeferAsync();
		await (await this.Context.Interaction.GetOriginalResponseAsync()).DeleteAsync();

		IMessage message = await this.Context.Channel.GetMessageAsync(messageId);

		DefaultEmbed embed = new DefaultEmbed("X-Mention", "🔗", this.Context.Interaction, message.GetJumpUrl())
		{
			Title = (message.Author as IGuildUser).DisplayName,
			Description = Utils.GetMessageContent(message),
		};

		SocketGuildChannel channel = this.Context.Guild.GetChannel(Convert.ToUInt64(selectedChannelId));
		await (channel as SocketTextChannel).SendMessageAsync(embed: embed.Build());
	}
}
