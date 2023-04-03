using System.Threading.Tasks;

using Discord;
using Discord.Interactions;

namespace Diamond.API.SlashCommands
{
    public class Avatar : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("avatar", "[Hidden] Gets the avatar image of the selected user.")]
        public async Task AvatarCommandAsync(
            [Summary("user", "The users whos roles you want to be listed")] IUser user,
            [Summary("size", "The size of the avatar image")] AvatarSize avatarSize = AvatarSize.x128,
            [Summary("show-everyone", "Show the command output to everyone.")] bool showEveryone = false
        )
        {
            await DeferAsync(!showEveryone);

            int size = (int)avatarSize;

            string avatar = user.GetAvatarUrl();
            avatar = avatar.Replace("?size=128", "?size=" + size);

            DefaultEmbed embed = new DefaultEmbed("Avatar", "📸", Context.Interaction);
            embed.AddField("👤 User", user.Mention, true);
            embed.AddField("📏 Size", $"{size}px²", true);
            embed.WithImageUrl(avatar);

            await embed.SendAsync();
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
