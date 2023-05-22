using System.Threading.Tasks;

using Diamond.API.Attributes;

using Discord.Interactions;

using Victoria.Node;
using Victoria.Player;

namespace Diamond.API.SlashCommands.Music;
public partial class Music
{
	[DSlashCommand("skip", "Skips the current playing song.")]
	public async Task SkipCommandAsync()
	{
		await this.DeferAsync();
		DefaultEmbed embed = new DefaultEmbed("Music", "🎶", this.Context.Interaction);
		LavaNode node = await _lava.GetNodeAsync();

		if (!node.TryGetPlayer(this.Context.Guild, out LavaPlayer<LavaTrack> player))
		{
			embed.Title = "Not playing";
			embed.Description = "There is nothing currently playing. In fact, I'm not even in a voice channel.";
			await embed.SendAsync();
			return;
		}

		if (player.PlayerState != PlayerState.Playing)
		{
			embed.Title = "Not playing";
			embed.Description = "There is nothing currently playing.";
			await this.ReplyAsync("Woaaah there, I can't skip when nothing is playing.");
			return;
		}

		// There is only 1 track (A.K.A. Count = 0, because the currently playing track is not in the queue, so we stop the player instead of skipping)
		if (player.Vueue.Count == 0)
		{
			string trackTitle = player.Track.Title;
			await player.StopAsync();

			embed.Title = "Stopped";
			embed.Description = $"Skipped '**{trackTitle}**'.\n Since this is the last track, the player stopped.";
			await embed.SendAsync();
			return;
		}

		// Skip and play next track
		(LavaTrack skipped, LavaTrack next) = await player.SkipAsync();

		embed.Title = "Skipped";
		embed.Description = $"Skipped '**{skipped.Title}**'.";
		await embed.SendAsync();

		// Play next track message
		DefaultEmbed playingEmbed = new DefaultEmbed("Music", "🎶", this.Context.Interaction)
		{
			Title = "Now playing",
			Description = $"Now playing: '**{next.Title}**'.",
		};
		await playingEmbed.SendAsync(false, true);
	}
}
