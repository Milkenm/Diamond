using System.Net;
using System.Threading.Tasks;

using Diamond.API.Attributes;

using ScriptsLibV2.Extensions;

namespace Diamond.API.SlashCommands.Server
{
	public partial class Server
	{
		[DSlashCommand("info", "Show info about the server.")]
		public async Task ServerInfoCommandAsync(
				[ShowEveryone] bool showEveryone = false
		)
		{
			await DeferAsync(!showEveryone);

			string guildIconUrl = Context.Guild.IconUrl.Contains("a_") ? Context.Guild.IconUrl.Replace(".jpg", ".gif") : Context.Guild.IconUrl;

			DefaultEmbed embed = new DefaultEmbed("Server Info", "🏡", Context.Interaction);
			embed.AddField("👤 Owner", Context.Guild.Owner.Mention, true);
			embed.AddField("📆 Creation date", Context.Guild.CreatedAt.ToString("dd/MM/yyyy, HH:mm:ss"), true);
			embed.AddField("🔗 Vanity URL", Context.Guild.VanityURLCode.IsEmpty() ? "None" : Context.Guild.VanityURLCode, true);
			embed.AddField("👥 Members", Context.Guild.MemberCount, true);
			embed.AddField("🏷️ Roles", Context.Guild.Roles.Count, true);
			embed.WithThumbnailUrl($"{guildIconUrl}?size=512");
			embed.WithImageUrl(Context.Guild.DiscoverySplashUrl);

			await embed.SendAsync();
		}

		private bool RemoteFileExists(string url)
		{
			try
			{
				//Creating the HttpWebRequest
				HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
				//Setting the Request method HEAD, you can also use GET too.
				request.Method = "HEAD";
				//Getting the Web Response.
				HttpWebResponse response = request.GetResponse() as HttpWebResponse;
				//Returns TRUE if the Status code == 200
				response.Close();
				return (response.StatusCode == HttpStatusCode.OK);
			}
			catch
			{
				//Any exception will returns false.
				return false;
			}
		}
	}
}