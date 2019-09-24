#region Usings
using Discord.Commands;
using System;
using System.Diagnostics;
using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
    internal static partial class Functions
    {
        internal static void EasterEggs(SocketCommandContext Context)
        {
            try
            {
                if (Context.Message.ToString().Contains("(╯°□°）╯︵ ┻━┻")) Client.GetGuild(Context.Guild.Id).GetTextChannel(Context.Channel.Id).SendMessageAsync("┬─┬ ノ( ゜-゜ノ)");
            }
            catch (Exception _Exception)
            {
                ShowException(_Exception, "DiamondGui.Functions.EasterEggs()");
            }
        }
    }
}