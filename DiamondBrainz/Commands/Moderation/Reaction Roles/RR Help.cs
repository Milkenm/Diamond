
using Discord;
using Discord.Commands;

using System.Threading.Tasks;

namespace Diamond.Brainz.Commands
{
	public partial class ModerationModule
	{
		public partial class ReactionRoles : ModuleBase<SocketCommandContext>
		{
			[Name("Reaction Roles"), Command("reactionroles help"), Alias("rr help"), Summary("Send a message with help for the Reaction Roles setup command.")]
			public async Task ReactionRolesHelp()
			{
				EmbedBuilder embed = new EmbedBuilder();
				embed.WithTitle("New Reaction Roles edit session started");
				embed.WithDescription("**__Use:__**\n**» !rr title <title>** to set the title\n**» !rr description <description>** to set the description\n**» !rr addrole <role> <emote> <description>** to add a role\n**» !rr delrole <role>** to remove a role\n**» !rr** to stop editing");
				await this.ReplyAsync(embed: embed.FinishEmbed(this.Context)).ConfigureAwait(false);
			}
		}
	}
}
