using RandomNamesWithUI.lib.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RandomNamesWithUI.lib.FileNameProcessing
{
    internal class FilenameValidator : IValidateName
    {
        public string ValidateName(TextBox newName)
        {
            string bannedSymbols = @"[\/\\:\*\?""\<\>\|]"; // filename cannot contain these symbols
            string testName; // showed to user as result of renaming

            if (newName.Text is not "")
            {
                if (Regex.IsMatch(newName.Text, bannedSymbols))
                {
                    MessageBox.Show("A filename cannot contain any of the following characters: \\ / : * ? \" < > |", "Error!",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    newName.Text = newName.Text[..^1];
                }
                if (newName.Text.Length > 15)
                {
                    MessageBox.Show("The name must contain less than 15 characters!", "Error!",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    newName.Text = newName.Text[..^1];
                }

                StringBuilder stringBuilder = new();
                for (int i = 0; i < 3; i++)
                {
                    stringBuilder.Append($"{newName.Text}_{i},\n");
                }

                testName = stringBuilder.ToString();
            }
            else
            {
                testName = "File_1,\nFile_2,\nFile_3, ";
            }

            return testName;
        }
    }
}
