using System.IO;

namespace RandomNamesWithUI.lib.interfaces
{
    internal interface IRename
    {
        protected void Rename(string actionLabelText, FileInfo file, string finalName);
    }
}
