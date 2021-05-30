using Diamond.Brainz.Data;

using Discord.Commands;

using System;
using System.Threading.Tasks;

namespace Diamond.Brainz.Commands
{
	public partial class ModerationModule
	{
		public partial class ReactionRoles : ModuleBase<SocketCommandContext>
		{
			[Name("Reaction Roles"), Command("reactionroles title"), Alias("rr title", "rr t", "reactionroles settitle", "rr settitle"), Summary("Edits the Title of a Reaction Roles message.")]
			public async Task ReactionRolesTitle(params string[] title)
			{
				string t = string.Join(' ', title);

				if (!string.IsNullOrWhiteSpace(t) || !string.IsNullOrEmpty(t))
				{
					GlobalData.RRMessagesDataTable.GetEditingMessageByChannelId(Context.Channel.Id).SetTitle(t);
				}
				else
				{
					throw new Exception("Invalid title.");
				}
			}
		}
	}
}
