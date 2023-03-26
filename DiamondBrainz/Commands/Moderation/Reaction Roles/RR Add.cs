using System.Threading.Tasks;

using Discord;
using Discord.Commands;

namespace Diamond.API.Commands
{
	public partial class ModerationModule
	{
		public partial class ReactionRoles : ModuleBase<SocketCommandContext>
		{
			[Name("Reaction Roles"), Command("reactionroles addrole"), Alias("rr ar", "rr addrole", "rr addr", "rr add"), Summary("Adds a Role to a Reaction Roles message.")]
			public async Task ReactionRolesAddRole(IRole role, string emote, params string[] description)
			{
				//GlobalData.RRMessagesDataTable.GetEditingMessageByChannelId(Context.Channel.Id).AddRole(role.Id, emote, description);
			}
		}
	}
}
