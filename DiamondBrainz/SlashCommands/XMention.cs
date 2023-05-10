using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.Bot;

using Discord;
using Discord.Interactions;
using Discord.WebSocket;

using ScriptsLibV2.Extensions;

namespace Diamond.API.SlashCommands;
public class XMention : InteractionModuleBase<SocketInteractionContext>
{
	private readonly DiamondBot _bot;

	public XMention(DiamondBot bot)
	{
		_bot = bot;
	}

	[MessageCommand("X-Mention")]
	public async Task XmentionCommandAsync(IMessage message)
	{
		await DeferAsync(true);

		IGuildUser guildAuthor = (IGuildUser)message.Author;

		DefaultEmbed embed = new DefaultEmbed("X-Mention", "🔗", Context.Interaction, message.GetJumpUrl())
		{
			Title = guildAuthor.DisplayName,
			Description = GetMessageContent(message),
		};

		List<SelectMenuOptionBuilder> textChannels = new List<SelectMenuOptionBuilder>();
		foreach (SocketTextChannel textChannel in Context.Guild.TextChannels)
		{
			ChannelPermissions permissions = (Context.User as SocketGuildUser).GetPermissions(textChannel);
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
		await DeferAsync();
		await (await Context.Interaction.GetOriginalResponseAsync()).DeleteAsync();

		IMessage message = await Context.Channel.GetMessageAsync(messageId);

		DefaultEmbed embed = new DefaultEmbed("X-Mention", "🔗", Context.Interaction, message.GetJumpUrl())
		{
			Title = (message.Author as IGuildUser).DisplayName,
			Description = GetMessageContent(message),
		};

		SocketGuildChannel channel = Context.Guild.GetChannel(Convert.ToUInt64(selectedChannelId));
		await (channel as SocketTextChannel).SendMessageAsync(embed: embed.Build());
	}

	private string? GetMessageContent(IMessage message)
	{
		if (message.Content.IsEmpty())
		{
			// Get content from embed
			if (message.Embeds.Count == 0) return null;

			return message.Embeds.ElementAt(0).Description;
		}
		return message.Content;
	}
}
