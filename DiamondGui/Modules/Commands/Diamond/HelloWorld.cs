#region Usings
using System;
using System.Threading.Tasks;

using Discord.Commands;

using static DiamondGui.Functions;
using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
	public partial class Commands : ModuleBase<SocketCommandContext>
	{
		[Command("helloworld"), Alias("hello", "world"), Summary("Prints 'Hello World!' in event-channel.")]
		public async Task HelloWorld()
        {
            try
            {
				Settings.CommandsUsed += 1; Settings.Save();

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