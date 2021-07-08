using Diamond.Brainz.Data;
using Diamond.Brainz.Utils;

using System.IO;

using static Diamond.Brainz.Utils.Folders;

namespace Diamond.Brainz
{
	public partial class Bot
	{
		public void DataTablesManager(DataTableAction action)
		{
			string fileName = GlobalData.Folders[EFolder.Data].Path + "RR Messages DataTable.bin";

			if (action == DataTableAction.Save)
			{
				DataTableSaver.SaveToFile(fileName, GlobalData.RRMessagesDataTable.DTable);
			}
			else if (action == DataTableAction.Read)
			{
				if (File.Exists(fileName))
				{
					GlobalData.RRMessagesDataTable.DTable = DataTableSaver.ReadFromFile(fileName);
				}
			}
		}

		public enum DataTableAction
		{
			Save,
			Read,
		}
	}
}