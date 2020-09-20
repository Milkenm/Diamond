using Diamond.Core;
using Diamond.WPFCore.GUI;
using Diamond.WPFCore.Structures;
using Diamond.WPFCore.Structures.Games;

using System.Collections.Generic;

using static Diamond.WPFCore.GUI.Main;

namespace Diamond.WPFCore.Data
{
    public static class GlobalData
    {
        // MAIN STUFF
        public static DiamondCore DiamondCore;

        public static SQLiteDB DB;
        public static Main Main;

        // FOLDERS
        public static readonly Dictionary<EFolder, Folder> Folders = new Dictionary<EFolder, Folder>();

        // TIC TAC TOE GAME
        public static TTTGamesDataTable TTTGamesDataTable = new TTTGamesDataTable();
    }
}