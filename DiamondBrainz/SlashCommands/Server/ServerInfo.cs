using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.Util;

using Discord.WebSocket;

using Org.BouncyCastle.Asn1.Ntt;

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
			await this.DeferAsync(!showEveryone);

			string guildIconUrl = this.Context.Guild.IconUrl.Contains("a_") ? this.Context.Guild.IconUrl.Replace(".jpg", ".gif") : this.Context.Guild.IconUrl;
			await this.Context.Guild.DownloadUsersAsync();

			DefaultEmbed embed = new DefaultEmbed("Guild Info", "🏡", this.Context)
			{
				ThumbnailUrl = $"{guildIconUrl}?size=512",
				ImageUrl = this.Context.Guild.DiscoverySplashUrl,
			};
			// First row
			_ = embed.AddField("👤 Owner", this.Context.Guild.Owner.Mention, true);
			_ = embed.AddField("📆 Creation date", this.Context.Guild.CreatedAt.ToString("dd/MM/yyyy, HH:mm:ss"), true);
			_ = embed.AddField("🔗 Vanity URL", this.Context.Guild.VanityURLCode.IsEmpty() ? "None" : this.Context.Guild.VanityURLCode, true);
			// Second row
			_ = embed.AddField("👥 Members", this.Context.Guild.Users.Where(u => !u.IsBot).Count(), true);
			_ = embed.AddField("🤖 Bots", this.Context.Guild.Users.Where(u => u.IsBot).Count(), true);
			_ = embed.AddField("🏷 Roles", this.Context.Guild.Roles.Count, true);
			// Third row
			_ = embed.AddField("✏️ Text channels", this.Context.Guild.Channels.Where(c => c is SocketTextChannel).Count(), true);
			_ = embed.AddField("🔈 Voice channels", this.Context.Guild.Channels.Where(c => c is SocketVoiceChannel).Count(), true);
			_ = embed.AddField("🗨 Other channels", this.Context.Guild.Channels.Where(c => c is not SocketTextChannel and not SocketVoiceChannel).Count(), true);
			Debug.WriteLine(this.Context.Guild.Channels.Where(c => c is not SocketTextChannel and not SocketVoiceChannel));

			_ = await embed.SendAsync();
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
				return response.StatusCode == HttpStatusCode.OK;
			}
			catch
			{
				//Any exception will returns false.
				return false;
			}
		}
	}
}