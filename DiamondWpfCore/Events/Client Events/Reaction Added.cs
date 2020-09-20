﻿using Discord;
using Discord.WebSocket;

using System.Threading.Tasks;

namespace Diamond.WPFCore.Events
{
    public partial class ClientEvents
    {
        public async Task ReactionAdded(Cacheable<IUserMessage, ulong> msg, ISocketMessageChannel channel, SocketReaction reaction)
        {
            if (!reaction.User.Value.IsBot)
            {
                await GameEvents.TicTacToe(reaction).ConfigureAwait(false);
            }
        }
    }
}