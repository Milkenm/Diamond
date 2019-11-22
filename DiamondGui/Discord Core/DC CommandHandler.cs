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
	internal static partial class DiscordCore
	{
		internal static async Task DiscordCoreCommandHandler(SocketMessage _SocketMsg)
		{
			try
			{
				if (!(_SocketMsg is SocketUserMessage _Message))
				{
					return;
				}

				int _ArgPos = 0;

				if (Settings.DisableCommands || !Settings.AllowPrefix || !(_Message.HasCharPrefix('!', ref _ArgPos) || _Message.HasMentionPrefix(Client.CurrentUser, ref _ArgPos) && Settings.AllowMention) || _Message.Author.IsBot)
				{
					return;
				}

				SocketCommandContext _Context = new SocketCommandContext(Client, _Message);

				await Command.ExecuteAsync(_Context, _ArgPos, null);
			}
			catch (Exception _Exception) { ShowException(_Exception); }
		}
	}
}