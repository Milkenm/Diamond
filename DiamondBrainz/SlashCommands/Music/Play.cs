using System.Threading;
using System.Threading.Tasks;

using Discord;
using Discord.Interactions;

namespace Diamond.API.SlashCommands.Music;
public partial class Music
{
	[SlashCommand("play", "Add an audio to the queue.")]
	public async Task PlayCommandAsync(
		/*[Summary("url", "The URL to play sound from.")] string url*/
		[Summary("voice-channel", "The voice channel to join.")] IVoiceChannel voiceChannel
	)
	{
		await DeferAsync();

		await _lavanode.GetNode().ConnectAsync();
		Thread.Sleep(5000);
		await _lavanode.GetNode().JoinAsync(voiceChannel);

		DefaultEmbed embed = new DefaultEmbed("Music Play", "🎵", Context.Interaction)
		{
			Description = "Command is **WIP** *(Work In Progress)*."
		};
		await embed.SendAsync();
	}
}
