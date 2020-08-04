using Discord;

using System.Threading.Tasks;

namespace Diamond.WPF.Events
{
    public partial class ClientEvents
    {
        public async Task ReactionAdded(Cacheable<IUserMessage, ulong> msg, Discord.WebSocket.ISocketMessageChannel channel, Discord.WebSocket.SocketReaction reaction)
        {
            if (!reaction.User.Value.IsBot)
            {
                // GAME EVENTS
                await GameEvents.TicTacToe(reaction).ConfigureAwait(false);
            }
        }
    }
}
