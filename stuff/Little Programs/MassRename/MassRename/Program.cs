using System;
using System.IO;

namespace MassRename
{
	internal class Program
	{
		private static void Main()
		{
			MassRename2();
		}

		private static void MassRename1()
		{
			const string baseDir = @"C:\Milkenm\Projects\Diamond\docs\static\lol\champs";
			string[] dirs = Directory.GetDirectories(baseDir);

			foreach (string dir in dirs)
			{
				string folderName = Path.GetFileName(dir);

				Directory.Move(dir, baseDir + @"\!");
				Directory.Move(dir, baseDir + @"\" + folderName.ToLower().Replace("!", ""));
			}
		}

		private static void MassRename2()
		{
			const string baseDir = @"C:\Milkenm\Projects\Diamond\docs\static\lol\spells";
			const string extension = ".png";
			string[] files = Directory.GetFiles(baseDir);

			foreach (string file in files)
			{
				string fileName = Path.GetFileName(file);

				File.Move(file, baseDir + @"\!" + extension);
				File.Move(baseDir + @"\!" + extension, baseDir + @"\" + fileName.ToLower().Replace("!", ""));
			}
		}

		private static void MassRename3()
		{
			const string baseDir = @"C:\Milkenm\Projects\Diamond\docs\static\lol\champs";
			string[] dirs = Directory.GetDirectories(baseDir);

			foreach (string dir in dirs)
			{
				string[] files = Directory.GetFiles(dir + @"\spells");

				foreach (string file in files)
				{
					string fileName = Path.GetFileName(file);

					File.Move(file, dir + @"\spells\" + fileName.ToLower() + "!");
					File.Move(dir + @"\spells\" + fileName.ToLower() + "!", dir + @"\spells\" + fileName.ToLower());

					Console.WriteLine(fileName);
				}
			}
		}
	}
}
