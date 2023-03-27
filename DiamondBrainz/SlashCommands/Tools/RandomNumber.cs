using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;

using ScriptsLibV2.ScriptsLib.DiscordBot;

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
					MinValue = int.MinValue + 1,
					MaxValue = int.MaxValue - 1,
				},
				new SlashCommandOptionBuilder()
				{
					Name = "max",
					Description = "The maximum range.",
					Type = ApplicationCommandOptionType.Integer,
					MinValue = int.MinValue + 1,
					MaxValue = int.MaxValue - 1,
				}
			};
		}

		protected override async Task Action(SocketSlashCommand command, DiscordSocketClient client)
		{
			long min = 1, max = 6;

			foreach (SocketSlashCommandDataOption option in command.Data.Options)
			{
				switch (option.Name)
				{
					case "min": min = (long)option.Value; break;
					case "max": max = (long)option.Value; break;
				}
			}

			bool swapped = false;
			if (min > max)
			{
				swapped = true;
				(min, max) = (max, min);
			}

			DefaultEmbed embed = new DefaultEmbed("Random Number", "🎲", command);

			long randomNumber = new Random().Next((int)min, (int)max);
			embed.AddField("🔽 Minimum", min, true);
			embed.AddField("🔼 Maximum", max, true);
			embed.WithDescription($"`Generated Number:` {NumberToEmoji(randomNumber)}\n`Text:` {randomNumber}{(swapped ? "\n\n:warning: **__Note__:** 'min' and 'max' have been swapped because\nthe minimum value was larger than the maximum one." : "")}");

			await embed.SendAsync();
		}

		private string NumberToEmoji(long number)
		{
			string numbersString = number.ToString();

			Dictionary<string, string> replacementsMap = new Dictionary<string, string>()
			{
				{ "0", "0️⃣" },
				{ "1", "1️⃣" },
				{ "2", "2️⃣" },
				{ "3", "3️⃣" },
				{ "4", "4️⃣" },
				{ "5", "5️⃣" },
				{ "6", "6️⃣" },
				{ "7", "7️⃣" },
				{ "8", "8️⃣" },
				{ "9", "9️⃣" },
			};

			foreach (KeyValuePair<string, string> pair in replacementsMap)
			{
				numbersString = numbersString.Replace(pair.Key, pair.Value);
			}

			return numbersString;
		}
	}
}
