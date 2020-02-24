#region Usings

using Discord.Commands;

using Newtonsoft.Json;

using System;
using System.Diagnostics;
using System.Threading.Tasks;

using static DiamondGui.Functions;
using static ScriptsLib.Network.Requests;

#endregion Usings

namespace DiamondGui
{
	public partial class CommandsModule : ModuleBase<SocketCommandContext>
	{
		[Command("meme"), Summary("Gives you a meme.")]
		public async Task CMD_Meme()
		{
			try
			{
				Stopwatch s = new Stopwatch();
				s.Start();

				string url = "https://www.reddit.com/r/memes/new.json?limit=100";

				MemeSchema meme = JsonConvert.DeserializeObject<MemeSchema>(GET(url));

				s.Stop();
				await ReplyAsync(s.ElapsedMilliseconds + "ms");
				await ReplyAsync(meme.data.children[0].data.author_fullname.ToString());
			}
			catch (Exception _Exception) { ShowException(_Exception); }
		}

		public class MemeSchema
		{
			public string kind;
			public DataSchema data;
		}

		public class DataSchema
		{
			public string modhash;
			public int dist;
			public ChildrenSchema[] children;
			public string after;
			public string before;
		}

		public class ChildrenSchema
		{
			public string kind;
			public ChildrenDataSchema data;
		}

		public class ChildrenDataSchema
		{
			public string approved_at_utc;
			public string subreddit;
			public string selftext;
			public string author_fullname;
			public bool saved;
			public string mod_reason_title;
			public int gilded;
			public bool clicked;
			public string title;
			public string[] link_flair_richtext;
		}
	}
}