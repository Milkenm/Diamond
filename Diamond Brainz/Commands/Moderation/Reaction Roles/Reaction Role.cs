using Diamond.Brainz.Data;
using Diamond.Brainz.Structures.ReactionRoles;
using Diamond.Brainz.Utils;

using Discord;
using Discord.Commands;

using System.Threading.Tasks;

namespace Diamond.Brainz.Commands
{
	public partial class ModerationModule
	{
		public partial class ReactionRoles : ModuleBase<SocketCommandContext>
		{
			[Name("Reaction Roles"), Command("reactionroles"), Alias("rr", "reactionrole", "reactionroles edit", "rr edit"), Summary("Creates a message which users can react to and receive a role.")]
			public async Task ReactionRole(ulong? messageId = null)
			{
				RRMessage editingMessage = GlobalData.RRMessagesDataTable.GetEditingMessageByChannelId(Context.Channel.Id);

				if (messageId != null)
				{
					editingMessage?.StopEditing();

					editingMessage = GlobalData.RRMessagesDataTable.GetMessageById((ulong)messageId);
					editingMessage.StartEditing();
					await ReplyAsync("Started editing the message.").ConfigureAwait(false);
				}
				else
				{
					if (editingMessage == null)
					{
						// SEND THE MESSAGE TO DISCORD
						EmbedBuilder embed = new EmbedBuilder();
						IUserMessage reply = await ReplyAsync(embed: Embeds.FinishEmbed(embed, Context)).ConfigureAwait(false);

						// SAVE THE MESSAGE ON THE DATATABLE
						GlobalData.RRMessagesDataTable.NewMessage(reply.Id, Context.Channel.Id, Context.Guild.Id, Context.User.Id);
					}
					else
					{
						editingMessage.StopEditing();
						await ReplyAsync("Stopped editing the message.").ConfigureAwait(false);
					}
				}
			}
		}
	}
}
