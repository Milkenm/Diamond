using Diamond.Brainz.Utils;

using Discord;
using Discord.Commands;

using System.Threading.Tasks;

namespace Diamond.Brainz.Commands
{
	public partial class UnsortedModule
	{
		public partial class ReactionRoles : ModuleBase<SocketCommandContext>
		{
			[Name("Reaction Roles"), Command("reactionroles help"), Alias("rr help"), Summary("Send a message with help for the Reaction Roles setup command.")]
			public async Task ReactionRolesHelp()
			{
				EmbedBuilder helpEmbed = new EmbedBuilder();
				helpEmbed.WithTitle("New Reaction Roles edit session started");
				helpEmbed.WithDescription("**__Use:__**\n**» !rr title <title>** to set the title\n**» !rr description <description>** to set the description\n**» !rr addrole <role> <emote> <description>** to add a role\n**» !rr delrole <role>** to remove a role\n**» !rr** to stop editing");
				await ReplyAsync(embed: Embeds.FinishEmbed(helpEmbed, Context)).ConfigureAwait(false);
			}
		}
	}
}
