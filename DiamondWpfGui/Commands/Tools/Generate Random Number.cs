using Diamond.WPF.Utils;

using Discord;
using Discord.Commands;

using System;
using System.Threading.Tasks;

namespace Diamond.WPF.Commands
{
    public partial class Tools_Module : ModuleBase<SocketCommandContext>
    {
        [Name("Random Number Generator"), Command("random number"), Alias("rnum", "rand num", "rand number", "random num", "rn", "random", "rnd", "rand"), Summary("Generates a random number between min and max numbers.")]
        public async Task GenerateRandomNumber(int min, int max)
        {
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithTitle("🎲 Random Number Generator");

            if (min < max)
            {
                int number = new Random().Next(min, max);

                embed.AddField("**Generated Number**", number.ToString());
            }
            else
            {
                embed.WithDescription("**❌ Error:** Invalid numbers.");
            }

            await ReplyAsync(embed: Embeds.FinishEmbed(embed, Context)).ConfigureAwait(false);
        }
    }
}