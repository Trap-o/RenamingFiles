using Microsoft.Win32;
using RandomNamesWithUI.lib.interfaces;

namespace RandomNamesWithUI.lib.models.Dialogs
{
    public class FileDialogAdapter : IConfigurableDialog
    {
        private readonly OpenFileDialog _openFileDialog = new();
        public bool Multiselect
        {
            get => _openFileDialog.Multiselect;
            set => _openFileDialog.Multiselect = value;
        }

        public string Title
        {
            get => _openFileDialog.Title;
            set => _openFileDialog.Title = value;
        }

        public string[] FileNames { get => _openFileDialog.FileNames; }

        public bool? ShowDialog()
        {
            return _openFileDialog.ShowDialog();
        }
    }
}
