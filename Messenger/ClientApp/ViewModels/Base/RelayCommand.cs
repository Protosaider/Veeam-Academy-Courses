using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ClientApp.ViewModels.Base
{
    public class CRelayCommand<T> : ICommand, INotifyPropertyChanged
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

        public CRelayCommand(Action<T> execute) : this(execute, null)
        {
            _execute = execute;
        }

        public CRelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

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

	internal sealed class CRelayCommand : ICommand, INotifyPropertyChanged
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

		internal CRelayCommand(Action<Object> execute) : this(execute, null)
        {
            _execute = execute;
        }

		internal CRelayCommand(Action<Object> execute, Predicate<Object> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public Boolean CanExecute(Object parameter)
        {
            return !_isExecuting && (_canExecute?.Invoke(parameter) ?? true);
        }

        public void Execute(Object parameter)
        {
            _execute(parameter);
        }

        // Ensures WPF commanding infrastructure asks all RelayCommand objects whether their
        // associated views should be enabled whenever a command is invoked 
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                CanExecuteChangedInternal += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
                CanExecuteChangedInternal -= value;
            }
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
