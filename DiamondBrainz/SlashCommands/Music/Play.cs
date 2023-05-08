using System;
using System.Linq;
using System.Threading.Tasks;

using Discord;
using Discord.Interactions;

using Victoria.Node;
using Victoria.Player;
using Victoria.Responses.Search;

namespace Diamond.API.SlashCommands.Music;
public partial class Music
{
	[SlashCommand("play", "Add an audio to the queue.")]
	public async Task PlayCommandAsync(
		[Summary("url", "The URL to play sound from.")] string url,
		[Summary("voice-channel", "The voice channel to join.")] IVoiceChannel voiceChannel
	)
	{
		await DeferAsync();

		LavaNode node = _lava.GetNode();

		if (string.IsNullOrWhiteSpace(url))
		{
			await ReplyAsync("Please provide search terms.");
			return;
		}

		if (!node.TryGetPlayer(Context.Guild, out var player))
		{
			if (voiceChannel == null)
			{
				await ReplyAsync("You must be connected to a voice channel!");
				return;
			}

			try
			{
				player = await node.JoinAsync(voiceChannel, Context.Channel as ITextChannel);
				await ReplyAsync($"Joined {voiceChannel.Name}!");
			}
			catch (Exception exception)
			{
				await ReplyAsync(exception.Message);
			}
		}

		var searchResponse = await node.SearchAsync(Uri.IsWellFormedUriString(url, UriKind.Absolute) ? SearchType.Direct : SearchType.YouTube, url);
		if (searchResponse.Status is SearchStatus.LoadFailed or SearchStatus.NoMatches)
		{
			await ReplyAsync($"I wasn't able to find anything for `{url}`.");
			return;
		}

		if (!string.IsNullOrWhiteSpace(searchResponse.Playlist.Name))
		{
			player.Vueue.Enqueue(searchResponse.Tracks);
			await ReplyAsync($"Enqueued {searchResponse.Tracks.Count} songs.");
		}
		else
		{
			var track = searchResponse.Tracks.FirstOrDefault();
			player.Vueue.Enqueue(track);

			await ReplyAsync($"Enqueued {track?.Title}");
		}

		if (player.PlayerState is PlayerState.Playing or PlayerState.Paused)
		{
			return;
		}

		player.Vueue.TryDequeue(out var lavaTrack);
		await player.PlayAsync(lavaTrack);

		/*DefaultEmbed embed = new DefaultEmbed("Music Play", "🎵", Context.Interaction)
		{
			Description = "Command is **WIP** *(Work In Progress)*."
		};
		await embed.SendAsync();*/
	}
}
