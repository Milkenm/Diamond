#region Usings
using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
    internal static partial class Core
    {
        internal static void LoadLavaPlayer()
        {
            Client.Ready += async () =>
            {
                await LavalinkManager.StartAsync();
            };
        }
    }
}