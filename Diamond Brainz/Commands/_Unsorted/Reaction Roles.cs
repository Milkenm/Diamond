using Diamond.Brainz.Data;
using Diamond.Brainz.Structures.ReactionRoles;
using Diamond.Brainz.Utils;

using Discord;
using Discord.Commands;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Diamond.Brainz.Commands
{
	public partial class UnsortedModule : ModuleBase<SocketCommandContext>
	{
		private readonly List<IEmote> Reactions = new List<IEmote>() { new Emoji("1️⃣"), new Emoji("2️⃣"), new Emoji("3️⃣"), new Emoji("4️⃣"), new Emoji("5️⃣"), new Emoji("6️⃣"), new Emoji("7️⃣"), new Emoji("8️⃣"), new Emoji("9️⃣") };

		[Name("Reaction Roles"), Command("reactionroles"), Alias("rr", "reactionrole"), Summary("Creates a message which users can react to and receive a role.")]
		public async Task ReactionRoles()
		{
			// SEND THE MESSAGE TO DISCORD
			EmbedBuilder embed = new EmbedBuilder();
			IUserMessage reply = await ReplyAsync(embed: Embeds.FinishEmbed(embed, Context)).ConfigureAwait(false);

			// SAVE THE MESSAGE ON THE DATABASE
			GlobalData.RRMessagesDataTable.AddMessage(Context, reply, embed);

			// SEND HELP EMBED
			EmbedBuilder helpEmbed = new EmbedBuilder();
			helpEmbed.WithTitle("New Reaction Roles edit session started");
			helpEmbed.WithDescription("**__Use:__**\n**» !rr title <title>** to set the title\n**» !rr description <description>** to set the description\n**» !rr addrole <role> <emote> <description>** to add a role\n**» !rr delrole <role>** to remove a role\n**» !rr** to stop editing");
			await ReplyAsync(embed: Embeds.FinishEmbed(helpEmbed, Context)).ConfigureAwait(false);
		}

		[Name("Reaction Roles"), Command("reactionroles title"), Alias("rr title", "rr t", "reactionroles settitle", "rr settitle"), Summary("Edits the Title of a Reaction Roles message.")]
		public async Task ReactionRolesTitle(params string[] title)
		{
			RRMessage rrMsg = GlobalData.RRMessagesDataTable.GetRRMessageByChannelId(Context.Channel.Id);

			await rrMsg.SetTitle(title);
			await rrMsg.ModifyDiscordEmbedAsync();
		}

		[Name("Reaction Roles"), Command("reactionroles description"), Alias("rr description", "rr desc", "rr d"), Summary("Edits the Description of a Reaction Roles message.")]
		public async Task ReactionRolesDescription(params string[] description)
		{
			RRMessage rrMsg = GlobalData.RRMessagesDataTable.GetRRMessageByChannelId(Context.Channel.Id);

			await rrMsg.SetDescription(description);
			await rrMsg.ModifyDiscordEmbedAsync();
		}

		[Name("Reaction Roles"), Command("reactionroles addrole"), Alias("rr ar", "rr addrole", "rr addr", "rr add"), Summary("Adds a Role to a Reaction Roles message.")]
		public async Task ReactionRolesAddRole(IRole role, string emote, params string[] description)
		{
			RRMessage rrMsg = GlobalData.RRMessagesDataTable.GetRRMessageByChannelId(Context.Channel.Id);

			await rrMsg.AddRoleLine(role, emote, description);
			await rrMsg.ModifyDiscordEmbedAsync();
		}
	}
}
