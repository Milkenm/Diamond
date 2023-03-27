using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;

using NCalc;

using ScriptsLibV2.ScriptsLib.DiscordBot;

namespace Diamond.API.SlashCommands.Tools
{
	public class CalculateCommand : ISlashCommand
	{
		protected override void SlashCommandBuilder()
		{
			Name = "calculate";
			Description = "Calculate the value of the math expression.";
			Options = new List<SlashCommandOptionBuilder> {
				new SlashCommandOptionBuilder()
				{
					Name = "expression",
					Description = "The math expression to calculate.",
					Type  = ApplicationCommandOptionType.String,
					IsRequired = true,
				},
			};
		}

		protected override async Task Action(SocketSlashCommand command, DiscordSocketClient client)
		{
			string expressionString = (string)command.Data.Options.ElementAt(0).Value;
			Expression expression = new Expression(expressionString);

			DefaultEmbed embed = new DefaultEmbed("Calculate", "🧮", command);

			try
			{
				if (!expression.HasErrors())
				{
					embed.AddField("**Expression**", expression.ToString());
					embed.AddField("**Result**", expression.Evaluate().ToString());
				}
			}
			catch
			{
				embed.WithDescription("❌ **Error:** Invalid expression.");
			}

			await embed.SendAsync();
		}
	}
}
