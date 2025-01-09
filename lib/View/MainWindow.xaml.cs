using RandomNamesWithUI.lib.Constants;
using RandomNamesWithUI.lib.FileNameProcessing;
using RandomNamesWithUI.lib.models.Dialogs;
using RandomNamesWithUI.lib.ViewModel;
using System.IO;
using System.Windows;
using Path = System.IO.Path;

namespace RandomNamesWithUI.lib.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();
        readonly FileRenamer fileRenamer = new();

        private async void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedOption = RenamingTarget.Text;
            switch (selectedOption)
            {
                case "Select folder(s)":
                    MainWindowViewModel.OpenSpecificDialog<FolderDialogAdapter>(out var dialogFolder, out bool? resultFolder);
                    if (resultFolder == true)
                    {
                        DirectoryInfo d = new(dialogFolder.FolderName);
                        foreach (var file in d.GetFiles())
                        {
                            string finalName = Path.GetRandomFileName();
                            fileRenamer.Rename(actionLabel, file, finalName);
                        }
                    }
                    break;
                case "Select file(s)":
                    MainWindowViewModel.OpenSpecificDialog<FileDialogAdapter>(out var dialogFile, out bool? resultFile);
                    if (resultFile == true)
                    {
                        foreach (string filePath in dialogFile.FileNames)
                        {
                            FileInfo file = new(filePath);
                            string finalName = Path.GetRandomFileName();
                            fileRenamer.Rename(actionLabel, file, finalName);
                        }
                    }
                    break;
                default:
                    actionLabel.Content = "Select what you want to rename from list!";
                    break;
            }
            await MainWindowViewModel.ReturnToDefaultLabelText(actionLabel);
        }

        private async void ManuallyButton_Click(object  sender, RoutedEventArgs e)
        {
            SelectName EnterName()
            {
                SelectName selectName = new();
                Hide();
                selectName.ShowDialog();
                Show();
                return selectName;
            }

            var selectedOption = RenamingTarget.Text;
            SelectName selectName = EnterName();

            switch (selectedOption)
            {
                case "Select folder(s)":
                    MainWindowViewModel.OpenSpecificDialog<FolderDialogAdapter>(out var dialogFolder, out bool? resultFolder);
                    if (resultFolder == true)
                    {
                        DirectoryInfo d = new(dialogFolder.FolderName);
                        int i = 0;
                        foreach (var file in d.GetFiles())
                        {
                            string finalName = selectName.NewName + "_" + i;
                            fileRenamer.Rename(actionLabel, file, finalName);
                            i++;
                        }
                    }
                    break;
                case "Select file(s)":
                    MainWindowViewModel.OpenSpecificDialog<FileDialogAdapter>(out var dialogFile, out bool? resultFile);
                    if (resultFile == true)
                    {
                        int i = 0;
                        foreach (var filePath in dialogFile.FileNames)
                        {
                            FileInfo file = new(filePath);
                            {
                                string finalName = selectName.NewName + "_" + i;
                                fileRenamer.Rename(actionLabel, file, finalName);
                            }
                            i++;
                        }
                    }
                    break;
                default:
                    actionLabel.Content = "Select what you want to rename from list!";
                    break;
            }
            await MainWindowViewModel.ReturnToDefaultLabelText(actionLabel);
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Instruction.messageBoxText,
                Instruction.caption,
                MessageBoxButton.OK,
                MessageBoxImage.Asterisk);
        }
    }
}