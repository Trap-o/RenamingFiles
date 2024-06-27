using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Path = System.IO.Path;

namespace RandomNamesWithUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            if (RenamingTarget.Text is "Select folder(s)")
            {
                OpenDialogForFolders(out OpenFolderDialog dialog, out bool? result);

                if (result == true)
                {
                    DirectoryInfo d = new(dialog.FolderName);
                    foreach (var file in d.GetFiles())
                    {
                        string finalName = Path.GetRandomFileName();
                        try
                        {
                            GenerateRenaming(file, finalName);
                        }
                        catch (Exception ex)
                        {
                            testLabel.Content = $"Failed to rename files in folder: {ex.Message}";
                        }
                    }
                }
            }
            if (RenamingTarget.Text is "Select file(s)")
            {
                OpenDialogForFiles(out OpenFileDialog dialog, out bool? result);

                if (result == true)
                {
                    foreach (var filePath in dialog.FileNames)
                    {
                        FileInfo file = new(filePath);
                        {
                            string finalName = Path.GetRandomFileName();
                            try
                            {
                                GenerateRenaming(file, finalName);
                            }
                            catch (Exception ex)
                            {
                                testLabel.Content = $"Failed to rename file(s) in folder: {ex.Message}";
                            }
                        }
                    }
                }
            }
            else
            {
                testLabel.Content = "Оберіть ціль перейменування у списку!";
            }
        }

        private static void OpenDialogForFiles(out OpenFileDialog dialog, out bool? result)
        {
            dialog = new()
            {
                Multiselect = true,
                Title = "Select file(s) to rename"
            };
            result = dialog.ShowDialog();
        }

        private static void OpenDialogForFolders(out OpenFolderDialog dialog, out bool? result)
        {
            dialog = new()
            {
                Multiselect = true,
                Title = "Select folder(s) to rename"
            };
            result = dialog.ShowDialog();
        }

        private void ManuallyButton_Click(object sender, RoutedEventArgs e)
        {
            SelectName selectName = new();
            selectName.ShowDialog();

            if (RenamingTarget.Text is "Select folder(s)")
            {
                OpenDialogForFolders(out OpenFolderDialog dialog, out bool? result);

                if (result == true)
                {
                    DirectoryInfo d = new(dialog.FolderName);
                    int i = 0;
                    foreach (var file in d.GetFiles())
                    {
                        ManuallyRename(selectName, i, file);
                        i++;
                    }
                }
            }
            if (RenamingTarget.Text is "Select file(s)")
            {
                OpenDialogForFiles(out OpenFileDialog dialog, out bool? result);

                if (result == true)
                {
                    int i = 0;
                    foreach (var filePath in dialog.FileNames)
                    {
                        FileInfo file = new(filePath);
                        {
                            ManuallyRename(selectName, i, file);
                        }
                        i++;
                    }
                }
            }
            else
            {
                testLabel.Content = "Select what you want to rename from list!";
            }
        }

        private void ManuallyRename(SelectName selectName, int i, FileInfo file)
        {
            string finalName = selectName.newName + "_" + i;
            try
            {
                GenerateRenaming(file, finalName);
            }
            catch (Exception ex)
            {
                testLabel.Content = $"Failed to rename file(s) in folder: {ex.Message}";
            }
        }

        private static void GenerateRenaming(FileInfo file, string finalName)
        {
            finalName = Path.ChangeExtension(finalName, null);
            string newPath = Path.Combine(file.DirectoryName, finalName + file.Extension);
            if (!File.Exists(newPath))
            {
                File.Move(file.FullName, newPath);
            }
        }
    }
}