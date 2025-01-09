using Microsoft.Win32;
using RandomNamesWithUI.lib.interfaces;
using RandomNamesWithUI.lib.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace RandomNamesWithUI.lib.ViewModel
{
    static class MainWindowViewModel
    {
        public static void OpenSpecificDialog<Tdialog>(out Tdialog dialog, out bool? result) where Tdialog : IConfigurableDialog, new()
        {
            dialog = new Tdialog
            {
                Multiselect = true,
                Title = "Select file(s) to rename",
            };

            result = dialog.ShowDialog();
        }

        public static async Task ReturnToDefaultLabelText(System.Windows.Controls.Label testLabel)
        {
            await Task.Delay(5000);
            testLabel.Content = "Choose action";
        }
    }
}
