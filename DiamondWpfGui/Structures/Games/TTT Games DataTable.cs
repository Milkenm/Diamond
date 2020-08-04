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
            GamesTable.Columns.Add("Host", typeof(IUser));
            GamesTable.Columns.Add("Opponent", typeof(IUser));
            GamesTable.Columns.Add("MessageId", typeof(ulong));
            GamesTable.Columns.Add("ChannelId", typeof(ulong));
            GamesTable.Columns.Add("Game", typeof(TTTGame));
        }

        private static readonly DataTable GamesTable = new DataTable();
        private static readonly EnumerableRowCollection<DataRow> GamesTableEnumerator = GamesTable.AsEnumerable();

        public void AddGame(IUser host, IUser opponent, ulong msgId, ulong channelId, TTTGame game)
        {
            GamesTable.Rows.Add(host, opponent, msgId, channelId, game);
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

            return null;
        }
    }
}
