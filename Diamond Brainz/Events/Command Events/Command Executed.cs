using Diamond.Brainz.Data;

using Discord;
using Discord.Commands;

using System.Threading.Tasks;

namespace Diamond.Brainz.Events
{
    public partial class CommandEvents
    {
        public async Task CommandExecuted(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            if (!string.IsNullOrEmpty(result?.ErrorReason) && command.IsSpecified)
            {
                await context.Channel.SendMessageAsync(result.ErrorReason).ConfigureAwait(false);

                await GlobalData.Brainz.TriggerLogEventAsync($"{context.User.Username} executed the command \"{command.Value.Name}\" on server \"{context.Guild.Name}\".").ConfigureAwait(false);
            }
        }
    }
}