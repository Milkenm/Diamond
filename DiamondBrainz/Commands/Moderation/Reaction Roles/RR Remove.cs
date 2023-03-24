using System.Threading.Tasks;

using Discord;
using Discord.Commands;

namespace Diamond.Brainz.Commands
{
	public partial class ModerationModule
	{
		public partial class ReactionRoles : ModuleBase<SocketCommandContext>
		{
			[Name("Reaction Roles"), Command("reactionroles removerole"), Alias("rr rem", "rr remove", "rr delrole", "rr remrole"), Summary("Removes a Role from a Reaction Roles message.")]
			public async Task ReactionRolesRemoveRole(IRole role)
			{
				//GlobalData.RRMessagesDataTable.GetEditingMessageByChannelId(Context.Channel.Id).RemoveRole(role.Id);
			}

			[Name("Reaction Roles"), Command("reactionroles removerole"), Alias("rr rem", "rr remove", "rr delrole", "rr remrole"), Summary("Removes a Role from a Reaction Roles message.")]
			public async Task ReactionRolesRemoveRole(string emote)
			{
				//GlobalData.RRMessagesDataTable.GetEditingMessageByChannelId(Context.Channel.Id).RemoveRole(emote);
			}
		}
	}
}