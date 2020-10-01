using Diamond.Brainz.Data;
using Diamond.Brainz.Structures.ReactionRoles;
using Diamond.Brainz.Utils;

using Discord;
using Discord.Commands;

using System;
using System.Threading.Tasks;

namespace Diamond.Brainz.Commands
{
	public partial class UnsortedModule : ModuleBase<SocketCommandContext>
	{
		// RR
		[Name("Reaction Roles"), Command("reactionroles"), Alias("rr", "reactionrole"), Summary("Creates a message which users can react to and receive a role.")]
		public async Task ReactionRoles(ulong? messageId = null)
		{
			if (messageId == null && !GlobalData.RRMessagesDataTable.AreMessagesBeingEditedOnChannel(Context.Channel.Id)) // CREATE
			{
				// SEND THE MESSAGE TO DISCORD
				EmbedBuilder embed = new EmbedBuilder();
				IUserMessage reply = await ReplyAsync(embed: Embeds.FinishEmbed(embed, Context)).ConfigureAwait(false);

				// SAVE THE MESSAGE ON THE DATATABLE
				GlobalData.RRMessagesDataTable.AddMessage(Context, reply, embed);
			}
			else if (messageId == null && GlobalData.RRMessagesDataTable.AreMessagesBeingEditedOnChannel(Context.Channel.Id)) // STOP EDITING
			{
				GlobalData.RRMessagesDataTable.StopAllChannelEdits(Context.Channel.Id);
			}
			else // EDIT
			{
				RRMessage rrMsg;

				if (messageId != null)
				{
					rrMsg = GlobalData.RRMessagesDataTable.GetRRMessageByMessageId((ulong)messageId);
				}
				else
				{
					rrMsg = GlobalData.RRMessagesDataTable.GetLatestChannelRRMessage(Context.Channel.Id);
				}

				if (rrMsg != null)
				{
					GlobalData.RRMessagesDataTable.StopAllChannelEdits(Context.Channel.Id);
					rrMsg.StartEditing();
				}
			}
		}

		// RR TITLE
		[Name("Reaction Roles"), Command("reactionroles title"), Alias("rr title", "rr t", "reactionroles settitle", "rr settitle"), Summary("Edits the Title of a Reaction Roles message.")]
		public async Task ReactionRolesTitle(params string[] title)
		{
			RRMessage rrMsg = GlobalData.RRMessagesDataTable.GetRREditingMessage(Context.Channel.Id);

			rrMsg.SetTitle(title);
			rrMsg.ModifyDiscordEmbedAsync();
		}

		// RR DESC
		[Name("Reaction Roles"), Command("reactionroles description"), Alias("rr description", "rr desc", "rr d"), Summary("Edits the Description of a Reaction Roles message.")]
		public async Task ReactionRolesDescription(params string[] description)
		{
			RRMessage rrMsg = GlobalData.RRMessagesDataTable.GetRREditingMessage(Context.Channel.Id);

			rrMsg.SetDescription(description);
			rrMsg.ModifyDiscordEmbedAsync();
		}

		// RR ADD
		[Name("Reaction Roles"), Command("reactionroles addrole"), Alias("rr ar", "rr addrole", "rr addr", "rr add"), Summary("Adds a Role to a Reaction Roles message.")]
		public async Task ReactionRolesAddRole(IRole role, string emote, params string[] description)
		{
			RRMessage rrMsg = GlobalData.RRMessagesDataTable.GetRREditingMessage(Context.Channel.Id);

			rrMsg.AddRoleLine(role, emote, description);
			rrMsg.ModifyDiscordEmbedAsync();
		}

		// RR REM
		[Name("Reaction Roles"), Command("reactionroles removerole"), Alias("rr rem", "rr del", "rr delete", "rr remove", "rr delrole", "rr remrole"), Summary("Removes a Role from a Reaction Roles message.")]
		public async Task ReactionRolesRemoveRole(IRole role)
		{
			RRMessage rrMsg = GlobalData.RRMessagesDataTable.GetRREditingMessage(Context.Channel.Id);

			rrMsg.RemoveRoleLine(role);
			rrMsg.ModifyDiscordEmbedAsync();
		}
		[Name("Reaction Roles"), Command("reactionroles removerole"), Alias("rr rem", "rr del", "rr delete", "rr remove", "rr delrole", "rr remrole"), Summary("Removes a Role from a Reaction Roles message.")]
		public async Task ReactionRolesRemoveRole(string emote)
		{
			RRMessage rrMsg = GlobalData.RRMessagesDataTable.GetRREditingMessage(Context.Channel.Id);

			rrMsg.RemoveRoleLine(emote);
			rrMsg.ModifyDiscordEmbedAsync();
		}

		// RR DELETE
		[Name("Reaction Roles"), Command("reactionroles delete"), Alias("rr delete", "rr del"), Summary("Deletes a Reaction Roles message.")]
		public async Task ReactionRolesDelete(ulong? messageId = null)
		{
			RRMessage rrMsg;

			if (messageId == null)
			{
				rrMsg = GlobalData.RRMessagesDataTable.GetRREditingMessage(Context.Channel.Id);
			}
			else
			{
				rrMsg = GlobalData.RRMessagesDataTable.GetRRMessageByMessageId((ulong)messageId);
			}

			if (rrMsg != null)
			{
				rrMsg.DeleteMessage();
			}
			else
			{
				throw new Exception("No Reaction Roles messages found.");
			}
		}

		// RR HELP
		[Name("Reaction Roles"), Command("reactionroles help"), Alias("rr help"), Summary("Send a message with help for the Reaction Roles setup command.")]
		public async Task ReactionRolesHelp()
		{
			// SEND HELP EMBED
			EmbedBuilder helpEmbed = new EmbedBuilder();
			helpEmbed.WithTitle("New Reaction Roles edit session started");
			helpEmbed.WithDescription("**__Use:__**\n**» !rr title <title>** to set the title\n**» !rr description <description>** to set the description\n**» !rr addrole <role> <emote> <description>** to add a role\n**» !rr delrole <role>** to remove a role\n**» !rr** to stop editing");
			await ReplyAsync(embed: Embeds.FinishEmbed(helpEmbed, Context)).ConfigureAwait(false);
		}
	}
}
