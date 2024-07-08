using Microsoft.Win32;
using System.IO;
using System.Windows;
using Path = System.IO.Path;

namespace RandomNamesWithUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();

        private async void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            if (RenamingTarget.Text is "Select folder(s)")
            {
                OpenDialog(out OpenFolderDialog dialog, out bool? result);

                if (result == true)
                {
                    DirectoryInfo d = new(dialog.FolderName);
                    foreach (var file in d.GetFiles())
                    {
                        string finalName = Path.GetRandomFileName();
                        ManuallyRename(file, finalName);
                    }
                }
            }
            else if (RenamingTarget.Text is "Select file(s)")
            {
                OpenDialog(out OpenFileDialog dialog, out bool? result);

                if (result == true)
                {
                    foreach (string filePath in dialog.FileNames)
                    {
                        FileInfo file = new(filePath);
                        {
                            string finalName = Path.GetRandomFileName();
                            ManuallyRename(file, finalName);
                        }
                    }
                }
            }
            else
            {
                testLabel.Content = "Select what you want to rename from list!";
                await ReturnToDefaultLabelText();
            }
        }

        private async void ManuallyButton_Click(object sender, RoutedEventArgs e)
        {
            SelectName selectName = new();
            Hide();
            selectName.ShowDialog();
            Show();

            if (RenamingTarget.Text is "Select folder(s)")
            {
                OpenDialog(out OpenFolderDialog dialog, out bool? result);

                if (result == true)
                {
                    DirectoryInfo d = new(dialog.FolderName);
                    int i = 0;
                    foreach (var file in d.GetFiles())
                    {
                        string finalName = selectName.NewName + "_" + i;
                        ManuallyRename(file, finalName);
                        i++;
                    }
                }
            }
            else if (RenamingTarget.Text is "Select file(s)")
            {
                OpenDialog(out OpenFileDialog dialog, out bool? result);

                if (result == true)
                {
                    int i = 0;
                    foreach (var filePath in dialog.FileNames)
                    {
                        FileInfo file = new(filePath);
                        {
                            string finalName = selectName.NewName + "_" + i;
                            ManuallyRename(file, finalName);
                        }
                        i++;
                    }
                }
            }
            else
            {
                testLabel.Content = "Select what you want to rename from list!";
                await ReturnToDefaultLabelText();
            }

        }

        private static void OpenDialog(out OpenFileDialog dialog, out bool? result)
        {
            dialog = new()
            {
                Multiselect = true,
                Title = "Select file(s) to rename"
            };
            result = dialog.ShowDialog();
        }

        private static void OpenDialog(out OpenFolderDialog dialog, out bool? result)
        {
            dialog = new()
            {
                Multiselect = true,
                Title = "Select folder(s) to rename"
            };
            result = dialog.ShowDialog();
        }

        private async void ManuallyRename(FileInfo file, string finalName)
        {
            try
            {
                finalName = Path.ChangeExtension(finalName, null);
                string newPath = Path.Combine(file.DirectoryName!, finalName + file.Extension);
                if (!File.Exists(newPath))
                {
                    File.Move(file.FullName, newPath);
                }
                testLabel.Content = "Renaming complete!";
                await ReturnToDefaultLabelText();
            }
            catch (Exception ex)
            {
                testLabel.Content = $"Failed to rename file(s) in folder: {ex.Message}";
            }
        }

        private async Task ReturnToDefaultLabelText()
        {
            await Task.Delay(5000);
            testLabel.Content = "Choose action";
        }
    }
}