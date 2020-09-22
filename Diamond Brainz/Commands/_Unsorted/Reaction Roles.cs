using Diamond.Brainz.Utils;

using Discord;
using Discord.Commands;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Diamond.Brainz.Commands
{
    public partial class UnsortedModule : ModuleBase<SocketCommandContext>
    {
        private readonly List<IEmote> Reactions = new List<IEmote>() { new Emoji("1️⃣"), new Emoji("2️⃣"), new Emoji("3️⃣"), new Emoji("4️⃣"), new Emoji("5️⃣"), new Emoji("6️⃣"), new Emoji("7️⃣"), new Emoji("8️⃣"), new Emoji("9️⃣") };

        [Name("Reaction Roles"), Command("reactionroles"), Alias("rr"), Summary("Creates a message which users can react to receive a role.")]
        public async Task ReactionRoles(params string[] input)
        {
            string join = string.Join(' ', input);
            string[] args = join.Split(';');

            string title = args[0];
            string description = "";
            if (args.Length > 1)
            {
                description = args[1];
            }

            EmbedBuilder embed = new EmbedBuilder();
            embed.WithTitle(title);
            embed.WithDescription(description);

            await ReplyAsync(embed: Embeds.FinishEmbed(embed, Context)).ConfigureAwait(false);
        }
    }
}
