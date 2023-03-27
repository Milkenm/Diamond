using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;

using ScriptsLibV2.ScriptsLib.DiscordBot;

namespace Diamond.API.SlashCommands.Discord
{
	public class AvatarCommand : ISlashCommand
	{
		protected override void SlashCommandBuilder()
		{
			Name = "avatar";
			Description = "Gets the avatar image of the selected user.";
			Options = new List<SlashCommandOptionBuilder>() {
				new SlashCommandOptionBuilder()
				{
					Name = "user",
					Description = "The users whos roles you want to be listed",
					Type = ApplicationCommandOptionType.User,
					IsRequired = true,
				},
				new SlashCommandOptionBuilder()
				{
					Name = "size",
					Description = "The size of the avatar image",
					Type = ApplicationCommandOptionType.Integer,
					IsRequired = false,
				}   .AddChoice("16px", 16)
					.AddChoice("32px", 32)
					.AddChoice("64px", 64)
					.AddChoice("128px", 128)
					.AddChoice("256px", 256)
					.AddChoice("512px", 512)
					.AddChoice("1024px", 1024),
			};
		}
		protected override async Task Action(SocketSlashCommand command, DiscordSocketClient client)
		{
			IUser userOfAvatar = (IUser)command.Data.Options.ElementAt(0).Value;

			string avatar = userOfAvatar.GetAvatarUrl();
			int size = 128;
			if (command.Data.Options.Count == 2)
			{
				size = (int)command.Data.Options.ElementAt(1).Value;
				avatar = avatar.Replace("?size=128", "?size=" + size);
			}

			DefaultEmbed embed = new DefaultEmbed("Avatar", "📸", command);
			embed.AddField("**User**", userOfAvatar.Mention, true);
			embed.AddField("**Size**", $"{size}²px", true);
			embed.WithImageUrl(avatar);

			await embed.SendAsync();
		}
	}
}
