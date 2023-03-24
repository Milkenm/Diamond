using System.Threading.Tasks;

using Discord.Commands;

namespace Diamond.Brainz.Commands
{
	public partial class OwnerModule : ModuleBase<SocketCommandContext>
	{
		[Name("Config"), Command("config"), Alias("cfg"), Summary("Modify or refresh bot configs."), RequireOwner]
		public async Task Config(string action, string a = "", string b = "")
		{
			if (action == "set")
			{
				await this.ReplyAsync("set");
			}
			else if (action == "refresh")
			{
				await this.ReplyAsync("refresh");
			}
		}

		public enum ConfigAction
		{
			Refresh,
			Set,
		}
	}
}
