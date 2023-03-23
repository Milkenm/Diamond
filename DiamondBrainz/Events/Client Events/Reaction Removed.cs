
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;

namespace Diamond.Brainz.Events
{
	public partial class ClientEvents
	{
		//public async Task ReactionRemoved(Cacheable<IUserMessage, ulong> msg, Cacheable<IMessageChannel, ulong> channel, SocketReaction reaction)
		public async Task ReactionRemoved(Cacheable<IUserMessage, ulong> msg, Cacheable<IMessageChannel, ulong> channel, SocketReaction reaction)
		{
			//if (!reaction.User.Value.IsBot)
			//{
			//    GlobalData.RRMessagesDataTable.HandleReactionRemoved(reaction);
			//}
		}
	}
}