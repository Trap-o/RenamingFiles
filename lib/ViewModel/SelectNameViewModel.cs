using RandomNamesWithUI.lib.FileNameProcessing;
using RandomNamesWithUI.lib.View;
using System.ComponentModel;
using System.Windows.Input;

namespace RandomNamesWithUI.lib.ViewModel
{
    public class SelectNameViewModel : INotifyPropertyChanged
    {
        #region Variables
        public ICommand SetNewFileNameAndCloseCommand { get; }

        private string _newFileNameTextBox = string.Empty;
        public string NewFileNameTextBox
        {
            get => _newFileNameTextBox;
            set
            {
                _newFileNameTextBox = value;
                UpdateFileNameTextBox();
                OnPropertyChanged(nameof(TestNameList));
                CommandManager.InvalidateRequerySuggested();
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
                CommandManager.InvalidateRequerySuggested();
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
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Constructor
        public SelectNameViewModel()
        {
            TestNameList = $"New files' names:{Environment.NewLine}File_0," +
                $"{Environment.NewLine}File_1,{Environment.NewLine}File_2, ...";

            SetNewFileNameAndCloseCommand = new RelayCommand(SetNewFileName);
            CommandManager.InvalidateRequerySuggested();
        }
        #endregion

        #region Commands
        Task SetNewFileName(object? window)
        {
            if (!string.IsNullOrWhiteSpace(NewFileNameTextBox))
                NewName = NewFileNameTextBox;
            else
                NewName = "File";

            if(window is SelectName selectName)
                selectName.Close();
            return Task.CompletedTask;
        }
        #endregion

        #region CommandsMethods
        public void OnWindowClosing(object? sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NewName))
                NewName = "File";
        }

        private void UpdateFileNameTextBox()
        {
            var validatedName = FilenameValidator.ValidateName(_newFileNameTextBox);
            _newFileNameTextBox = validatedName;
            OnPropertyChanged(nameof(NewFileNameTextBox));
            if (string.IsNullOrEmpty(_newFileNameTextBox))
                validatedName = "File";

            TestNameList = $"New files' names:{Environment.NewLine}" +
                $"{FilenameValidator.GenerateListForTestFileNames(validatedName)}...";
        }
        #endregion
    }
}
