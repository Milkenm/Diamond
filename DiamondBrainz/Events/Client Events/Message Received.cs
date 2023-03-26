using System.Threading.Tasks;

namespace Diamond.API.Events
{
    public partial class ClientEvents
    {
        public async Task MessageReceived(Discord.WebSocket.SocketMessage msg)
        {
            if (!msg.Author.IsBot)
            {
                if (msg.Content == "(╯°□°）╯︵ ┻━┻")
                {
                    await msg.Channel.SendMessageAsync("┬─┬ ノ( ゜-゜ノ)").ConfigureAwait(false);
                }

                // GAME EVENTS
                await GameEvents.TicTacToe(msg).ConfigureAwait(false);
            }
        }
    }
}