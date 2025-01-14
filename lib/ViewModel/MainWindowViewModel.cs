using Microsoft.Win32;
using RandomNamesWithUI.lib.Constants;
using RandomNamesWithUI.lib.FileNameProcessing;
using RandomNamesWithUI.lib.interfaces;
using RandomNamesWithUI.lib.models.Dialogs;
using RandomNamesWithUI.lib.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RandomNamesWithUI.lib.ViewModel
{
    public class MainWindowViewModel
    {
        readonly FileRenamer fileRenamer = new();
        public ICommand GenerativelyNameCommand { get; }
        public ICommand ManuallyNameCommand { get; }
        public ICommand OpenInfoCommand { get; }

        private string _actionLabel;
        public string ActionLabel
        {
            get => _actionLabel;
            set
            {
                _actionLabel = value;
                OnPropertyChanged(nameof(ActionLabel));
            }
        }
        private string _selectedOption;
        public string SelectedOption
        {
            get => _selectedOption;
            set
            {
                _selectedOption = value;
                OnPropertyChanged(nameof(SelectedOption));
            }
        }

        public MainWindowViewModel()
        {
            ActionLabel = "Choose action";

            GenerativelyNameCommand = new RelayCommand(async _ => await GenerativelyRenameHandler());
            ManuallyNameCommand = new RelayCommand(async _ => await ManualRenameHandler());
            OpenInfoCommand = new RelayCommand(_ => ShowAppInfo());
        }

        async Task GenerativelyRenameHandler()
        {
            if (string.IsNullOrEmpty(SelectedOption))
            {
                ActionLabel = "Please select an option from the list!";
                return;
            }
            //var selectedOption = RenamingTarget.Text;
            switch (SelectedOption)
            {
                case "Select folder(s)":
                    OpenSpecificDialog<FolderDialogAdapter>(out var dialogFolder, out bool? resultFolder);
                    if (resultFolder == true)
                    {
                        DirectoryInfo d = new(dialogFolder.FolderName);
                        foreach (var file in d.GetFiles())
                        {
                            string finalName = Path.GetRandomFileName();
                            fileRenamer.Rename(ActionLabel, file, finalName);
                        }
                        ActionLabel = "Renaming complete!";
                    }
                    break;
                case "Select file(s)":
                    OpenSpecificDialog<FileDialogAdapter>(out var dialogFile, out bool? resultFile);
                    if (resultFile == true)
                    {
                        foreach (string filePath in dialogFile.FileNames)
                        {
                            FileInfo file = new(filePath);
                            string finalName = Path.GetRandomFileName();
                            fileRenamer.Rename(ActionLabel, file, finalName);
                        }
                        ActionLabel = "Renaming complete!";
                    }
                    break;
                default:
                    ActionLabel = "Select what you want to rename from list!";
                    break;
            }

            await Task.Delay(5000);
            ActionLabel = "Choose action";
        }

        async Task ManualRenameHandler()
        {
            if (string.IsNullOrEmpty(SelectedOption))
            {
                ActionLabel = "Please select an option from the list!";
                return;
            }
            SelectName EnterName()
            {
                SelectName selectName = new();
                selectName.ShowDialog();
                return selectName;
            }

            //var selectedOption = RenamingTarget.Text;
            SelectName selectName = EnterName();

            switch (SelectedOption)
            {
                case "Select folder(s)":
                    OpenSpecificDialog<FolderDialogAdapter>(out var dialogFolder, out bool? resultFolder);
                    if (resultFolder == true)
                    {
                        DirectoryInfo d = new(dialogFolder.FolderName);
                        int i = 0;
                        foreach (var file in d.GetFiles())
                        {
                            string finalName = selectName.NewName + "_" + i;
                            fileRenamer.Rename(ActionLabel, file, finalName);
                            i++;
                        }
                        ActionLabel = "Renaming complete!";
                    }
                    break;
                case "Select file(s)":
                    OpenSpecificDialog<FileDialogAdapter>(out var dialogFile, out bool? resultFile);
                    if (resultFile == true)
                    {
                        int i = 0;
                        foreach (var filePath in dialogFile.FileNames)
                        {
                            FileInfo file = new(filePath);
                            {
                                string finalName = selectName.NewName + "_" + i;
                                fileRenamer.Rename(ActionLabel, file, finalName);
                            }
                            i++;
                        }
                        ActionLabel = "Renaming complete!";
                    }
                    break;
                default:
                    ActionLabel = "Select what you want to rename from list!";
                    break;
            }

            await Task.Delay(5000);
            ActionLabel = "Choose action";
        }

        static void ShowAppInfo()
        {
            MessageBox.Show(Instruction.messageBoxText,
                Instruction.caption,
                MessageBoxButton.OK,
                MessageBoxImage.Asterisk);
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
