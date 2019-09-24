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
                // Don't process the command if it was a system message
                var _Message = _SocketMsg as SocketUserMessage;
                if (_Message == null) return;

                // Create a number to track where the prefix ends and the command begins
                int _ArgPos = 0;

                // Determine if the message is a command based on the prefix and make sure no bots trigger commands
                if (!(_Message.HasCharPrefix('!', ref _ArgPos) || _Message.HasMentionPrefix(Client.CurrentUser, ref _ArgPos)) || _Message.Author.IsBot)
                {
                    EasterEggs(new SocketCommandContext(Client, _Message));
                }

                // Create a WebSocket-based command context based on the message
                var _Context = new SocketCommandContext(Client, _Message);

                // Execute the command with the command context we just
                // created, along with the service provider for precondition checks.

                // Keep in mind that result does not indicate a return value
                // rather an object stating if the command executed successfully.
                await Command.ExecuteAsync(_Context, _ArgPos, null);

                // Optionally, we may inform the user if the command fails
                // to be executed; however, this may not always be desired,
                // as it may clog up the request queue should a user spam a
                // command.
                // if (!result.IsSuccess)
                // await context.Channel.SendMessageAsync(result.ErrorReason);
            }
            catch (Exception _Exception)
            {
                ShowException(_Exception, new StackFrame().GetMethod().DeclaringType.ReflectedType.ToString());
            }
        }
    }
}