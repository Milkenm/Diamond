using Diamond.Brainz.Utils;

using Discord;
using Discord.Commands;

using System.Threading.Tasks;

namespace Diamond.Brainz.Commands
{
	public partial class PokemonModule : ModuleBase<SocketCommandContext>
	{
		[Name("Pokemon Counters"), Command("pokecounters"), Alias("pcounters", "pcount"), Summary("Returns the counter for the provided element.")]
		public async Task Calculate(string element)
		{
			element = Capitalize(element);

			string counters = (element) switch
			{
				"Fairy" => "Poison/Steel",
				"Steel" => "Fire/Fight/Ground",
				"Dark" => "Fight/Bug/Fairy",
				"Dragon" => "Ice/Dragon/Fairy",
				"Ghost" => "Ghost/Dark",
				"Rock" => "Water/Grass/Fight/Ground/Steel",
				"Bug" => "Fire/Flying/Rock",
				"Physic" => "Bug/Ghost/Dark",
				"Flying" => "Electric/Ice/Rock",
				"Ground" => "Water/Grass/Ice",
				"Poison" => "Ground/Physic",
				"Fight" => "Flying/Physic/Fairy",
				"Ice" => "Fire/Fight/Rock/Steel",
				"Grass" => "Fire/Ice/Poison/Flying/Bug",
				"Electric" => "Ground",
				"Water" => "Electric/Grass",
				"Fire" => "Water/Ground/Rock",
				"Normal" => "Normal",
				_ => null,
			};

			EmbedBuilder embed = new EmbedBuilder();
			if (counters == null)
			{
				embed.WithAuthor("PokeCounters"/*, Twemoji.GetEmojiUrlFromEmoji("❌")*/);
				embed.WithTitle($"Element '{element}' not found.");
			}
			else
			{
				embed.WithAuthor("PokeCounters"/*, Twemoji.GetEmojiUrlFromEmoji("♻")*/);
				embed.WithTitle($"Counters for element: **__{element}__**");
				embed.WithDescription("🔹 " + string.Join("\n🔹 ", counters.Split("/")));
			}
			await ReplyAsync(embed: Embeds.FinishEmbed(embed, Context)).ConfigureAwait(false);
		}

		private string Capitalize(string word)
		{
			return word[0].ToString().ToUpper() + word.Substring(1).ToLower();
		}
	}
}