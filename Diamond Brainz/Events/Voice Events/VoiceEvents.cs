using Diamond.Brainz.Commands;

using Discord.WebSocket;

using System.Threading.Tasks;

namespace Diamond.Brainz.Events
{
	public partial class VoiceEvents
	{
		public async Task UserVoiceEvent(SocketUser user, SocketVoiceState previousChannel, SocketVoiceState newChannel)
		{
			VoiceSeatsModule.VoiceHandlerAsync(user, previousChannel, newChannel);
		}
	}
}