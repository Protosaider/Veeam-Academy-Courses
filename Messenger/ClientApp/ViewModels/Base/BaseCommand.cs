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
    public abstract class CBaseCommand<T> : ICommand, INotifyPropertyChanged
    {
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

        public Expression<Func<Boolean>> IsExecutingRef => () => IsExecuting;

        private readonly Predicate<T> _canExecute;
        private readonly Action<T> _execute;

        protected CBaseCommand()
        {
            _canExecute = CanExecute;
            _execute = Execute;
        }

        protected abstract Boolean CanExecute(T parameter);
        protected abstract void Execute(T parameter);

        public Boolean CanExecute(Object parameter)
        {
            return !_isExecuting && (_canExecute?.Invoke((T)parameter) ?? true);
        }

        public void Execute(Object parameter)
        {
            _execute((T)parameter);
        }

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

    public abstract class CBaseCommand : ICommand, INotifyPropertyChanged
    {
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

        public Expression<Func<Boolean>> IsExecutingRef => () => IsExecuting;

        private readonly Predicate<Object> _canExecute;
        private readonly Action<Object> _execute;

        protected CBaseCommand()
        {
            _canExecute = CanExecute<Object>;
            _execute = Execute<Object>;
        }

        protected abstract Boolean CanExecute<T>(Object parameter);
        protected abstract void Execute<T>(Object parameter);

        public Boolean CanExecute(Object parameter)
        {
            return !_isExecuting && (_canExecute?.Invoke(parameter) ?? true);
        }

        public void Execute(Object parameter)
        {
            _execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; CanExecuteChangedInternal += value; }
            remove { CommandManager.RequerySuggested -= value; CanExecuteChangedInternal -= value; }
        }

        private event EventHandler CanExecuteChangedInternal;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChangedInternal.Raise(this);
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
