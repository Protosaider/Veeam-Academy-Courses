using System;
using System.Windows.Input;

namespace ClientApp.ViewModels
{
	internal sealed class CRefreshMessageListCommand : ICommand
    {
		private Boolean IsWaitingForExecution { get; set; } = true;

        private readonly Predicate<Object> _canExecute;
        private readonly Action _refreshAction;

        public CRefreshMessageListCommand(Action refreshAction, Predicate<Object> canExecute)
        {
            _refreshAction = refreshAction ?? throw new ArgumentNullException(nameof(refreshAction));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }

        #region ICommand

        public Boolean CanExecute(Object parameter)
        {
            return IsWaitingForExecution && (_canExecute?.Invoke(parameter) ?? true);
        }

        public void Execute(Object parameter)
        {
            IsWaitingForExecution = false;

            _refreshAction();

            IsWaitingForExecution = true;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        #endregion
    }
}