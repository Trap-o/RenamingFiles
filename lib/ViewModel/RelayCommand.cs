using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RandomNamesWithUI.lib.ViewModel
{
    public class RelayCommand : ICommand
    {
        private readonly Func<object, Task> _executeAsync;
        private readonly Func<object, bool> _canExecute;

        // Для синхронних команд
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _executeAsync = null;
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        // Для асинхронних команд
        public RelayCommand(Func<object, Task> executeAsync, Func<object, bool> canExecute = null)
        {
            _executeAsync = executeAsync ?? throw new ArgumentNullException(nameof(executeAsync));
            _canExecute = canExecute;
        }

        private readonly Action<object> _execute;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter) =>
            _canExecute == null || _canExecute(parameter);

        public void Execute(object? parameter)
        {
            if (_executeAsync != null)
            {
                ExecuteAsync(parameter);
            }
            else
            {
                _execute(parameter);
            }
        }

        // Асинхронне виконання
        private async void ExecuteAsync(object? parameter)
        {
            if (_executeAsync != null)
            {
                await _executeAsync(parameter);
            }
        }
    }
}
