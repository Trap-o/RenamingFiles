using System.Windows.Input;

namespace RandomNamesWithUI.lib.ViewModel
{
    public class RelayCommand : ICommand
    {
        private readonly Func<object?, Task>? _executeAsync;
        private readonly Func<object?, bool>? _canExecute;
        private readonly Action<object?>? _execute;

        public RelayCommand(Action<object?>? execute, Func<object?, bool>? canExecute = null)
        {
            _executeAsync = null;
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? (_ => true);
        }

        public RelayCommand(Func<object?, Task> executeAsync, Func<object?, bool>? canExecute = null)
        {
            _executeAsync = executeAsync ?? throw new ArgumentNullException(nameof(executeAsync));
            _execute = null;
            _canExecute = canExecute ?? (_ => true);
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute(parameter ?? new object());
        }

        public async void Execute(object? parameter)
        {
            if (_executeAsync != null)
            {
                await ExecuteAsync(parameter);
            }
            else
            {
                if (parameter != null)
                    _execute?.Invoke(parameter);
            }
        }

        private async Task ExecuteAsync(object? parameter)
        {
            if (_executeAsync != null)
            {
                await _executeAsync(parameter);
            }
        }
    }
}