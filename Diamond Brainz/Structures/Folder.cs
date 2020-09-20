using System.IO;

namespace Diamond.Brainz.Structures
{
    public class Folder
    {
        public Folder(string path)
        {
            Path = path;
        }

        public string Path { get; }

        public bool Exists
        {
            get
            {
                return Directory.Exists(Path);
            }
        }

        public string CreateFolder()
        {
            if (!Exists)
            {
                Directory.CreateDirectory(Path);
            }

            return Path;
        }
    }
}