#region Usings
using System;
using System.Threading.Tasks;

using Discord.Commands;
using Discord.WebSocket;

using static DiamondGui.Functions;
using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
	internal static partial class Core
	{
		internal static async Task DiscordMessageReceived(SocketMessage socketMessage)
		{
			try
			{
				var message = socketMessage as SocketUserMessage;
				var context = new SocketCommandContext(Client, message);
				int argPos = 0;

				if (context.Message == null || context.Message.Content == "" || context.User.IsBot == true || !message.HasStringPrefix("!", ref argPos)) return;

				await Command.ExecuteAsync(context, argPos, null);
			}
			catch (Exception _Exception)
			{
				ShowException(_Exception, "Core.DiscordMessageReceived()");
			}
		}
	}
}