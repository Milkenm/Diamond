#region Usings
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static DiamondGui.Functions;
using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
    internal static partial class Core
    {
        internal static async Task DiscordInstallCommands()
        {
            try
            {
                Client.MessageReceived += DiscordCommandHandler;

                await Command.AddModulesAsync(Assembly.GetEntryAssembly(), null);
            }
            catch (Exception _Exception) { ShowException(_Exception); }
        }
    }
}
