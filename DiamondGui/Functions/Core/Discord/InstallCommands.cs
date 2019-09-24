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

                // Here we discover all of the command modules in the entry 
                // assembly and load them. Starting from Discord.NET 2.0, a
                // service provider is required to be passed into the
                // module registration method to inject the 
                // required dependencies.
                //
                // If you do not use Dependency Injection, pass null.
                // See Dependency Injection guide for more information.
                await Command.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: null);
            }
            catch (Exception _Exception)
            {
                ShowException(_Exception, "DiamondGui.Core.DiscordInstallCommands()");
            }
        }
    }
}
