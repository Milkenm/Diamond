using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;

using ScriptsLibV2.ScriptsLib.DiscordBot;
using ScriptsLibV2.Util;

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
					Choices = new List<ApplicationCommandOptionChoiceProperties>()
					{
						new ApplicationCommandOptionChoiceProperties()
						{
							Name = "16px",
							Value = 16,
						},
						new ApplicationCommandOptionChoiceProperties()
						{
							Name = "32px",
							Value = 32,
						},
						new ApplicationCommandOptionChoiceProperties()
						{
							Name = "64px",
							Value = 64,
						},
						new ApplicationCommandOptionChoiceProperties()
						{
							Name = "128px",
							Value = 128,
						},
						new ApplicationCommandOptionChoiceProperties()
						{
							Name = "256px",
							Value = 256,
						},
						new ApplicationCommandOptionChoiceProperties()
						{
							Name = "512px",
							Value = 512,
						},
						new ApplicationCommandOptionChoiceProperties()
						{
							Name = "1024px",
							Value = 1024,
						},
					},
				},
			};
		}
		protected override async Task Action(SocketSlashCommand command, DiscordSocketClient client)
		{
			IUser userOfAvatar = (IUser)command.Data.Options.ElementAt(0).Value;

			string avatar = userOfAvatar.GetAvatarUrl();
			long size = 128;
			if (command.Data.Options.Count == 2)
			{
				size = (long)command.Data.Options.ElementAt(1).Value;
				avatar = avatar.Replace("?size=128", "?size=" + size);
			}

			EmbedBuilder embed = new EmbedBuilder();
			embed.WithAuthor("Avatar", TwemojiUtils.GetUrlFromEmoji("📸"));
			embed.AddField("**User**", userOfAvatar.Mention, true);
			embed.AddField("**Size**", $"{size}²px", true);
			embed.WithImageUrl(avatar);

			await command.RespondAsync(embed: embed.FinishEmbed(command)).ConfigureAwait(false);
		}
	}
}
