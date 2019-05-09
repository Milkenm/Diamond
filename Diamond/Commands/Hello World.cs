#region Using
using System;
using System.Threading.Tasks;

using Discord.Commands;
using ScriptsLib.Database;
#endregion

namespace Diamond.Commands
{
	public class Hello_World : ModuleBase<SocketCommandContext>
	{
		[Command("helloworld"), Alias("hello", "world"), Summary("Prints 'Hello World!' in event-channel.")]
		public async Task CMD_HelloWorld()
		{
			SlDatabase _Database = new SlDatabase();
			if ($"{Context.User.Id}" == _Database.Select("DConfig", "Value", "Config = 'MilkenmID'")[0])
			{
				await ReplyAsync("Hello World!");
			}
		}
	}
}