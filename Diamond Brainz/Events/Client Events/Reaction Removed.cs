using Diamond.Brainz.Data;

using Discord;
using Discord.WebSocket;

using System.Threading.Tasks;

namespace Diamond.Brainz.Events
{
    public partial class ClientEvents
    {
        public async Task ReactionRemoved(Cacheable<IUserMessage, ulong> msg, ISocketMessageChannel channel, SocketReaction reaction)
        {
            if (!reaction.User.Value.IsBot)
            {
                GlobalData.RRMessagesDataTable.ReactionRemovedEvent(reaction);
            }
        }
    }
}