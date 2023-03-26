using Discord;
using Discord.Commands;

using System.Threading.Tasks;

namespace Diamond.API.Events
{
	public partial class CommandEvents
	{
		public async Task CommandExecuted(Optional<CommandInfo> command, ICommandContext context, IResult result)
		{
			string channel = "the private chat";
			if (context.Guild != null)
			{
				channel = $"the server \"{context.Guild.Name}\"";
			}

			//await dcore.TriggerLogEventAsync($"{context.User.Username} executed the command \"{command.Value.Name}\" on {channel}.").ConfigureAwait(false);

			if (!string.IsNullOrEmpty(result?.ErrorReason) && command.IsSpecified)
			{
				//await dcore.TriggerLogEventAsync($"Error on command \"{command.Value.Name}\" (executed by \"{context.User.Username}\" on {channel})").ConfigureAwait(false);
				await context.Channel.SendMessageAsync(result.ErrorReason).ConfigureAwait(false);
			}
		}
	}
}