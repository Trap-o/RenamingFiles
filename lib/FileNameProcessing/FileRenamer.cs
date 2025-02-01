using RandomNamesWithUI.lib.interfaces;
using System.IO;

namespace RandomNamesWithUI.lib.FileNameProcessing
{
    internal class FileRenamer : IRename
    {
        public void Rename(string actionLabelText, FileInfo file, string finalName)
        {
            finalName = Path.ChangeExtension(finalName, null);
            string newPath = Path.Combine(file.DirectoryName!, finalName + file.Extension);
            if (!File.Exists(newPath))
                File.Move(file.FullName, newPath);
        }
    }
}
