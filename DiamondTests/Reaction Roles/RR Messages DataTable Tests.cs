using Diamond.Brainz.Data;
using Diamond.Brainz.Data.DataTables;
using Diamond.Brainz.Structures.ReactionRoles;
using Diamond.Brainz.Utils;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;

using static Diamond.Brainz.Data.DataTables.RRMessagesDataTable;

namespace DiamondTests
{
	[TestClass]
	public class RRMessagesDataTableTests
	{
		[TestMethod]
		public void SaveAndReadFile()
		{
			try
			{
				//GlobalData.RRMessagesDataTable = new RRMessagesDataTable();
				//GlobalData.RRMessagesDataTable.AddMessage(new RRMessage());

				//DataTableSaver.SaveToFile(@"C:\Testing\Test.bin", GlobalData.RRMessagesDataTable.DTable);
				//GlobalData.RRMessagesDataTable.DTable.Clear();
				//GlobalData.RRMessagesDataTable.DTable = DataTableSaver.ReadFromFile(@"C:\Testing\Test.bin");

				//Assert.IsTrue(GlobalData.RRMessagesDataTable.DTable.Rows.Count > 0, "\n\nDT.Rows.Count is 0.");

				//ulong channelId = (ulong)GlobalData.RRMessagesDataTable.DTable.Rows[0][nameof(Column.ChannelId)];
				//Assert.IsTrue(channelId == 0, "\n\nChannelId is not 0.\nActual value: " + channelId);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}
	}
}
