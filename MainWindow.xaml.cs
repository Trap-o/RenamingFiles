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
using System.Windows.Shapes;
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
                OpenFolderDialog dialog = new()
                {
                    Multiselect = true,
                    Title = "Select folder(s) to rename"
                };

                bool? result = dialog.ShowDialog();

                if (result == true)
                {
                    DirectoryInfo d = new DirectoryInfo(dialog.FolderName);
                    foreach (var file in d.GetFiles())
                    {
                        string finalName = Path.GetRandomFileName();
                        GenerateRenaming(file, finalName);
                    }
                }
            }
            if (RenamingTarget.Text is "Select file(s)")
            {
                OpenFileDialog dialog = new()
                {
                    Multiselect = true,
                    Title = "Select file(s) to rename"
                };

                bool? result = dialog.ShowDialog();

                if (result == true)
                {
                    foreach (var filePath in dialog.FileNames)
                    {
                        FileInfo file = new FileInfo(filePath);
                        {
                            string finalName = Path.GetRandomFileName();
                            GenerateRenaming(file, finalName);
                        }
                    }
                }
            }
            else
            {
                testLabel.Content = "Оберіть ціль перейменування у списку!";
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

        private void ManuallyButton_Click(object sender, RoutedEventArgs e)
        {
            SelectName selectName = new SelectName();
            selectName.ShowDialog();

            if (RenamingTarget.Text is "Select folder(s)")
            {
                OpenFolderDialog dialog = new()
                {
                    Multiselect = true,
                    Title = "Select folder(s) to rename"
                };

                bool? result = dialog.ShowDialog();

                if (result == true)
                {
                    DirectoryInfo d = new DirectoryInfo(dialog.FolderName);
                    int i = 0;
                    foreach (var file in d.GetFiles())
                    {
                        string finalName = selectName.newName + "_" + i;
                        GenerateRenaming(file, finalName);
                        i++;
                    }
                }
            }
            if (RenamingTarget.Text is "Select file(s)")
            {
                OpenFileDialog dialog = new()
                {
                    Multiselect = true,
                    Title = "Select file(s) to rename"
                };

                bool? result = dialog.ShowDialog();

                if (result == true)
                {
                    int i = 0;
                    foreach (var filePath in dialog.FileNames)
                    {
                        FileInfo file = new FileInfo(filePath);
                        {
                            string finalName = selectName.newName + "_" + i;
                            GenerateRenaming(file, finalName);
                        }
                        i++;
                    }
                }
            }
            else
            {
                testLabel.Content = "Оберіть ціль перейменування у списку!";
            }
        }
    }
}