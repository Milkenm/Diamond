using Discord;

using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Diamond.WPF.Structures.Games
{
    public class TTTGamesDataTable
    {
        public TTTGamesDataTable()
        {
            GamesTable.Columns.Add(nameof(Column.Game), typeof(TTTGame));
            GamesTable.Columns.Add(nameof(Column.Host), typeof(IUser));
            GamesTable.Columns.Add(nameof(Column.Opponent), typeof(IUser));
            GamesTable.Columns.Add(nameof(Column.MessageId), typeof(ulong));
            GamesTable.Columns.Add(nameof(Column.ChannelId), typeof(ulong));
        }

        private static readonly DataTable GamesTable = new DataTable();
        private static readonly EnumerableRowCollection<DataRow> GamesTableEnumerator = GamesTable.AsEnumerable();

        public enum Column
        {
            Game,
            Host,
            Opponent,
            MessageId,
            ChannelId,
        }

        public void AddGame(TTTGame game, IUser host, IUser opponent, ulong msgId, ulong channelId)
        {
            GamesTable.Rows.Add(game, host, opponent, msgId, channelId);
        }

        public void UpdateGame(TTTGame game, Column column, object value)
        {
            List<TTTGame> games = GetGames();
            int gameIndex = games.IndexOf(game);

            GamesTable.Rows[gameIndex][column.ToString()] = value;
        }

        public void RemoveGame(TTTGame game)
        {
            List<TTTGame> games = GetGames();
            int gameIndex = games.IndexOf(game);

            GamesTable.Rows.RemoveAt(gameIndex);
        }

        public List<IUser> GetHosts()
        {
            return GamesTableEnumerator.Select(r => r.Field<IUser>("Host")).ToList();
        }

        public bool ContainsHost(IUser host)
        {
            return GetHosts().Contains(host);
        }

        public List<IUser> GetOpponents()
        {
            return GamesTableEnumerator.Select(r => r.Field<IUser>("Opponent")).ToList();
        }

        public bool ContainsOpponent(IUser opponent)
        {
            return GetOpponents().Contains(opponent);
        }

        public List<ulong> GetMessagesIds()
        {
            return GamesTableEnumerator.Select(r => r.Field<ulong>("MessageId")).ToList();
        }

        public bool ContainsMessageId(ulong msgId)
        {
            return GetMessagesIds().Contains(msgId);
        }

        public List<ulong> GetChannelsIds()
        {
            return GamesTableEnumerator.Select(r => r.Field<ulong>("ChannelId")).ToList();
        }

        public bool ContainsChannelId(ulong channelId)
        {
            return GetChannelsIds().Contains(channelId);
        }

        public List<TTTGame> GetGames()
        {
            return GamesTableEnumerator.Select(r => r.Field<TTTGame>("Game")).ToList();
        }

        public TTTGame GetGameByUser(IUser user)
        {
            List<IUser> hosts = GetHosts();
            if (hosts.Contains(user))
            {
                int gameIndex = hosts.IndexOf(user);
                return GetGames()[gameIndex];
            }

            List<IUser> opponents = GetOpponents();
            if (opponents.Contains(user))
            {
                int gameIndex = opponents.IndexOf(user);
                return GetGames()[gameIndex];
            }

            return null;
        }
    }
}