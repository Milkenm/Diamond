#region Usings
using System;
using System.Threading.Tasks;

using Discord.Commands;

using static DiamondGui.Functions;
using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
	public partial class CommandsModule : ModuleBase<SocketCommandContext>
	{
		[Command("helloworld"), Alias("hello", "world"), Summary("Prints 'Hello World!' in event-channel.")]
		public async Task HelloWorld()
		{
			try
			{
				Settings.CommandsUsed += 1; Settings.Save();

				if (Context.User.Id == Settings.AdminId)
				{
					await ReplyAsync("Hello World!");
				}
			}
			catch (Exception _Exception) { ShowException(_Exception); }
		}
	}
}