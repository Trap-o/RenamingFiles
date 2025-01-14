using RandomNamesWithUI.lib.interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomNamesWithUI.lib.FileNameProcessing
{
    internal class FileRenamer : IRename
    {
        public void Rename(string actionLabelText, FileInfo file, string finalName)
        {
            try
            {
                finalName = Path.ChangeExtension(finalName, null);
                string newPath = Path.Combine(file.DirectoryName!, finalName + file.Extension);
                if (!File.Exists(newPath))
                {
                    File.Move(file.FullName, newPath);
                }
            }
            catch (Exception ex)
            {
                 Console.WriteLine($"Failed to rename file(s) in folder: {ex.Message}");
            }
        }
    }
}
