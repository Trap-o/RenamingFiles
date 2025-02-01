using RandomNamesWithUI.lib.Constants;
using RandomNamesWithUI.lib.FileNameProcessing;
using RandomNamesWithUI.lib.interfaces;
using RandomNamesWithUI.lib.models.Dialogs;
using RandomNamesWithUI.lib.View;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace RandomNamesWithUI.lib.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Variables
        public ICommand GenerativelyNameCommand { get; }
        public ICommand ManuallyNameCommand { get; }
        public ICommand OpenInfoCommand { get; }

        readonly FileRenamer fileRenamer = new();

        private string _actionLabel = string.Empty;
        public string ActionLabel
        {
            get => _actionLabel;
            set
            {
                _actionLabel = value;
                OnPropertyChanged(nameof(ActionLabel));
                CommandManager.InvalidateRequerySuggested();
            }
        }
        private string _selectedOption = string.Empty;
        public string SelectedOption
        {
            get => _selectedOption;
            set
            {
                _selectedOption = value;
                OnPropertyChanged(nameof(SelectedOption));
                CommandManager.InvalidateRequerySuggested();
            }
        }

        private SelectNameViewModel? selectName;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Constructor
        public MainWindowViewModel()
        {
            ActionLabel = "Choose action";

            GenerativelyNameCommand = new RelayCommand(async _ => await GenerativelyRenameHandlerAsync());
            ManuallyNameCommand = new RelayCommand(async _ => await ManualRenameHandlerAsync());
            OpenInfoCommand = new RelayCommand(_ => ShowAppInfo());
            CommandManager.InvalidateRequerySuggested();
        }
        #endregion

        #region Commands
        private async Task GenerativelyRenameHandlerAsync() => await RenameHandlerAsync(true);

        private async Task ManualRenameHandlerAsync() => await RenameHandlerAsync(false);

        private static Task ShowAppInfo()
        {
            MessageBox.Show(InstructionText.messageBoxText,
                InstructionText.caption,
                MessageBoxButton.OK,
                MessageBoxImage.Asterisk);
            return Task.CompletedTask;
        }
        #endregion

        #region CommandsMethods
        private async Task RenameHandlerAsync(bool isAutoRename)
        {
            try
            {
                ValidateSelectedOption();

                switch (SelectedOption)
                {
                    case "Select folder(s)":
                        HandleRenameProcess<FolderDialogAdapter>(isAutoRename);
                        break;
                    case "Select file(s)":
                        HandleRenameProcess<FileDialogAdapter>(isAutoRename);
                        break;
                }
                await ResetActionLabel();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void HandleRenameProcess<T>(bool isGenerativelyRename) where T : IConfigurableDialog, new()
        {
            selectName = isGenerativelyRename ? null : OpenSelectNameWindowViewModel();

            OpenSpecificDialog<T>(out var dialog, out bool? result);

            if (typeof(T) == typeof(FileDialogAdapter))
                HandleFileRename(dialog as FileDialogAdapter, result, isGenerativelyRename, selectName?.NewName);
            else
                HandleFolderFilesRename(dialog as FolderDialogAdapter, result, isGenerativelyRename, selectName?.NewName);
        }

        private static SelectNameViewModel? OpenSelectNameWindowViewModel()
        {
            SelectName selectName = new();
            selectName.ShowDialog();
            return selectName.DataContext as SelectNameViewModel;
        }

        private void HandleFileRename(FileDialogAdapter? dialogFile, bool? resultFile, bool isGenerativelyRename, string? newName)
        {
            if (resultFile == true && dialogFile != null)
            {
                int i = 0;
                string finalName;

                foreach (var filePath in dialogFile.FileNames)
                {
                    FileInfo file = new(filePath);
                    finalName = CreateFinalName(isGenerativelyRename, newName, i);
                    fileRenamer.Rename(ActionLabel, file, finalName);
                    i++;
                }
                ActionLabel = "Renaming complete!";
            }
        }

        private void HandleFolderFilesRename(FolderDialogAdapter? dialogFolder, bool? resultFolder, bool isGenerativelyRename, string? newName)
        {
            if (resultFolder == true && dialogFolder != null)
            {
                DirectoryInfo d = new(dialogFolder.FolderName);
                int i = 0;
                string finalName;

                foreach (var file in d.GetFiles())
                {
                    finalName = CreateFinalName(isGenerativelyRename, newName, i);
                    fileRenamer.Rename(ActionLabel, file, finalName);
                    i++;
                }
                ActionLabel = "Renaming complete!";
            }
        }

        private static string CreateFinalName(bool isGenerativelyRename, string? newName, int i)
        {
            string finalName;
            if (isGenerativelyRename)
                finalName = Path.GetRandomFileName();
            else
                finalName = $"{newName}_{i}";

            return finalName;
        }

        private async Task ResetActionLabel()
        {
            await Task.Delay(5000);
            ActionLabel = "Choose action";
        }

        private void ValidateSelectedOption()
        {
            if (string.IsNullOrEmpty(SelectedOption))
                ActionLabel = "Please select an option from the list!";
        }

        public static void OpenSpecificDialog<Tdialog>(out Tdialog dialog, out bool? result) where Tdialog : IConfigurableDialog, new()
        {
            dialog = new Tdialog
            {
                Multiselect = true,
                Title = "Select file(s) to rename",
            };

            result = dialog.ShowDialog();
        }
        #endregion
    }
}
