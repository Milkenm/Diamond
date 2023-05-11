using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Discord;
using Discord.Interactions;
using Discord.WebSocket;

using ScriptsLibV2.Extensions;

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
			ChannelPermissions permissions = (this.Context.User as SocketGuildUser).GetPermissions(textChannel);
			if (permissions.ViewChannel && permissions.SendMessages)
			{
				textChannels.Add(new SelectMenuOptionBuilder(textChannel.Name, textChannel.Id.ToString()));
			}
		}

		ComponentBuilder component = new ComponentBuilder()
			.WithSelectMenu($"xmention_channel_selector:{message.Id}", textChannels, "Select a channel");

		await embed.SendAsync(component.Build());
	}

	[ComponentInteraction("xmention_channel_selector:*")]
	public async Task XChannelSelection(ulong messageId, string selectedChannelId)
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
