#region Usings
using System.Threading.Tasks;
using static DiamondGui.Static;
using Discord.Commands;
#endregion Usings



namespace DiamondGui.Commands
{
	public class Hello_World : ModuleBase<SocketCommandContext>
	{
		[Command("helloworld"), Alias("hello", "world"), Summary("Prints 'Hello World!' in event-channel.")]
		public async Task CMD_HelloWorld()
		{
			if (Context.User.Id == 222114807887691777)
			{
				await ReplyAsync("Hello World!");
			}
		}
	}
}