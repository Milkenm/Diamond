#region Usings
using Discord.Commands;
using SharpLink;
using static DiamondGui.Static;
#endregion Usings



namespace Dota_Geek.Modules
{
    public class Music : ModuleBase<SocketCommandContext>
    {
        LavalinkManager lavalinkManager = new LavalinkManager(Client, new LavalinkManagerConfig
        {
            RESTHost = "localhost",
            RESTPort = 2333,
            WebSocketHost = "localhost",
            WebSocketPort = 2333,
            Authorization = "YOUR_SECRET_AUTHORIZATION_KEY",
            TotalShards = 1
        });
    }
}