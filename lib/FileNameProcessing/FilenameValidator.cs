using RandomNamesWithUI.lib.interfaces;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace RandomNamesWithUI.lib.FileNameProcessing
{
    internal class FilenameValidator : IValidateName
    {
        public string ValidateName(TextBox newName)
        {
            const string bannedSymbols = @"[\/\\:\*\?""\<\>\|]";
            string testName;

            if (newName.Text is not "")
            {
                CheckForBannedSymbols(newName, bannedSymbols);
                CheckNameLength(newName);
                testName = GenerateListForTestFileNames(newName);
            }
            else
                testName = "File_1,\nFile_2,\nFile_3, ";

            return testName;
        }

        private static string GenerateListForTestFileNames(TextBox newName)
        {
            string testName;
            StringBuilder stringBuilder = new();

            for (int i = 0; i < 3; i++)
            {
                stringBuilder.Append($"{newName.Text}_{i},\n");
            }

            testName = stringBuilder.ToString();
            return testName;
        }

        private static void CheckNameLength(TextBox newName)
        {
            if (newName.Text.Length > 15)
            {
                MessageBox.Show("The name must contain less than 15 characters!", "Error!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                newName.Text = newName.Text[..^1];
            }
        }

        private static void CheckForBannedSymbols(TextBox newName, string bannedSymbols)
        {
            if (Regex.IsMatch(newName.Text, bannedSymbols))
            {
                MessageBox.Show("A filename cannot contain any of the following characters: \\ / : * ? \" < > |", "Error!",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                newName.Text = newName.Text[..^1];
            }
        }
    }
}
