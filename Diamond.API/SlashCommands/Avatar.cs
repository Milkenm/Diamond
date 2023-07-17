using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.Helpers;

using Discord;
using Discord.Interactions;

namespace Diamond.API.SlashCommands
{
	public class Avatar : InteractionModuleBase<SocketInteractionContext>
	{
		[DSlashCommand("avatar", "Gets the avatar image of the selected user.")]
		public async Task AvatarCommandAsync(
			[Summary("user", "The users whos roles you want to be listed")] IUser user,
			[Summary("size", "The size of the avatar image")] AvatarSize avatarSize = AvatarSize.x4096,
			[ShowEveryone] bool showEveryone = false
		)
		{
			await this.DeferAsync(!showEveryone);

			int size = (int)avatarSize;

			string avatarUrl = user.GetAvatarUrl();
			avatarUrl = avatarUrl.Replace("?size=128", "?size=" + size);

			DefaultEmbed embed = new DefaultEmbed("Avatar", "📸", this.Context);
			_ = embed.AddField("👤 User", user.Mention, true);
			_ = embed.AddField("📏 Size", $"{size}px²", true);
			_ = embed.WithImageUrl(avatarUrl);

			MessageComponent components = new ComponentBuilder()
				.WithButton("Open in browser", style: ButtonStyle.Link, url: avatarUrl)
				.Build();

			_ = await embed.SendAsync(components);
		}

		[UserCommand("Avatar")]
		public async Task AvatarUserCommandAsync(IUser user)
		{
			await this.AvatarCommandAsync(user, AvatarSize.x4096);
		}

		public enum AvatarSize
		{
			[ChoiceDisplay("16px")] x16 = 16,
			[ChoiceDisplay("32px")] x32 = 32,
			[ChoiceDisplay("64px")] x64 = 64,
			[ChoiceDisplay("128px")] x128 = 128,
			[ChoiceDisplay("256px")] x256 = 256,
			[ChoiceDisplay("512px")] x512 = 512,
			[ChoiceDisplay("1024px")] x1024 = 1024,
			[ChoiceDisplay("2048px")] x2048 = 2048,
			[ChoiceDisplay("4096px")] x4096 = 4096,
		}
	}
}
