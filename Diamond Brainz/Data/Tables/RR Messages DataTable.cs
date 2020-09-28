using Discord;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using static Diamond.Brainz.Structures.ReactionRoles;

namespace Diamond.Brainz.Data.Tables
{
    public class RRMessagesDataTable
    {
        public RRMessagesDataTable()
        {
            DTable.Columns.Add(nameof(Column.MessageId), typeof(ulong));
            DTable.Columns.Add(nameof(Column.ChannelId), typeof(ulong));
            DTable.Columns.Add(nameof(Column.Embed), typeof(EmbedBuilder));
            DTable.Columns.Add(nameof(Column.IsFinished), typeof(bool));
            DTable.Columns.Add(nameof(Column.RoleLines), typeof(List<RoleLine>));
        }

        public static DataTable DTable = new DataTable();
        private static readonly EnumerableRowCollection<DataRow> MessagesTableEnumerator = DTable.AsEnumerable();

        public enum Column
        {
            MessageId,
            ChannelId,
            Embed,
            RoleLines,
            IsFinished,
        }

        public void AddMessage(ulong messageId, ulong channelId, EmbedBuilder embed, bool isFinished, List<RoleLine> roleLines = null)
        {
            DTable.Rows.Add(messageId, channelId, embed, isFinished, roleLines);
        }

        public List<ulong> GetMessages()
        {
            return MessagesTableEnumerator.Select(r => r.Field<ulong>(nameof(Column.MessageId))).ToList();
        }

        public List<ulong> GetChannels()
        {
            return MessagesTableEnumerator.Select(r => r.Field<ulong>(nameof(Column.ChannelId))).ToList();
        }

        public List<ulong> GetMessagesByChannelId(ulong channelId)
        {
            return DTable.Select($"{nameof(Column.ChannelId)}={channelId}").Select(r => r.Field<ulong>(nameof(Column.MessageId))).ToList();
        }

        public ulong GetLatestChannelMessage(ulong channelId)
        {
            List<ulong> messages = GetMessagesByChannelId(channelId);

            return messages.Count > 0 ? messages[^1] : throw new Exception("No messages found for this channel.");
        }

        public EmbedBuilder GetMessageEmbed(ulong messageId)
        {
            return DTable.Select($"{nameof(Column.MessageId)}={messageId}").Select(r => r.Field<EmbedBuilder>(nameof(Column.Embed))).ToList()[0];
            // return embedList.Count > 0 ? embedList[0] : throw new Exception("No embed found for the message.");
        }

        public void RemoveMessage(ulong messageId)
        {
            List<ulong> messagesIds = GetMessages();
            int gameIndex = messagesIds.IndexOf(messageId);

            DTable.Rows.RemoveAt(gameIndex);
        }

        public void AddRoleLine(ulong messageId, IRole role, EmoteType emoteType, dynamic emote, string description)
        {
            AddRoleLine(messageId, new RoleLine(role, emoteType, emote, description));
        }

        public void AddRoleLine(ulong messageId, RoleLine roleLine)
        {
            object rlList = DTable.Rows[GetRowIndexOfMessage(messageId)][nameof(Column.RoleLines)];
            if (rlList.GetType() == typeof(DBNull))
            {
                rlList = new List<RoleLine>();
            }

            ((List<RoleLine>)rlList).Add(roleLine);

            DTable.Rows[GetRowIndexOfMessage(messageId)][nameof(Column.RoleLines)] = rlList;
        }

        public DataRow GetRowByMessage(ulong messageId)
        {
            DataRow[] selection = DTable.Select($"{nameof(Column.MessageId)}={messageId}");
            return selection[0];
        }

        public int GetRowIndexOfMessage(ulong messageId)
        {
            DataRow row = GetRowByMessage(messageId);

            return DTable.Rows.IndexOf(row);
        }
    }
}
