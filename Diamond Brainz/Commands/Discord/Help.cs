﻿using Diamond.Brainz.Data;
using Diamond.Brainz.Utils;

using Discord;
using Discord.Commands;

using System.Threading.Tasks;

namespace Diamond.Brainz.Commands
{
    public partial class DiscordModule : ModuleBase<SocketCommandContext>
    {
        [Name("Help"), Command("help"), Alias("commands", "cmd", "cmds"), Remarks("lel"), Summary("Returns a list with every command available.")]
        public async Task Help()
        {
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithAuthor("Help", Twemoji.GetEmojiUrlFromEmoji("📔"));

            foreach (CommandInfo ci in GlobalData.DiamondCore.Commands.Commands)
            {
                embed.AddField(ci.Name, "-" + ci.Remarks);
            }

            await ReplyAsync(embed: Embeds.FinishEmbed(embed, Context)).ConfigureAwait(false);
        }
    }
}