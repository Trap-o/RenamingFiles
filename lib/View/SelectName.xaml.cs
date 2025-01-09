using RandomNamesWithUI.lib.FileNameProcessing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace RandomNamesWithUI.lib.View
{
    /// <summary>
    /// Interaction logic for SelectName.xaml
    /// </summary>
    public partial class SelectName : Window
    {
        public SelectName() => InitializeComponent();
        readonly FilenameValidator filenameValidator = new();

        public string? NewName { get; set; }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            if (NewFileName.Text is not "")
            {
                NewName = NewFileName.Text;
            }
            else
                NewName = "File";
            Close();
        }

        private void RenamingPart_TextChanged(object sender, TextChangedEventArgs e)
        {
            string showedTestName = filenameValidator.ValidateName(NewFileName);
            TestNameList.Text = "New files' names:\n" + showedTestName + "...";
        }
    }
}
