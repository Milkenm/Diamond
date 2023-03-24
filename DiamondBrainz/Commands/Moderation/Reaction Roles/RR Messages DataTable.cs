namespace Diamond.Brainz.Data.DataTables
{
	public class RRMessagesDataTable
	{
		//public RRMessagesDataTable()
		//{
		//	DTable.TableName = "RR Messages DataTable";

		//	DTable.Columns.Add(nameof(Column.MessageId), typeof(ulong));
		//	DTable.Columns.Add(nameof(Column.ChannelId), typeof(ulong));
		//	DTable.Columns.Add(nameof(Column.GuildId), typeof(ulong));
		//	DTable.Columns.Add(nameof(Column.AuthorId), typeof(ulong));
		//	DTable.Columns.Add(nameof(Column.RoleEntry), typeof(List<RoleEntry>));
		//	DTable.Columns.Add(nameof(Column.EmbedTitle), typeof(string));
		//	DTable.Columns.Add(nameof(Column.EmbedDescription), typeof(string));
		//	DTable.Columns.Add(nameof(Column.IsEditing), typeof(bool));

		//	DTable.PrimaryKey = new DataColumn[] { DTable.Columns[nameof(Column.MessageId)] };
		//}

		//public enum Column
		//{
		//	MessageId,
		//	ChannelId,
		//	GuildId,
		//	AuthorId,
		//	RoleEntry,
		//	EmbedTitle,
		//	EmbedDescription,
		//	IsEditing,
		//}

		//public DataTable DTable = new DataTable();

		//public RRMessage this[ulong messageId]
		//{
		//	get
		//	{
		//		DataRow[] selection = DTable.Select($"{nameof(Column.MessageId)}={messageId}");

		//		if (selection.Length > 0)
		//		{
		//			ulong channelId = (ulong)selection[0][nameof(Column.ChannelId)];
		//			ulong guildId = (ulong)selection[0][nameof(Column.GuildId)];
		//			ulong authorId = (ulong)selection[0][nameof(Column.AuthorId)];
		//			List<RoleEntry> roleEntry = (List<RoleEntry>)selection[0][nameof(Column.RoleEntry)];
		//			string embedTitle = (string)selection[0][nameof(Column.EmbedTitle)];
		//			string embedDescription = (string)selection[0][nameof(Column.EmbedDescription)];
		//			bool isEditing = (bool)selection[0][nameof(Column.IsEditing)];

		//			return new RRMessage(messageId, channelId, guildId, authorId, roleEntry, embedTitle, embedDescription, isEditing);
		//		}
		//		else
		//		{
		//			return null;
		//		}
		//	}
		//}

		//public void NewMessage(ulong messageId, ulong channelId, ulong guildId, ulong authorId)
		//{
		//	DTable.Rows.Add(messageId, channelId, guildId, authorId, new List<RoleEntry>(), "", "", true);
		//}

		//public void DeleteMessage(ulong messageId)
		//{
		//	DataRow[] selection = DTable.Select($"{nameof(Column.MessageId)}={messageId}");

		//	if (selection.Length > 0)
		//	{
		//		ulong msgId = (ulong)selection[0][nameof(Column.MessageId)];
		//		RRMessage msg = this[msgId];

		//		msg.DeleteMessage();

		//		DTable.Rows.RemoveAt(DTable.Rows.IndexOf(selection[0]));
		//	}
		//	else
		//	{
		//		throw new Exception("Message not found.");
		//	}
		//}

		//public void UpdateMessage(RRMessage rrMsg)
		//{
		//	DataRow[] selection = DTable.Select($"{nameof(Column.MessageId)}={rrMsg.MessageId}");

		//	if (selection.Length > 0)
		//	{
		//		selection[0][nameof(Column.RoleEntry)] = rrMsg.RoleEntriesList;
		//		selection[0][nameof(Column.EmbedTitle)] = rrMsg.EmbedTitle;
		//		selection[0][nameof(Column.EmbedDescription)] = rrMsg.EmbedDescription;
		//		selection[0][nameof(Column.IsEditing)] = rrMsg.IsEditing;
		//	}
		//	else
		//	{
		//		throw new Exception("Message not found!");
		//	}
		//}

		//public ulong GetLatestChannelMessageId(ulong channelId)
		//{
		//	IEnumerable<ulong> selection = DTable.Select($"{nameof(Column.ChannelId)}={channelId}").Select(r => r.Field<ulong>(nameof(Column.MessageId)));

		//	if (selection.Any())
		//	{
		//		return selection.Last();
		//	}
		//	else
		//	{
		//		throw new Exception("No messages found on the current channel.");
		//	}
		//}

		//public RRMessage GetMessageById(ulong messageId)
		//{
		//	RRMessage rrMsg = this[messageId];

		//	if (rrMsg != null)
		//	{
		//		return rrMsg;
		//	}
		//	else
		//	{
		//		throw new Exception("Message not found.");
		//	}
		//}

		//public RRMessage GetEditingMessageByChannelId(ulong channelId)
		//{
		//	DataRow[] selection = DTable.Select($"{nameof(Column.ChannelId)}={channelId} AND {nameof(Column.IsEditing)}={true}");

		//	if (selection.Length > 0)
		//	{
		//		ulong messageId = (ulong)selection[0][nameof(Column.MessageId)];
		//		return this[messageId];
		//	}
		//	else
		//	{
		//		return null;
		//	}
		//}

		//public void HandleReactionAdded(SocketReaction reaction)
		//{
		//	this[reaction.MessageId]?.GiveRoleToUser(reaction);
		//}

		//public void HandleReactionRemoved(SocketReaction reaction)
		//{
		//	this[reaction.MessageId]?.RemoveRoleFromUser(reaction);
		//}
	}
}
