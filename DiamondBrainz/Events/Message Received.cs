using System.Threading.Tasks;

using Diamond.API.Util;

namespace Diamond.API.Events
{
	public partial class ClientEvents
	{
		public async Task MessageReceived(Discord.WebSocket.SocketMessage msg)
		{
			if (msg.Author.IsBot) return;

			if (msg.Content == "(╯°□°）╯︵ ┻━┻" && Utils.ChanceOf(0.01))
			{
				_ = await msg.Channel.SendMessageAsync("┬─┬ ノ( ゜-゜ノ)").ConfigureAwait(false);
			}
		}
	}
}