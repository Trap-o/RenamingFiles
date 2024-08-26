using Microsoft.Win32;
using System.IO;
using System.Windows;
using File = System.IO.File;
using Path = System.IO.Path;

namespace RandomNamesWithUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();

        private static void OpenSpecificDialog<Tdialog>(out Tdialog dialog, out bool? result) where Tdialog : CommonDialog, new()
        {
            dialog = new Tdialog();

            if (dialog is OpenFileDialog fileDialog)
            {
                fileDialog.Multiselect = true;
                fileDialog.Title = "Select file(s) to rename";
            }
            if (dialog is OpenFolderDialog folderDialog)
            {
                folderDialog.Multiselect = true;
                folderDialog.Title = "Select file(s) to rename";
            };
            result = dialog.ShowDialog();
        }

        private async void Rename(FileInfo file, string finalName)
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

        private async void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            if (RenamingTarget.Text is "Select folder(s)")
            {
                OpenSpecificDialog(out OpenFolderDialog dialog, out bool? result);

                if (result == true)
                {
                    DirectoryInfo d = new(dialog.FolderName);
                    foreach (var file in d.GetFiles())
                    {
                        string finalName = Path.GetRandomFileName();
                        Rename(file, finalName);
                    }
                }
            }
            else if (RenamingTarget.Text is "Select file(s)")
            {
                OpenSpecificDialog(out OpenFileDialog dialog, out bool? result);

                if (result == true)
                {
                    foreach (string filePath in dialog.FileNames)
                    {
                        FileInfo file = new(filePath);
                        string finalName = Path.GetRandomFileName();
                        Rename(file, finalName);
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
            SelectName EnterName()
            {
                SelectName selectName = new();
                Hide();
                selectName.ShowDialog();
                if (selectName.RenamingPart.Text == "")
                    selectName.NewName = "File";
                Show();
                return selectName;
            }

            if (RenamingTarget.Text is "Select folder(s)")
            {
                SelectName selectName = EnterName();
                OpenSpecificDialog(out OpenFolderDialog dialog, out bool? result);

                if (result == true)
                {
                    DirectoryInfo d = new(dialog.FolderName);
                    int i = 0;
                    foreach (var file in d.GetFiles())
                    {
                        string finalName = selectName.NewName + "_" + i;
                        Rename(file, finalName);
                        i++;
                    }
                }
            }
            else if (RenamingTarget.Text is "Select file(s)")
            {
                SelectName selectName = EnterName();
                OpenSpecificDialog(out OpenFileDialog dialog, out bool? result);

                if (result == true)
                {
                    int i = 0;
                    foreach (var filePath in dialog.FileNames)
                    {
                        FileInfo file = new(filePath);
                        {
                            string finalName = selectName.NewName + "_" + i;
                            Rename(file, finalName);
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

        private async Task ReturnToDefaultLabelText()
        {
            await Task.Delay(5000);
            testLabel.Content = "Choose action";
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            string caption = "Info";
            string messageBoxText = "First, You need to select the object to rename form drop - down list: " +
                "\"Select folder(s)\" – select one or more folders and rename ALL files in the folder(s); " +
                "\"Select file(s)\" – select one or more files in specific directory and rename ONLY THE SELECTED file(s). " +
                "If You don't do this, the message \"Select what you want to rename from list!\" displayes. " +
                "\n\nAfter that, You choose the type of renaming: " +
                "generate random name(s) for file(s) with certain letters and numbers or give them similar specific name(s), " +
                "that You enter yourself(like \"photo_0, photo_1...\", etc.). " +
                "\nFor the first variant, You just need to click the \"Generate name\" button and then select folder(s) or file(s) to rename " +
                "(depending on what you select in drop - down list)." +
                "\nFor the second variant, firstly, You need to click the \"Change name manually\" button, " +
                "secondly, enter a new name for file(s) and click the \"Continue\" button, thirdly, select folder(s) or file(s) to rename " +
                "(depending on what you select in drop - down list). Finally, You can see results of files\' renaming." +
                "\n\nThe app was developed by Trap_o out of personal need (and as a small portfolio project). " +
                "Please, write any suggestions (new features, app design, bugs, code improvements, etc.) in \"Discussions\" section (English or Ukrainian).";
            MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
    }
}