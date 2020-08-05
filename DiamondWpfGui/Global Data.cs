using Diamond.Core;
using Diamond.WPF.GUI;
using Diamond.WPF.Structures;
using Diamond.WPF.Structures.Games;

using System.Collections.Generic;

using static Diamond.WPF.GUI.Main;

namespace Diamond.WPF.Data
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