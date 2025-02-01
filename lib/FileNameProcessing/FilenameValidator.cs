using RandomNamesWithUI.lib.interfaces;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace RandomNamesWithUI.lib.FileNameProcessing
{
    internal class FilenameValidator : IValidateName
    {
        public static string ValidateName(string newName)
        {
            const string bannedSymbols = @"[\/\\:\*\?""\<\>\|]";

            if (!IsContainBannedSymbols(newName, bannedSymbols) || !IsNameLengthCorrect(newName))
                newName = newName[..^1];

            return newName;
        }

        public static string GenerateListForTestFileNames(string newName)
        {
            StringBuilder stringBuilder = new();
            int numberOfTestNames = 3;

            for (int i = 0; i < numberOfTestNames; i++)
            {
                stringBuilder.Append($"{newName}_{i},{Environment.NewLine}");
            }

            return stringBuilder.ToString();
        }

        private static bool IsNameLengthCorrect(string newName)
        {
            if (newName.Length > 15)
            {
                MessageBox.Show("The name must contain less than 15 characters!", "Error!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private static bool IsContainBannedSymbols(string newName, string bannedSymbols)
        {
            if (Regex.IsMatch(newName, bannedSymbols))
            {
                MessageBox.Show("A filename cannot contain any of the following characters: \\ / : * ? \" < > |",
                    "Error!",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return false;
            }
            return true;
        }
    }
}
