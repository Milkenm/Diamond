using System.Net;
using System.Threading.Tasks;

using Discord.Interactions;

using Newtonsoft.Json;

using ScriptsLibV2.Util;

namespace Diamond.API.SlashCommands.NSFW;

public class Hentai : InteractionModuleBase<SocketInteractionContext>
{
	private readonly AppSettings _appSettings;

	public Hentai(AppSettings appSettings)
	{
		_appSettings = appSettings;
	}

	[RequireNsfw]
	[SlashCommand("hentai", "Gives you a sus image in 2D.")]
	public async Task HentaiCommand(
		[Summary("type", "The sus type.")] HentaiType type = HentaiType.Classic,
		[Summary("show-everyone", "Show the command output to everyone.")] bool showEveryone = false
	)
	{
		await DeferAsync(!showEveryone);

		// Type
		string hentaiType = type.ToString().ToLower();
		if (type == HentaiType.Classic)
		{
			hentaiType = "hentai";
		}
		else if (type != HentaiType.Tentacle)
		{
			hentaiType = "h" + hentaiType;
		}

		// Request
		WebHeaderCollection headers = new WebHeaderCollection
		{
			{ "authorization", _appSettings.Settings.NightapiApiKey }
		};
		string response = RequestUtils.Get($"https://api.night-api.com/images/nsfw/{hentaiType}", headers);
		NightApiResponse resp = JsonConvert.DeserializeObject<NightApiResponse>(response);

		// Create the embed
		DefaultEmbed embed = new DefaultEmbed("Hentai", "💮", Context);
		embed.WithImageUrl(resp.Content.Url);

		// Reply
		await embed.SendAsync();
	}

	public enum HentaiType
	{
		Anal,
		Ass,
		Boobs,
		Classic,
		Kitsune,
		Midriff,
		Neko,
		Thigh,
		Tentacle
	}

	public class NightApiResponse
	{
		[JsonProperty("status")] public string Status { get; set; }
		[JsonProperty("content")] public NightApiResponseContent Content { get; set; }
	}

	public class NightApiResponseContent
	{
		[JsonProperty(propertyName: "id")] public int Id { get; set; }
		[JsonProperty("type")] public string Type { get; set; }
		[JsonProperty("url")] public string Url { get; set; }
	}
}
