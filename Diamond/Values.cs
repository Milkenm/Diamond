#region Usings
using System.IO;
using static ScriptsLib.Tools;
#endregion Usings



namespace Diamond
{
    internal static class Values
    {
        internal static readonly string ValuesFilePath = @"C:\Users\Miguel Campos\Desktop\GitHub\Diamond\_Random\Values.txt";

        internal static string BotToken
        {
            get
            {
                return File.ReadAllLines(ValuesFilePath)[0];
            }
            set { }
        }

        internal static string 
    }
}
