using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;

using ScriptsLibV2.ScriptsLib.DiscordBot;
using ScriptsLibV2.Util;

namespace Diamond.API.SlashCommands.Discord
{
	public class EmojiCommand : ISlashCommand
	{
		protected override void SlashCommandBuilder()
		{
			Name = "twemoji";
			Description = "Gets the provided twemoji.";
			Options = new List<SlashCommandOptionBuilder>()
			{
				new SlashCommandOptionBuilder()
				{
					Name = "twemoji",
					Type = ApplicationCommandOptionType.String,
					Description = "The twemoji to get.",
					IsRequired = true,
				},
			};
		}

		protected override async Task Action(SocketSlashCommand command, DiscordSocketClient client)
		{
			string emoji = (string)command.Data.Options.ElementAt(0).Value;

			DefaultEmbed embed = new DefaultEmbed("Twemoji", "😃", command);

			try
			{
				string emojiUrl = TwemojiUtils.GetUrlFromEmoji(emoji);
				embed.WithImageUrl(emojiUrl);
			}
			catch
			{
				embed.WithDescription($"No valid twemoji found for '{emoji}'.");
			}

			await embed.SendAsync();
		}
	}
}
