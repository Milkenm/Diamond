﻿using Diamond.Brainz.Utils;

using Discord;
using Discord.Commands;

using NCalc;

using System.Threading.Tasks;

namespace Diamond.Brainz.Commands
{
    public partial class ToolsModule : ModuleBase<SocketCommandContext>
    {
        [Name("Calculate"), Command("calculate"), Alias("calc"), Summary("Calculate math expression.")]
        public async Task Calculate([Remainder] string expression)
        {
            expression = expression.Replace(" ", "");
            Expression e = new Expression(expression);

            expression = Text.Purify(expression);
            bool errors = e.HasErrors();
            string result = null;

            try
            {
                result = e.Evaluate().ToString();
            }
            catch
            {
                errors = true;
            }

            EmbedBuilder embed = new EmbedBuilder();
            embed.WithAuthor("Calculate", Twemoji.GetEmojiUrlFromEmoji("🧮"));

            if (!errors)
            {
                embed.AddField("**Expression**", expression);
                embed.AddField("**Result**", result);
            }
            else
            {
                embed.WithDescription("**❌ Error:** Invalid expression.");
            }

            await ReplyAsync(embed: Embeds.FinishEmbed(embed, Context)).ConfigureAwait(false);
        }
    }
}