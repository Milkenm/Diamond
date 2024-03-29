﻿using System.Diagnostics;
using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.Helpers;

using Discord.Interactions;

using ScriptsLibV2.Util;

namespace Diamond.API.SlashCommands
{
    public class Twemoji : InteractionModuleBase<SocketInteractionContext>
    {
        [DSlashCommand("twemoji", "Gets the provided twemoji.")]
        public async Task TwemojiCommandAsync(
            [Summary("twemoji", "The twemoji to get.")] string twemoji,
            [ShowEveryone] bool showEveryone = false
        )
        {
            await DeferAsync(!showEveryone);

            DefaultEmbed embed = new DefaultEmbed("Twemoji", "😃", Context);

            try
            {
                Debug.WriteLine(twemoji);
                string emojiUrl = TwemojiUtils.GetUrlFromEmoji(twemoji);
                embed.WithImageUrl(emojiUrl);
                embed.AddField("🔢 Code", TwemojiUtils.GetEmojiCode(twemoji), true);
                embed.AddField("📏 Size", "72px²", true);
            }
            catch
            {
                embed.WithDescription($"No valid twemoji found for '{twemoji}'.");
            }

            await embed.SendAsync();
        }
    }
}
