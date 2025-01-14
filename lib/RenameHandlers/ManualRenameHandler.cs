//using RandomNamesWithUI.lib.models.Dialogs;
//using RandomNamesWithUI.lib.View;
//using RandomNamesWithUI.lib.ViewModel;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RandomNamesWithUI.lib.RenameHandlers
//{
//    public static class ManualRenameHandler
//    {
//        static async Task ManualRenameHandlerAsync()
//        {
//            SelectName EnterName()
//            {
//                SelectName selectName = new();
//                Hide();
//                selectName.ShowDialog();
//                Show();
//                return selectName;
//            }

//            var selectedOption = RenamingTarget.Text;
//            SelectName selectName = EnterName();

//            switch (selectedOption)
//            {
//                case "Select folder(s)":
//                    MainWindowViewModel.OpenSpecificDialog<FolderDialogAdapter>(out var dialogFolder, out bool? resultFolder);
//                    if (resultFolder == true)
//                    {
//                        DirectoryInfo d = new(dialogFolder.FolderName);
//                        int i = 0;
//                        foreach (var file in d.GetFiles())
//                        {
//                            string finalName = selectName.NewName + "_" + i;
//                            fileRenamer.Rename(actionLabel, file, finalName);
//                            i++;
//                        }
//                    }
//                    break;
//                case "Select file(s)":
//                    MainWindowViewModel.OpenSpecificDialog<FileDialogAdapter>(out var dialogFile, out bool? resultFile);
//                    if (resultFile == true)
//                    {
//                        int i = 0;
//                        foreach (var filePath in dialogFile.FileNames)
//                        {
//                            FileInfo file = new(filePath);
//                            {
//                                string finalName = selectName.NewName + "_" + i;
//                                fileRenamer.Rename(actionLabel, file, finalName);
//                            }
//                            i++;
//                        }
//                    }
//                    break;
//                default:
//                    actionLabel.Content = "Select what you want to rename from list!";
//                    break;
//            }
//            await MainWindowViewModel.ReturnToDefaultLabelText(actionLabel);
//        }
//    }
//}
