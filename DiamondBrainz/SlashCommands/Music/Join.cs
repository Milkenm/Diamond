using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.Util;

using Discord;
using Discord.Interactions;

using Victoria.Node;

namespace Diamond.API.SlashCommands.Music
{
	public partial class Music
	{
		[DSlashCommand("join", "Join your voice channel or the voice channel you provide.")]
		public async Task JoinCommandAsync(
			[Summary("voice-channel", "The voice channel to join.")] IVoiceChannel voiceChannel
		)
		{
			await this.DeferAsync();
			DefaultEmbed embed = new DefaultEmbed("Music", "🎶", this.Context);
			LavaNode node = await _lava.GetNodeAsync();

			// Get voice channel
			if (voiceChannel == null)
			{
				IVoiceState voiceState = this.Context.User as IVoiceState;

				if (voiceState.VoiceChannel == null)
				{
					embed.Title = "No voice channel.";
					embed.Description = "You need to provide or be on a voice channel to play at.";
					await embed.SendAsync();
					return;
				}

				voiceChannel = voiceState.VoiceChannel;
			}

			await node.JoinAsync(voiceChannel, this.Context.Channel as ITextChannel);

			embed.Title = "Joined voice channel";
			embed.Description = $"Joined voice channel '**{voiceChannel.Name}**'";
			await embed.SendAsync();
		}
	}
}