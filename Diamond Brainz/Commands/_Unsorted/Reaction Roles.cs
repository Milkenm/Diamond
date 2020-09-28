using Diamond.Brainz.Data;
using Diamond.Brainz.Utils;

using Discord;
using Discord.Commands;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using static Diamond.Brainz.Structures.ReactionRoles;

namespace Diamond.Brainz.Commands
{
    public partial class UnsortedModule : ModuleBase<SocketCommandContext>
    {
        public ulong GetMessageId()
        {
            return GlobalData.RRMessagesDataTable.GetLatestChannelMessage(Context.Channel.Id);
        }

        private async Task<IUserMessage> GetMessage()
        {
            return (IUserMessage)await Context.Channel.GetMessageAsync(GetMessageId()).ConfigureAwait(false);
        }

        private async Task<EmbedBuilder> GetEmbed()
        {
            return await Task.FromResult(GlobalData.RRMessagesDataTable.GetMessageEmbed(GetMessageId())).ConfigureAwait(false);
        }

        private readonly List<IEmote> Reactions = new List<IEmote>() { new Emoji("1️⃣"), new Emoji("2️⃣"), new Emoji("3️⃣"), new Emoji("4️⃣"), new Emoji("5️⃣"), new Emoji("6️⃣"), new Emoji("7️⃣"), new Emoji("8️⃣"), new Emoji("9️⃣") };

        [Name("Reaction Roles"), Command("reactionroles"), Alias("rr", "reactionrole"), Summary("Creates a message which users can react to and receive a role.")]
        public async Task ReactionRoles()
        {
            // SEND THE MESSAGE TO DISCORD
            IUserMessage reply = await ReplyAsync(embed: Embeds.FinishEmbed(new EmbedBuilder(), Context)).ConfigureAwait(false);
            // SAVE THE MESSAGE ID ON THE DATABASE
            GlobalData.RRMessagesDataTable.AddMessage(reply.Id, Context.Channel.Id, new EmbedBuilder(), false);

            // SEND HELP EMBED
            EmbedBuilder helpEmbed = new EmbedBuilder();
            helpEmbed.WithTitle("New Reaction Roles edit session started");
            helpEmbed.WithDescription("**__Use:__**\n**» !rr title <title>** to set the title\n**» !rr description <description>** to set the description\n**» !rr addrole <role> <emote> <description>** to add a role\n**» !rr delrole <role>** to remove a role\n**» !rr** to stop editing");
            await ReplyAsync(embed: Embeds.FinishEmbed(helpEmbed, Context)).ConfigureAwait(false);
        }

        [Name("Reaction Roles"), Command("reactionroles title"), Alias("rr title", "rr t", "reactionroles settitle", "rr settitle"), Summary("Edits the Title of a Reaction Roles message.")]
        public async Task ReactionRolesTitle(params string[] title)
        {
            // EDIT THE EMBED
            EmbedBuilder embed = (await GetEmbed().ConfigureAwait(false)).WithTitle(string.Join(' ', title));

            // UPDATE THE EMBED ON DISCORD
            await (await GetMessage().ConfigureAwait(false)).ModifyAsync(msg => msg.Embed = Embeds.FinishEmbed(embed, Context)).ConfigureAwait(false);
        }

        [Name("Reaction Roles"), Command("reactionroles description"), Alias("rr d", "rr desc", "rr description"), Summary("Edits the Description of a Reaction Roles message.")]
        public async Task ReactionRolesDescription(params string[] desc)
        {
            // EDIT THE EMBED
            EmbedBuilder embed = (await GetEmbed().ConfigureAwait(false)).WithDescription(string.Join(' ', desc));

            // UPDATE THE EMBED ON DISCORD
            await (await GetMessage().ConfigureAwait(false)).ModifyAsync(msg => msg.Embed = Embeds.FinishEmbed(embed, Context)).ConfigureAwait(false);
        }

        [Name("Reaction Roles"), Command("reactionroles addrole"), Alias("rr ar", "rr add", "rr addrole", "rr addr"), Summary("Adds a Role to a Reaction Roles message.")]
        public async Task ReactionRolesAddRole(IRole role, string emote, params string[] desc)
        {
            string joinDesc = string.Join(' ', desc);
            EmoteType emoteType;

            try // EMOJI
            {
                var emoji = Twemoji.GetEmojiUrlFromEmoji(emote);
                emoteType = EmoteType.Emoji;
            }
            catch // EMOTE
            {
                Emote.TryParse(emote, out Emote e);

                if (e != null)
                {
                    emoteType = EmoteType.Emote;
                }
                else
                {
                    throw new Exception("Invalid emoji/emote.");
                }
            }

            ulong msgId = (await GetMessage()).Id;
            GlobalData.RRMessagesDataTable.AddRoleLine(msgId, role, emoteType, emote, joinDesc);

            await ReplyAsync($"**__Role Mention:__** {role.Mention}\n**__Emote Type:__** {emoteType}\n**__Description:__** {joinDesc}").ConfigureAwait(false);
        }
    }
}
