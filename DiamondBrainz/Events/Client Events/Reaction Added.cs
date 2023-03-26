using Discord;
using Discord.WebSocket;

using System.Threading.Tasks;

namespace Diamond.API.Events
{
	public partial class ClientEvents
	{
		//public async Task ReactionAdded(Cacheable<IUserMessage, ulong> msg, Cacheable<IMessageChannel, ulong> channel, SocketReaction reaction)
		public async Task ReactionAdded(Cacheable<IUserMessage, ulong> msg, Cacheable<IMessageChannel,ulong> channel, SocketReaction reaction)
		{
			//if (!reaction.User.Value.IsBot)
			//{
			//    await GameEvents.TicTacToe(reaction).ConfigureAwait(false);

			//   GlobalData.RRMessagesDataTable.HandleReactionAdded(reaction);
			//}
		}
	}
}