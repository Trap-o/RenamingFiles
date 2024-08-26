using System.Windows;
using System.Windows.Controls;

namespace RandomNamesWithUI
{
    /// <summary>
    /// Interaction logic for SelectName.xaml
    /// </summary>
    public partial class SelectName : Window
    {
        public SelectName() => InitializeComponent();

        public string? NewName { get; set; }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            if (RenamingPart.Text is not "")
            {
                NewName = RenamingPart.Text;
            }
            else
                NewName = "File";
            Close();
        }

        private void RenamingPart_TextChanged(object sender, TextChangedEventArgs e)
        {
            string testName = "";
            char[] prohibitedSymbols = ['/', '\\', ':', '*', '?', '\"', '<', '>', '|'];
            for (int i = 0; i < 3; i++)
            {
                if (RenamingPart.Text is not "")
                {
                    foreach (char symbol in prohibitedSymbols)
                    {
                        if (RenamingPart.Text.Contains(symbol))
                        {
                            MessageBox.Show("A filename cannot contain any of the following characters: \\ / : * ? \" < > |", "Error!",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                            RenamingPart.Text = RenamingPart.Text[..^1];
                        }
                        else
                        {
                            if (RenamingPart.Text.Length <= 15)
                            {
                                testName += RenamingPart.Text + "_" + i + ",\n";
                            }
                            else
                            {
                                MessageBox.Show("The name must contain less than 15 characters!", "Error!",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                                RenamingPart.Text = RenamingPart.Text[..^1];
                            }
                        }
                    }
                }
                else
                    testName = "File_1,\nFile_2,\nFile_3, ";
            }
            TestNameList.Text = "New files' names:\n" + testName + "...";
        }
    }
}
