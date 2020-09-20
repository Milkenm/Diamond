using Diamond.Brainz.Data;
using Diamond.Brainz.Structures;

using System;
using System.Threading.Tasks;

namespace Diamond.Brainz.Utils
{
    public static partial class Folders
    {
        public static async void CreateFolders()
        {
            await Task.Run(new Action(() =>
            {
                foreach (Folder folder in GlobalData.Folders.Values)
                {
                    folder.CreateFolder();
                }
            })).ConfigureAwait(false);
        }
    }
}