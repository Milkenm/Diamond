using System;
using System.Threading.Tasks;

using Discord.Interactions;

namespace Diamond.API.SlashCommands;
public class Info : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("info", "[Hidden] Shows info about the bot.")]
    public async Task InfoCommandAsync(
            [Summary("show-everyone", "Show the command output to everyone.")] bool showEveryone = false
    )
    {
        await DeferAsync(!showEveryone);

        string avatar = Context.Client.CurrentUser.GetAvatarUrl();
        avatar = avatar.Replace("?size=128", "?size=" + 512);
        long msDelay = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - Context.Interaction.CreatedAt.ToUnixTimeMilliseconds();

        DefaultEmbed embed = new DefaultEmbed("Info", "ℹ️", Context.Interaction);
        embed.AddField("💻 Developer", "<@222114807887691777>");
        embed.AddField("🏷️ Version", $"v1.0", true);
        embed.AddField("⏰ Delay", $"{msDelay}ms", true);
        embed.WithThumbnailUrl(avatar);

        await embed.SendAsync();
    }
}
