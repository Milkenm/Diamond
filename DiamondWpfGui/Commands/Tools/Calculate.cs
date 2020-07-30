using Diamond.WPF.Utils;

using Discord;
using Discord.Commands;

using NCalc;

using System.Threading.Tasks;

namespace Diamond.WPF.Commands
{
    public partial class CommandsModule : ModuleBase<SocketCommandContext>
    {
        [Name("Calculate"), Command("calculate"), Alias("calc"), Summary("Calculate math expression.")]
        public async Task Calculate(string expression)
        {
            Expression e = new Expression(expression);

            expression = expression.Replace("*", @"\*").Replace("_", @"\_").Replace("~", @"\~").Replace("`", @"\`");
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
            embed.WithTitle("Calculate");
            embed.WithDescription("**Expression:** " + expression + "\n**Result:** " + (!errors ? result : "__❌ Error! (Invalid expression)__"));

            await ReplyAsync(embed: Embeds.FinishEmbed(embed, Context)).ConfigureAwait(false);
        }
    }
}