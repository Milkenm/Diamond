using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;

using ScriptsLibV2.ScriptsLib.DiscordBot;
using ScriptsLibV2.Util;

namespace Diamond.API.SlashCommands.Tools
{
	public class RandomNumberCommand : ISlashCommand
	{
		protected override void SlashCommandBuilder()
		{
			Name = "randomnumber";
			Description = "Generates a random number between \"min\" and \"max\".";
			Options = new List<SlashCommandOptionBuilder>()
			{
				new SlashCommandOptionBuilder()
				{
					Name = "min",
					Description = "The minimum range.",
					Type = ApplicationCommandOptionType.Integer,
					IsRequired = true,
				},
				new SlashCommandOptionBuilder()
				{
					Name = "max",
					Description = "The maximum range.",
					Type = ApplicationCommandOptionType.Integer,
					IsRequired = true,
				}
			};
		}

		protected override async Task Action(SocketSlashCommand command, DiscordSocketClient client)
		{
			long min = (long)command.Data.Options.ElementAt(0).Value;
			long max = (long)command.Data.Options.ElementAt(1).Value;

			EmbedBuilder embed = new EmbedBuilder();
			embed.WithAuthor("Random Number Generator", TwemojiUtils.GetUrlFromEmoji("🎲"));

			if (min < max)
			{
				long randomNumber = Utils.RandomLong(min, max);
				embed.AddField("**Minimum**", min, true);
				embed.AddField("**Maximum**", max, true);
				embed.AddField("**Generated Number**", randomNumber.ToString());
			}
			else
			{
				embed.WithDescription("**❌ Error:** Invalid numbers.");
			}

			await command.RespondAsync(embed: embed.FinishEmbed(command)).ConfigureAwait(false);
		}
	}
}
