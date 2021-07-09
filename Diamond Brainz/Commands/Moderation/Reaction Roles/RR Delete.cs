using Diamond.Brainz.Data;
using Diamond.Brainz.Structures.ReactionRoles;

using Discord.Commands;

using System;
using System.Threading.Tasks;

namespace Diamond.Brainz.Commands
{
	public partial class ModerationModule
	{
		public partial class ReactionRoles : ModuleBase<SocketCommandContext>
		{
			[Name("Reaction Roles"), Command("reactionroles delete"), Alias("rr delete", "rr del"), Summary("Deletes a Reaction Roles message.")]
			public async Task ReactionRolesDelete(ulong? messageId = null)
			{
				//if (messageId != null)
				//{
				//	GlobalData.RRMessagesDataTable.DeleteMessage((ulong)messageId);
				//}
				//else
				//{
				//	RRMessage rrMsg = GlobalData.RRMessagesDataTable.GetEditingMessageByChannelId(Context.Channel.Id);

				//	if (rrMsg != null)
				//	{
				//		GlobalData.RRMessagesDataTable.DeleteMessage(rrMsg.MessageId);
				//	}
				//	else
				//	{
				//		throw new Exception("No messages found.");
				//	}
				//}
			}
		}
	}
}
