namespace Diamond.API.Structures.ReactionRoles
{
	public class RRMessage
	{
		//public RRMessage() { }

		//public RRMessage(ulong messageId, ulong channelId, ulong guildId, ulong author, List<RoleEntry> roleEntriesList, string embedTitle, string embedDescription, bool isEditing)
		//{
		//	MessageId = messageId;
		//	ChannelId = channelId;
		//	GuildId = guildId;
		//	AuthorId = author;
		//	RoleEntriesList = roleEntriesList;
		//	EmbedTitle = embedTitle;
		//	EmbedDescription = embedDescription;
		//	IsEditing = isEditing;

		//	Message = (IUserMessage)GlobalData.DiamondCore.Client.GetGuild(GuildId).GetTextChannel(ChannelId).GetMessageAsync(MessageId).GetAwaiter().GetResult();
		//	Author = GlobalData.DiamondCore.Client.GetUser(AuthorId);
		//}

		//public ulong MessageId;
		//public ulong ChannelId;
		//public ulong GuildId;
		//public ulong AuthorId;
		//public List<RoleEntry> RoleEntriesList;
		//public string EmbedTitle;
		//public string EmbedDescription;
		//public bool IsEditing;

		//private readonly IUserMessage Message;
		//private readonly IUser Author;

		//public void SetTitle(string title)
		//{
		//	EmbedTitle = title;
		//	DiscordRefresh();
		//}

		//public void SetDescription(string description)
		//{
		//	EmbedDescription = description;
		//	DiscordRefresh();
		//}

		//public void AddRole(ulong roleId, string emote, params string[] description)
		//{
		//	foreach (RoleEntry entry in RoleEntriesList)
		//	{
		//		if (entry.RoleId == roleId || entry.Emote == emote)
		//		{
		//			throw new Exception("That role/emote already exists.");
		//		}
		//	}

		//	RoleEntriesList.Add(new RoleEntry(roleId, emote, string.Join(' ', description)));
		//	DiscordRefresh();
		//}

		//public void RemoveRole(ulong roleId)
		//{
		//	foreach (RoleEntry entry in RoleEntriesList)
		//	{
		//		if (entry.RoleId == roleId)
		//		{
		//			RoleEntriesList.Remove(entry);
		//			DiscordRefresh();
		//			return;
		//		}
		//	}

		//	throw new Exception("That role does not exist!");
		//}

		//public void RemoveRole(string emote)
		//{
		//	foreach (RoleEntry entry in RoleEntriesList)
		//	{
		//		if (entry.Emote == emote)
		//		{
		//			RoleEntriesList.Remove(entry);
		//			DiscordRefresh();
		//			return;
		//		}
		//	}

		//	throw new Exception("That emote does not exist!");
		//}

		//public RoleEntry GetEntryByEmote(string emote)
		//{
		//	foreach (RoleEntry entry in RoleEntriesList)
		//	{
		//		if (entry.Emote == emote)
		//		{
		//			return entry;
		//		}
		//	}
		//	return null;
		//}

		//public void GiveRoleToUser(SocketReaction reaction)
		//{
		//	RoleEntry re = GetEntryByEmote(reaction.Emote.Name);
		//	if (re != null)
		//	{
		//		IGuild guild = GlobalData.DiamondCore.Client.GetGuild(GuildId);
		//		IRole role = guild.GetRole(re.RoleId);

		//		guild.GetUserAsync(reaction.UserId).GetAwaiter().GetResult().AddRoleAsync(role);
		//	}
		//}

		//public void RemoveRoleFromUser(SocketReaction reaction)
		//{
		//	RoleEntry re = GetEntryByEmote(reaction.Emote.Name);
		//	if (re != null)
		//	{
		//		IGuild guild = GlobalData.DiamondCore.Client.GetGuild(GuildId);
		//		IRole role = guild.GetRole(re.RoleId);

		//		guild.GetUserAsync(reaction.UserId).GetAwaiter().GetResult().RemoveRoleAsync(role);
		//	}
		//}

		//public void StartEditing()
		//{
		//	if (!IsEditing)
		//	{
		//		IsEditing = true;
		//		UpdateMessageOnDataTable();
		//	}
		//}

		//public void StopEditing()
		//{
		//	if (IsEditing)
		//	{
		//		IsEditing = false;
		//		UpdateMessageOnDataTable();
		//	}
		//}

		//public void DeleteMessage()
		//{
		//	Message.DeleteAsync();
		//}

		//private void UpdateMessageOnDataTable()
		//{
		//	GlobalData.RRMessagesDataTable.UpdateMessage(this);
		//}

		//private async void DiscordRefresh()
		//{
		//	UpdateMessageOnDataTable();

		//	List<IEmote> emotes = new List<IEmote>();

		//	EmbedBuilder embed = new EmbedBuilder();
		//	embed.WithTitle(EmbedTitle);
		//	StringBuilder description = new StringBuilder(EmbedDescription);
		//	bool firstLoop = true;
		//	foreach (RoleEntry entry in RoleEntriesList)
		//	{
		//		if (firstLoop)
		//		{
		//			description.Append("\n");
		//			firstLoop = false;
		//		}

		//		IGuild guild = GlobalData.DiamondCore.Client.GetGuild(GuildId);
		//		IRole role = guild.GetRole(entry.RoleId);

		//		description.Append('\n').Append(entry.Emote).Append(" : ").Append(role.Mention).Append(" » ").Append(entry.Description);

		//		emotes.Add(Emotes.ParseEmote(entry.Emote));
		//	}
		//	embed.WithDescription(description.ToString());

		//	await Message.ModifyAsync(msg => msg.Embed = Embeds.FinishEmbed(embed, Author)).ConfigureAwait(false);

		//	List<IEmote> messageEmotes = Message.Reactions.Keys.ToList();
		//	foreach (IEmote emote in messageEmotes) // REMOVE EMOTES
		//	{
		//		if (!emotes.Contains(emote))
		//		{
		//			await Message.RemoveAllReactionsForEmoteAsync(emote).ConfigureAwait(false);
		//		}
		//	}
		//	foreach (IEmote emote in emotes) // ADD EMOTES
		//	{
		//		if (!messageEmotes.Contains(emote))
		//		{
		//			await Message.AddReactionAsync(emote).ConfigureAwait(false);
		//		}
		//	}
		//}
	}
}
