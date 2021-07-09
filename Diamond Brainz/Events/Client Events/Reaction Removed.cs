
using Discord;
using Discord.WebSocket;

using System.Threading.Tasks;

namespace Diamond.Brainz.Events
{
	public partial class ClientEvents
	{
		public async Task ReactionRemoved(Cacheable<IUserMessage, ulong> msg, Cacheable<IMessageChannel, ulong> channel, SocketReaction reaction)
		{
			//if (!reaction.User.Value.IsBot)
			//{
			//    GlobalData.RRMessagesDataTable.HandleReactionRemoved(reaction);
			//}
		}
	}
}