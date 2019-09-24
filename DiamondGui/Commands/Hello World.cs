#region Usings
using System.Threading.Tasks;

using Discord.Commands;
#endregion Usings



namespace DiamondGui.Commands
{
	internal class Hello_World : ModuleBase<SocketCommandContext>
	{
		[Command("helloworld"), Alias("hello", "world"), Summary("Prints 'Hello World!' in event-channel.")]
		internal async Task CMD_HelloWorld()
		{
			if ($"{Context.User.Id}" == "222114807887691777")
			{
				await ReplyAsync("Hello World!");
			}
		}
	}
}