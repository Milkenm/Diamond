using System.Text.Json;
using System.Threading.Tasks;

using Diamond.API.Bot;

using Microsoft.Extensions.Logging;

using Victoria.Node;
using Victoria.Node.EventArgs;
using Victoria.Player;

namespace Diamond.API;
public class Lava
{
	private readonly DiamondBot _bot;

	private readonly LavaNode _lavanode;
	private readonly ILogger<LavaNode> _logger;

	public Lava(DiamondBot bot)
	{
		_bot = bot;

		NodeConfiguration config = new NodeConfiguration();

		_logger = new Loggerr();
		_lavanode = new LavaNode(_bot.Client, config, _logger);

		_lavanode.OnTrackEnd += OnTrackEndAsync;
		_lavanode.OnTrackStart += OnTrackStartAsync;
		_lavanode.OnStatsReceived += OnStatsReceivedAsync;
		_lavanode.OnUpdateReceived += OnUpdateReceivedAsync;
		_lavanode.OnWebSocketClosed += OnWebSocketClosedAsync;
		_lavanode.OnTrackStuck += OnTrackStuckAsync;
		_lavanode.OnTrackException += OnTrackExceptionAsync;
	}

	public LavaNode GetNode()
	{
		return this._lavanode;
	}

	private static Task OnTrackExceptionAsync(TrackExceptionEventArg<LavaPlayer<LavaTrack>, LavaTrack> arg)
	{
		arg.Player.Vueue.Enqueue(arg.Track);
		return arg.Player.TextChannel.SendMessageAsync($"{arg.Track} has been requeued because it threw an exception.");
	}

	private static Task OnTrackStuckAsync(TrackStuckEventArg<LavaPlayer<LavaTrack>, LavaTrack> arg)
	{
		arg.Player.Vueue.Enqueue(arg.Track);
		return arg.Player.TextChannel.SendMessageAsync($"{arg.Track} has been requeued because it got stuck.");
	}

	private Task OnWebSocketClosedAsync(WebSocketClosedEventArg arg)
	{
		_logger.LogCritical($"{arg.Code} {arg.Reason}");
		return Task.CompletedTask;
	}

	private Task OnStatsReceivedAsync(StatsEventArg arg)
	{
		_logger.LogInformation(JsonSerializer.Serialize(arg));
		return Task.CompletedTask;
	}

	private static Task OnUpdateReceivedAsync(UpdateEventArg<LavaPlayer<LavaTrack>, LavaTrack> arg)
	{
		return arg.Player.TextChannel.SendMessageAsync($"Player update received: {arg.Position}/{arg.Track?.Duration}");
	}

	private static Task OnTrackStartAsync(TrackStartEventArg<LavaPlayer<LavaTrack>, LavaTrack> arg)
	{
		return arg.Player.TextChannel.SendMessageAsync($"Started playing {arg.Track}.");
	}

	private static Task OnTrackEndAsync(TrackEndEventArg<LavaPlayer<LavaTrack>, LavaTrack> arg)
	{
		return arg.Player.TextChannel.SendMessageAsync($"Finished playing {arg.Track}.");
	}
}
