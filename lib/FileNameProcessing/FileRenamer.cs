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
        public void Rename(System.Windows.Controls.Label actionLabel, FileInfo file, string finalName)
        {
            try
            {
                finalName = Path.ChangeExtension(finalName, null);
                string newPath = Path.Combine(file.DirectoryName!, finalName + file.Extension);
                if (!File.Exists(newPath))
                {
                    File.Move(file.FullName, newPath);
                }
                actionLabel.Content = "Renaming complete!";
            }
            catch (Exception ex)
            {
                actionLabel.Content = $"Failed to rename file(s) in folder: {ex.Message}";
            }
        }
    }
}
