using Microsoft.Win32;
using RandomNamesWithUI.lib.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomNamesWithUI.lib.models.Dialogs
{
    public class FolderDialogAdapter : IConfigurableDialog
    {
        private readonly OpenFolderDialog _openFolderDialog = new();
        public bool Multiselect
        {
            get => _openFolderDialog.Multiselect;
            set => _openFolderDialog.Multiselect = value;
        }

        public string Title
        {
            get => _openFolderDialog.Title;
            set => _openFolderDialog.Title = value;
        }

        public string FolderName { get => _openFolderDialog.FolderName; }

        public bool? ShowDialog()
        {
            return _openFolderDialog.ShowDialog();
        }
    }
}
