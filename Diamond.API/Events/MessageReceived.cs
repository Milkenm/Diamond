using System.Threading.Tasks;

using Diamond.API.Util;

using Discord.WebSocket;

namespace Diamond.API.Events
{
	public partial class ClientEvents
	{
		public async Task MessageReceived(SocketMessage msg)
		{
			if (msg.Author.IsBot) return;

			if (msg.Content.Contains("(╯°□°）╯︵ ┻━┻") && Utils.ChanceOf(0.01))
			{
				_ = await msg.Channel.SendMessageAsync("┬─┬ ノ( ゜-゜ノ)").ConfigureAwait(false);
			}
		}
	}
}