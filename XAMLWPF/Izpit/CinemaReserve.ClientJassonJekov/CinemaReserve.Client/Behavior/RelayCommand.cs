using System;
using System.Windows.Input;

namespace CinemaReserve.Client.Behavior
{
    public delegate void ExecuteDelegate(object parameter);
    public delegate bool CanExecuteDelegate(object parameter);

    public class RelayCommand : ICommand
    {
        private ExecuteDelegate execute;
        private CanExecuteDelegate canExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(ExecuteDelegate execute,
            CanExecuteDelegate canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            var result = this.canExecute == null || this.canExecute(parameter);
            return result;
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
}
