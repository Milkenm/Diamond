using Diamond.WPF.Utils;

using Discord;
using Discord.Commands;

using NCalc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Diamond.WPF.Commands
{
    public partial class CommandsModule : ModuleBase<SocketCommandContext>
    {
        [Name("Calculate"), Command("calculate"), Alias("calc"), Summary("Calculate math expression.")]
        public async Task Calculate(string expression)
        {
            Expression e = new Expression(expression);

            Debug.WriteLineIf(e.HasErrors(), "deeeebug: " + e.Error);

            EmbedBuilder embed = new EmbedBuilder()
            {
                Title = "Calculate",
                Description = "Expression: " + expression + "\n\nResult: " + e.Evaluate().ToString(),
            };

            await ReplyAsync(embed: Embeds.FinishEmbed(embed, Context)).ConfigureAwait(false);
        }
    }
}