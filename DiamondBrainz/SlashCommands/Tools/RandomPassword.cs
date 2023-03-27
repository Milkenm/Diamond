using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;

using ScriptsLibV2.Extensions;
using ScriptsLibV2.ScriptsLib.DiscordBot;
using ScriptsLibV2.Util;

namespace Diamond.API.SlashCommands.Tools
{
	public class RandomPasswordCommand : ISlashCommand
	{
		protected override void SlashCommandBuilder()
		{
			Name = "randompassword";
			Description = "Generates a random password.";
			Options = new List<SlashCommandOptionBuilder>()
			{
				new SlashCommandOptionBuilder()
				{
					Name = "size",
					Description = "The amount of characters to generate for the password.",
					Type = ApplicationCommandOptionType.Integer,
				},
				new SlashCommandOptionBuilder()
				{
					Name = "useletters",
					Description = "Include lower and uppercase letters from 'a' to 'z' (ignored if 'allowedcharacters' is set).",
					Type = ApplicationCommandOptionType.Boolean,
				},
				new SlashCommandOptionBuilder()
				{
					Name = "usenumbers",
					Description = "Include numbers from 0 to 9 (ignored if 'allowedcharacters' is set).",
					Type = ApplicationCommandOptionType.Boolean,
				},
				new SlashCommandOptionBuilder()
				{
					Name = "usespecialcharacters",
					Description = "Inlcude special characters like '#', '!', '\"' and others (ignored if 'allowedcharacters' is set).",
					Type = ApplicationCommandOptionType.Boolean,
				},
				new SlashCommandOptionBuilder()
				{
					Name = "allowedcharacters",
					Description = "Indicate which characters to use to generate the password.",
					Type = ApplicationCommandOptionType.String,
				}
			};
		}

		protected override async Task Action(SocketSlashCommand command, DiscordSocketClient client)
		{
			long passwordSize = 16;
			bool useLetters = true, useNumbers = true, useSpecialCharacters = true;
			string allowedCharacters = string.Empty;

			foreach (SocketSlashCommandDataOption option in command.Data.Options)
			{
				switch (option.Name)
				{
					case "size": passwordSize = (long)option.Value; break;
					case "useletters": useLetters = (bool)option.Value; break;
					case "usenumbers": useNumbers = (bool)option.Value; break;
					case "usespecialcharacters": useSpecialCharacters = (bool)option.Value; break;
					case "allowedcharacters": allowedCharacters = (string)option.Value; break;
				}
			}

			StringBuilder passwordCharacters = new StringBuilder(allowedCharacters);
			if (allowedCharacters.IsEmpty())
			{
				if (useLetters)
				{
					passwordCharacters.Append("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ");
				}
				if (useNumbers)
				{
					passwordCharacters.Append("0123456789");
				}
				if (useSpecialCharacters)
				{
					passwordCharacters.Append("!\"@#£$§%&/{([])}=?'«»´`~^+¨ºª-.:,;€<>");
				}
			}

			string password = Utils.GeneratePassword(passwordSize, passwordCharacters.ToString());

			DefaultEmbed embed = new DefaultEmbed("Password Generator", "🔐", command);
			embed.AddField("🔡 Allowed Characters", passwordCharacters.ToString());
			if (allowedCharacters.IsEmpty())
			{
				embed.AddField("🔤 Letters", BoolToString(useLetters), true);
				embed.AddField("🔢 Numbers", BoolToString(useNumbers), true);
				embed.AddField("🔣 Special Characters", BoolToString(useSpecialCharacters), true);
			}
			embed.AddField("#️⃣ Password Size", $"{password.Length} characters", true);
			embed.WithFancyDescription($"||{password}||", "🔑 Here is your password 🔑", "Tip: You can copy the password without releaving it by selecting the black square and hitting CTRL+C.");

			await embed.SendAsync(true);
		}

		private string BoolToString(bool value)
		{
			return value ? "Yes" : "No";
		}
	}
}
