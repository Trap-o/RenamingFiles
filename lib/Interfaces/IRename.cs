using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomNamesWithUI.lib.interfaces
{
    internal interface IRename
    {
        protected void Rename(string actionLabelText, FileInfo file, string finalName);
    }
}
