using ClientApp.Other;
using ClientApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClientApp.ViewModels
{
    public abstract class CBaseAsyncCommand : ICommand, INotifyPropertyChanged
    {
        #region IsExecuting

        private Boolean _isExecuting;

		private Boolean IsExecuting
        {
            get => _isExecuting;
			set
            {
                _isExecuting = value;
                OnPropertyChanged();
            }
        }

		private Expression<Func<Boolean>> IsExecutingRef => () => IsExecuting;

        #endregion

        private readonly Predicate<Object> _canExecute;
        private readonly Func<Object, Task> _execute;

		protected CBaseAsyncCommand()
        {
            _execute = ExecuteAsync;
            _canExecute = CanExecute<Object>;
        }

        protected abstract Task ExecuteAsync(Object parameter);
        protected abstract Boolean CanExecute<T>(T parameter);


        private readonly Object _propertyValueCheckLock = new Object();

		private async Task RunCommandAsync(Expression<Func<Boolean>> updatingFlag, Func<Object, Task> action, Object parameter)
        {
            lock (_propertyValueCheckLock)
            {
                if (updatingFlag.GetPropertyValue())
                    return;

                updatingFlag.SetPropertyValue(true);
            }

            try
            {
                await action(parameter);
            }
            finally
            {
                updatingFlag.SetPropertyValue(false);
            }
        }

        #region ICommand

        public Boolean CanExecute(Object parameter)
        {
            return !_isExecuting && (_canExecute?.Invoke(parameter) ?? true);
        }

        public async void Execute(Object parameter)
        {
            await RunCommandAsync(IsExecutingRef, _execute, parameter);
        }

        #endregion

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged([CallerMemberName]String propertyName = "")
        {
            VerifyPropertyName(propertyName);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [Conditional("DEBUG")]
        private void VerifyPropertyName(String propertyName)
        {
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
                throw new ArgumentNullException(GetType().Name + " does not contain property: " + propertyName);
        }

        #endregion

    }
}
