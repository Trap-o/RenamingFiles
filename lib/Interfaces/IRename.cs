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
        protected void Rename(System.Windows.Controls.Label actionLabel, FileInfo file, string finalName);
    }
}
