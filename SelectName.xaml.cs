using System.Windows;
using System.Windows.Controls;

namespace RandomNamesWithUI
{
    /// <summary>
    /// Interaction logic for SelectName.xaml
    /// </summary>
    public partial class SelectName : Window
    {
        public SelectName()
        {
            InitializeComponent();
        }

        public string NewName { get; set; }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            if (RenamingPart.Text is not "")
                NewName = RenamingPart.Text;
            else
                NewName = "File";
            Close();
        }

        private void RenamingPart_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (RenamingPart.Text is "")
                TestNameList.Content = "New file's names:";
            string testName = "";
            Thread.Sleep(2000);
            for (int i = 0; i < 3; i++)
            {
                testName = '\n' + RenamingPart.Text + i;
            }
            TestNameList.Content += testName;
        }
    }
}
