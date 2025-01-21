using RandomNamesWithUI.lib.FileNameProcessing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RandomNamesWithUI.lib.ViewModel
{
    public class SelectNameViewModel : INotifyPropertyChanged
    {
        public ICommand SetNewFileNameAndCloseCommand { get; }

        readonly FilenameValidator filenameValidator = new();

        private string _newFileName = string.Empty;
        public string NewFileName
        {
            get => _newFileName;
            set
            {
                _newFileName = value;
                OnPropertyChanged(nameof(NewFileName));
            }
        }
        private string _newName = string.Empty;
        public string NewName
        {
            get => _newName;
            set
            {
                _newName = value;
                OnPropertyChanged(nameof(NewName));
            }
        }
        private string _testNameList = string.Empty;
        public string TestNameList
        {
            get => _testNameList;
            set 
            {
                _testNameList = value;
                OnPropertyChanged(nameof(TestNameList));
            }
        }

        public SelectNameViewModel()
        {
            TestNameList = $"New files' names:{Environment.NewLine}File_1," +
                $"{Environment.NewLine}File_2,{Environment.NewLine}File_3, ...";

            SetNewFileNameAndCloseCommand = new RelayCommand(_ => SetNewFileNameAndCloseWindow());

        }

        Task SetNewFileNameAndCloseWindow()
        {
            if (string.IsNullOrWhiteSpace(NewFileName))
            {
                NewName = NewFileName;
            }
            else
                NewName = "File";

            return Task.CompletedTask;
            //Close();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
