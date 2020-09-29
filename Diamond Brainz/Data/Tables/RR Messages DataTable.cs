using Diamond.Brainz.Structures.ReactionRoles;

using Discord;
using Discord.Commands;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Diamond.Brainz.Data.Tables
{
	public class RRMessagesDataTable : IEnumerable
	{
		public RRMessagesDataTable()
		{
			DTable.Columns.Add(nameof(Column.MessageId), typeof(ulong));
			DTable.Columns.Add(nameof(Column.ChannelId), typeof(ulong));
			DTable.Columns.Add(nameof(Column.RRMessage), typeof(RRMessage));
			DTable.Columns.Add(nameof(Column.IsEditing), typeof(bool));

			DTable.PrimaryKey = new DataColumn[] { DTable.Columns[nameof(Column.MessageId)] };
		}

		private static DataTable DTable = new DataTable();

		public RRMessage this[int index]
		{
			get
			{
				return (RRMessage)DTable.Rows[index][nameof(Column.RRMessage)];
			}
		}

		public enum Column
		{
			MessageId,
			ChannelId,
			RRMessage,
			IsEditing,
		}

		public void AddMessage(SocketCommandContext context, IUserMessage reply, EmbedBuilder embed, RoleLinesList roleLinesList = null)
		{
			AddMessage(new RRMessage(context, reply, embed, roleLinesList ?? new RoleLinesList()));
		}

		public void AddMessage(RRMessage rrMessage, bool isEditing = true)
		{
			DTable.Rows.Add(rrMessage.GetReplyId(), rrMessage.GetChannelId(), rrMessage, isEditing);
		}

		public void StartEditing(ulong messageId)
		{
			DataRow row = GetRowByMessageId(messageId);
			UpdateRow(row, isEditing: true);
		}

		public void StopEditing(ulong messageId)
		{
			DataRow row = GetRowByMessageId(messageId);
			UpdateRow(row, isEditing: false);
		}

		public RRMessage GetRRMessageByMessageId(ulong messageId)
		{
			return (RRMessage)DTable.Select($"{nameof(Column.MessageId)}={messageId}")[0][nameof(Column.RRMessage)];
		}

		public RRMessage GetRRMessageByChannelId(ulong channelId)
		{
			DataRow[] selection = DTable.Select($"{nameof(Column.ChannelId)}={channelId} AND {nameof(Column.IsEditing)}={true}");
			if (selection.Count() > 0)
			{
				return (RRMessage)selection[0][nameof(Column.RRMessage)];
			}
			else
			{
				throw new Exception("There are no messages being edited on this channel.");
			}
		}

		public void UpdateRRMessage(ulong messageId, RRMessage rrMessage)
		{
			DataRow row = GetRowByMessageId(messageId);
			UpdateRow(row, rrMessage: rrMessage);
		}

		public void DeleteRRMessage(ulong messageId)
		{
			DataRow row = GetRowByMessageId(messageId);
			DTable.Rows.Remove(row);
		}

		private DataRow GetRowByMessageId(ulong messageId)
		{
			return DTable.Rows.Find(messageId);
		}

		private int GetTableIndexByMessageId(ulong messageId)
		{
			DataRow row = GetRowByMessageId(messageId);
			return DTable.Rows.IndexOf(row);
		}

		private void UpdateRow(int rowIndex, ulong? messageId = null, ulong? channelId = null, RRMessage rrMessage = null, bool? isEditing = null)
		{
			DataRow row = DTable.Rows[rowIndex];
			UpdateRow(row, messageId, channelId, rrMessage, isEditing);
		}

		private void UpdateRow(DataRow row, ulong? messageId = null, ulong? channelId = null, RRMessage rrMessage = null, bool? isEditing = null)
		{
			int rowIndex = DTable.Rows.IndexOf(row);

			DataRow newRow = DTable.NewRow();

			newRow[nameof(Column.MessageId)] = messageId ?? row[nameof(Column.MessageId)];
			newRow[nameof(Column.ChannelId)] = channelId ?? row[nameof(Column.ChannelId)];
			newRow[nameof(Column.RRMessage)] = rrMessage ?? row[nameof(Column.RRMessage)];
			newRow[nameof(Column.IsEditing)] = isEditing ?? row[nameof(Column.IsEditing)];

			DTable.Rows.RemoveAt(rowIndex);
			DTable.Rows.InsertAt(newRow, rowIndex);
		}

		public IEnumerator GetEnumerator()
		{
			return (IEnumerator<RRMessage>)DTable.Select($"{nameof(Column.RRMessage)}").GetEnumerator();
		}
	}
}
