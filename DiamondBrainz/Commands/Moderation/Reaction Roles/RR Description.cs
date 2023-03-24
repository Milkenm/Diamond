using System.Threading.Tasks;

using Discord.Commands;

namespace Diamond.Brainz.Commands
{
	public partial class ModerationModule
	{
		public partial class ReactionRoles : ModuleBase<SocketCommandContext>
		{
			[Name("Reaction Roles"), Command("reactionroles description"), Alias("rr description", "rr desc", "rr d"), Summary("Edits the Description of a Reaction Roles message.")]
			public async Task ReactionRolesDescription(params string[] description)
			{
				//string d = string.Join(' ', description);

				//if (!string.IsNullOrWhiteSpace(d) || !string.IsNullOrEmpty(d))
				//{
				//	GlobalData.RRMessagesDataTable.GetEditingMessageByChannelId(Context.Channel.Id).SetDescription(d);
				//}
				//else
				//{
				//	throw new Exception("Invalid description.");
				//}
			}
		}
	}
}
