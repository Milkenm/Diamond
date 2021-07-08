using Diamond.Brainz.Data.DataTables;
using Diamond.Brainz.Structures;
using Diamond.Brainz.Structures.Games;
using Diamond.Core;

using System.Collections.Generic;

using static Diamond.Brainz.Utils.Folders;

namespace Diamond.Brainz.Data
{
	public static class GlobalData
	{
		// MAIN STUFF
		public static DiamondCore DiamondCore;
		public static Bot.Bot Bot;

		// DATA
		public static SQLiteDB DB;

		// FOLDERS
		public static readonly Dictionary<EFolder, Folder> Folders = new Dictionary<EFolder, Folder>();

		// DATA TABLES
		public static TTTGamesDataTable TTTGamesDataTable = new TTTGamesDataTable();
		public static RRMessagesDataTable RRMessagesDataTable = new RRMessagesDataTable();
	}
}