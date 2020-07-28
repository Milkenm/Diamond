#region Usings

using Discord.Commands;

using System.Threading.Tasks;

using static DiamondGui.Static;

#endregion Usings

namespace DiamondGui
{
	public partial class CommandsModule : ModuleBase<SocketCommandContext>
	{
		[Command("helloworld"), Alias("hello", "world"), Summary("Prints 'Hello World!' in event-channel.")]
		public async Task HelloWorld()
		{
			settings.CommandsUsed++;

			if (Context.User.Id == settings.AdminId)
			{
				await ReplyAsync("Hello World!").ConfigureAwait(false);
			}
		}
	}
}