#region Usings
using System.Threading.Tasks;

using Discord.WebSocket;
#endregion Usings



namespace DiamondGui
{
	internal static partial class EventsModule
	{
		internal static Task MessageReceived(SocketMessage _Message)
		{
			if (_Message.Author.IsBot == false)
			{
				if (_Message.Content == "(╯°□°）╯︵ ┻━┻")
				{
					_Message.Channel.SendMessageAsync("┬─┬ ノ( ゜-゜ノ)");
				}
			}

			return Task.CompletedTask;
		}
	}
}
