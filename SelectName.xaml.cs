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

        public string? NewName { get; set; }

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
            string testName = "";
            for (int i = 0; i < 3; i++)
                testName += RenamingPart.Text + "_" + i + ", ";
            TestNameList.Text = "New files' names:\n" + testName + "...";
        }
    }
}
