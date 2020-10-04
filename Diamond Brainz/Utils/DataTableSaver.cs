using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Diamond.Brainz.Utils
{
	public static class DataTableSaver
	{
		public static void SaveToFile(string binaryFilePath, DataTable dt)
		{
			FileStream fs = new FileStream(binaryFilePath, FileMode.Create);
			BinaryFormatter bf = new BinaryFormatter();

			bf.Serialize(fs, dt);
			fs.Close();
		}

		public static DataTable ReadFromFile(string binaryFilePath)
		{
			FileStream fs = new FileStream(binaryFilePath, FileMode.Open);
			BinaryFormatter bf = new BinaryFormatter();

			DataTable dt = (DataTable)bf.Deserialize(fs);
			fs.Close();

			return dt;
		}
	}
}
