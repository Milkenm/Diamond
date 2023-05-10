using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

using Diamond.API.Bot;

using Microsoft.Extensions.Logging;

using Victoria.Node;
using Victoria.Node.EventArgs;
using Victoria.Player;

using SUtils = ScriptsLibV2.Util.Utils;

namespace Diamond.API;
public class Lava : IDisposable
{
	private readonly DiamondBot _bot;

	private readonly ILogger<LavaNode> _logger;

	private LavaNode _node;
	private readonly Process _lavalinkProcess;

	public Lava(DiamondBot bot)
	{
		_bot = bot;
		_logger = new Loggerr();

		_lavalinkProcess = Process.Start(new ProcessStartInfo()
		{
			FileName = $"java",
			Arguments = $"-jar \"{Path.Join(SUtils.GetInstallationFolder(), @"\Lavalink\Lavalink.jar")}\"",
			UseShellExecute = false,
			RedirectStandardOutput = true,
			RedirectStandardError = true,
			CreateNoWindow = true,
		});
		_lavalinkProcess.BeginOutputReadLine();
		_lavalinkProcess.BeginErrorReadLine();
	}

	public void Dispose()
	{
		// Stop LavaNode
		this.StopNodeAsync().Wait();
		// Stop Lavalink process
		this._lavalinkProcess.CloseMainWindow();
		this._lavalinkProcess.Close();
		this._lavalinkProcess.Dispose();
	}

	public Process GetLavalinkProcess()
	{
		return _lavalinkProcess;
	}

	public async Task<LavaNode> GetNodeAsync()
	{
		if (this._node == null)
		{
			_node = new LavaNode(_bot.Client, new NodeConfiguration(), _logger);

			_node.OnTrackEnd += OnTrackEndAsync;
			_node.OnTrackStart += OnTrackStartAsync;
			_node.OnStatsReceived += OnStatsReceivedAsync;
			_node.OnUpdateReceived += OnUpdateReceivedAsync;
			_node.OnWebSocketClosed += OnWebSocketClosedAsync;
			_node.OnTrackStuck += OnTrackStuckAsync;
			_node.OnTrackException += OnTrackExceptionAsync;
		}

		if (!this._node.IsConnected)
		{
			Debug.WriteLine("Connecting to node...");
			try
			{
				await this._node.ConnectAsync();
				if (this._node.IsConnected)
				{
					Debug.WriteLine("Connected to node!");
				}
			}
			catch
			{
				Debug.WriteLine("An error ocurred while connecting to the node.");
			}
		}

		return this._node;
	}

	public async Task StopNodeAsync()
	{
		if (this._node == null) return;

		if (this._node.IsConnected)
		{
			await this._node.DisconnectAsync();
		}
		await this._node.DisposeAsync();
		this._node = null;
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
