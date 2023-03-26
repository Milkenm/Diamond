using System.Threading.Tasks;

using Discord.WebSocket;

namespace Diamond.API.Events
{
	public static partial class GameEvents
	{
		// 1, 2, 3, 4, 5, 6, 7, 8, 9
		public static async Task TicTacToe(SocketMessage msg)
		{
			//if (GlobalData.TTTGamesDataTable.ContainsChannelId(msg.Channel.Id))
			//{
			//    TTTGame game = GlobalData.TTTGamesDataTable.GetGameByUser(msg.Author);

			//    await game?.HandlePlay(msg);
			//}
		}

		// LEAVE/CALL EMOTES
		public static async Task TicTacToe(SocketReaction reaction)
		{
			//if (GlobalData.TTTGamesDataTable.ContainsMessageId(reaction.MessageId))
			//{
			//    TTTGame game = GlobalData.TTTGamesDataTable.GetGameByUser(reaction.User.Value);

			//    await game?.HandleReaction(reaction);
			//}
		}
	}
}