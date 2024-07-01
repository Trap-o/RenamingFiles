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
        public MainWindow() => InitializeComponent();

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
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
            if (RenamingTarget.Text is "Select file(s)")
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
                testLabel.Content = "Оберіть ціль перейменування у списку!";
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

        private void ManuallyButton_Click(object sender, RoutedEventArgs e)
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
            if (RenamingTarget.Text is "Select file(s)")
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
            }
        }

        private void ManuallyRename(FileInfo file, string finalName)
        {
            try
            {
                finalName = Path.ChangeExtension(finalName, null);
                string newPath = Path.Combine(file.DirectoryName!, finalName + file.Extension);
                if (!File.Exists(newPath))
                {
                    File.Move(file.FullName, newPath);
                }
            }
            catch (Exception ex)
            {
                testLabel.Content = $"Failed to rename file(s) in folder: {ex.Message}";
            }
        }
    }
}