﻿using Diamond.WPF.Data;
using Diamond.WPF.Structures.Games;

using Discord.WebSocket;

using System.Threading.Tasks;

namespace Diamond.WPF.Events
{
    public static partial class GameEvents
    {
        // 1, 2, 3, 4, 5, 6, 7, 8, 9
        public static async Task TicTacToe(SocketMessage msg)
        {
            if (GlobalData.TTTGamesDataTable.ContainsChannelId(msg.Channel.Id))
            {
                TTTGame game = GlobalData.TTTGamesDataTable.GetGameByUser(msg.Author);

                await game?.HandlePlay(msg);
            }
        }

        // EMOJI ADDED
        public static async Task TicTacToe(SocketReaction reaction)
        {
            if (GlobalData.TTTGamesDataTable.ContainsChannelId(reaction.Channel.Id))
            {
                TTTGame game = GlobalData.TTTGamesDataTable.GetGameByUser(reaction.User.Value);

                await game?.HandlePlay(reaction);
            }
        }
    }
}
