using System.Collections.Generic;
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
				},
				new SlashCommandOptionBuilder()
				{
					Name = "size",
					Description = "The size of the avatar image",
					Type = ApplicationCommandOptionType.Integer,
				}   .AddChoice("4096px", 4096)
					.AddChoice("2048px", 2048)
					.AddChoice("1024px", 1024)
					.AddChoice("512px", 512)
					.AddChoice("256px", 256)
					.AddChoice("128px", 128)
					.AddChoice("64px", 64)
					.AddChoice("32px", 32)
					.AddChoice("16px", 16),
			};
		}
		protected override async Task Action(SocketSlashCommand command, DiscordSocketClient client)
		{
			IUser userOfAvatar = command.User;
			long size = 128;

			foreach (SocketSlashCommandDataOption option in command.Data.Options)
			{
				switch (option.Name)
				{
					case "user": userOfAvatar = (IUser)option.Value; break;
					case "size":
						{
							size = (long)option.Value;
							break;
						}
				}
			}

			string avatar = userOfAvatar.GetAvatarUrl();
			avatar = avatar.Replace("?size=128", "?size=" + size);

			DefaultEmbed embed = new DefaultEmbed("Avatar", "📸", command);
			embed.AddField("👤 User", userOfAvatar.Mention, true);
			embed.AddField("📏 Size", $"{size}px²", true);
			embed.WithImageUrl(avatar);

			await embed.SendAsync();
		}
	}
}
