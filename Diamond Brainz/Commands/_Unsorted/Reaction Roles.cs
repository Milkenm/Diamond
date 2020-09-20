using Discord;
using Discord.Commands;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Diamond.Brainz.Commands
{
    public partial class UnsortedModule : ModuleBase<SocketCommandContext>
    {
        [Name("Reaction Roles"), Command("reactionroles"), Alias("rr"), Summary("Creates a message which users can react to receive a role.")]
        public async Task ReactionRoles()
        {
            await ReplyAsync("teste").ConfigureAwait(false);
        }
    }
}
