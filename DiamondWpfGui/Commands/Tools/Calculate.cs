using Diamond.WPF.Utils;

using Discord;
using Discord.Commands;

using NCalc;

using System.Threading.Tasks;

namespace Diamond.WPF.Commands
{
    public partial class Tools_Module : ModuleBase<SocketCommandContext>
    {
        [Name("Calculate"), Command("calculate"), Alias("calc"), Summary("Calculate math expression.")]
        public async Task Calculate(string expression)
        {
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
            embed.WithTitle("🧮 Calculate");

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