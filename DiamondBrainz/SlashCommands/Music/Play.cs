using System;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.Util;

using Discord;
using Discord.Interactions;

using Victoria.Node;
using Victoria.Player;
using Victoria.Responses.Search;

namespace Diamond.API.SlashCommands.Music;
public partial class Music
{
	[DSlashCommand("play", "Add an audio to the queue.")]
	public async Task PlayCommandAsync(
		[Summary("url", "The URL to play sound from.")] string url,
		[Summary("voice-channel", "The voice channel to play at.")] IVoiceChannel voiceChannel = null
	)
	{
		await this.DeferAsync();
		DefaultEmbed embed = new DefaultEmbed("Music", "🎶", this.Context);
		LavaNode node = await _lava.GetNodeAsync();

		// Get voice channel
		if (voiceChannel == null)
		{
			IVoiceState voiceState = this.Context.User as IVoiceState;

			// No voice channel
			if (voiceState.VoiceChannel == null)
			{
				embed.Title = "No voice channel.";
				embed.Description = "You need to provide or be on a voice channel to play at.";
				await embed.SendAsync();
				return;
			}

			voiceChannel = voiceState.VoiceChannel;
		}

		// No search
		if (string.IsNullOrWhiteSpace(url))
		{
			embed.Title = "No search term.";
			embed.Description = "Type a URL or the name of a music to search for.";
			await embed.SendAsync();
			return;
		}

		// Join voice channel if not already in one
		if (!node.TryGetPlayer(this.Context.Guild, out LavaPlayer<LavaTrack> player))
		{
			player = await node.JoinAsync(voiceChannel, this.Context.Channel as ITextChannel);
		}

		// Search for a song
		SearchResponse searchResponse = await node.SearchAsync(Uri.IsWellFormedUriString(url, UriKind.Absolute) ? SearchType.Direct : SearchType.YouTube, url);
		// No results
		if (searchResponse.Status is SearchStatus.LoadFailed or SearchStatus.NoMatches)
		{
			embed.Title = "Nothing found.";
			embed.Description = $"'**{url}**' wield no results.";
			await embed.SendAsync();
			return;
		}
		// Add a playlist to the queue
		if (!string.IsNullOrWhiteSpace(searchResponse.Playlist.Name))
		{
			player.Vueue.Enqueue(searchResponse.Tracks);

			embed.Title = "Added to queue";
			embed.Description = $"Added **{searchResponse.Tracks.Count}** to the queue.";
			await embed.SendAsync();
		}
		// Add a track to the queue
		else
		{
			LavaTrack track = searchResponse.Tracks.FirstOrDefault();
			player.Vueue.Enqueue(track);

			embed.Title = "Added to queue";
			embed.Description = $"Added **{track.Title}** to the queue.";
			await embed.SendAsync();
		}
		// Start playing
		if (player.PlayerState is PlayerState.Playing or PlayerState.Paused)
		{
			return;
		}

		player.Vueue.TryDequeue(out LavaTrack lavaTrack);
		await player.PlayAsync(lavaTrack);

		embed.Title = "Playing";
		embed.Description = $"Playing '**{lavaTrack.Title}**'";
		await embed.SendAsync();
	}
}
