#region Usings
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DiamondGui.Functions;
using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
    internal static partial class Core
    {
        internal static async Task DiscordCommandHandler(SocketMessage _SocketMsg)
        {
            try
            {
                var _Message = _SocketMsg as SocketUserMessage;
                if (_Message == null) return;
				
                int _ArgPos = 0;
				
                if (!(_Message.HasCharPrefix('!', ref _ArgPos) || _Message.HasMentionPrefix(Client.CurrentUser, ref _ArgPos)) || _Message.Author.IsBot)
                {
                    EasterEggs(new SocketCommandContext(Client, _Message));
                }
				
                var _Context = new SocketCommandContext(Client, _Message);
				
                await Command.ExecuteAsync(_Context, _ArgPos, null);
            }
            catch (Exception _Exception)
            {
                ShowException(_Exception, "DiamondGui.Core.DiscordCommandHandler()");
            }
        }
    }
}