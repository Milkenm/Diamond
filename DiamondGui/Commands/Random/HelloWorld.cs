#region Usings
using Discord.Commands;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DiamondGui.Functions;
#endregion Usings



namespace DiamondGui.Commands
{
    public class HelloWorld : ModuleBase<SocketCommandContext>
	{
		[Command("helloworld"), Alias("hello", "world"), Summary("Prints 'Hello World!' in event-channel.")]
		public async Task CMD_HelloWorld()
        {
            try
            {
                if (Context.User.Id == 222114807887691777)
                {
                    await ReplyAsync("Hello World!");
                }
            }
            catch (Exception _Exception)
            {
                ShowException(_Exception, "DiamondGui.Commands.HelloWorld.CMD_HelloWorld()");
            }
		}
	}
}