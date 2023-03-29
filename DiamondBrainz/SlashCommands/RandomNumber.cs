using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Discord.Interactions;

namespace Diamond.API.SlashCommands
{
    public class RandomNumberCommand : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("randomnumber", "[Public] Generates a random number between \"min\" and \"max\".")]
        public async Task RandomNumberCmd(
            [Summary("min", "The minimum range.")][MinValue(int.MinValue + 1)][MaxValue(int.MaxValue - 1)] int min = 1,
            [Summary("max", "The maximum range.")][MinValue(int.MinValue + 1)][MaxValue(int.MaxValue - 1)] int max = 6,
            [Summary("show-everyone", "Show the command output to everyone.")] bool showEveryone = true
        )
        {
            await DeferAsync(!showEveryone);

            bool swapped = false;
            if (min > max)
            {
                swapped = true;
                (min, max) = (max, min);
            }

            DefaultEmbed embed = new DefaultEmbed("Random Number", "🎲", Context);

            long randomNumber = new Random().Next(min, max);
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
