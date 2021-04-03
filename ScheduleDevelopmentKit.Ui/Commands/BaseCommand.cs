using System;
using System.Windows.Input;

namespace ScheduleDevelopmentKit.Ui.Commands
{
    public class BaseCommand : ICommand
    {
        private readonly Action<object> _action;

        public BaseCommand(Action<object> action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
    public class BaseCommand<T> : ICommand
    {
        private readonly Action<T> _action;

        public BaseCommand(Action<T> action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action((T)parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}