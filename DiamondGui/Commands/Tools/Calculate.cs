using Discord.Commands;

using NCalc;

using System.Threading.Tasks;

namespace DiamondGui
{
	public partial class CommandsModule : ModuleBase<SocketCommandContext>
	{
		[Command("calculate"), Name("Calculate"), Alias("calc"), Summary("Calculate math expression.")]
		public async Task Calculate(string expression)
		{
			Expression e = new Expression(expression);

			await ReplyAsync(e.Evaluate().ToString()).ConfigureAwait(false);
		}
	}
}