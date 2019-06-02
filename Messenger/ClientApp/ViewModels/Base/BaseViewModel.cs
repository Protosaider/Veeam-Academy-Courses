using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ClientApp.Other;

namespace ClientApp.ViewModels.Base
{
    public abstract class BaseViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private readonly Object _propertyValueCheckLock = new Object();

        #region Validation

        private readonly Dictionary<String, ICollection<String>> _validationErrors = new Dictionary<String, ICollection<String>>();

		protected delegate TResult ValidateFunc<in T1, T2, out TResult>(T1 propertyValue, out T2 validationErrors);

		protected Boolean ValidateProperty<T>(ValidateFunc<T, ICollection<String>, Boolean> validate, T propertyValue, [CallerMemberName]String propertyName = "")
        {
            ICollection<String> validationErrors;
            Boolean isValid = validate(propertyValue, out validationErrors);

            SetValidationResults(isValid, validationErrors, propertyName);

            return isValid;
        }

        public void ValidatePropertyByErrorMessages(ICollection<String> errorMessages = null, [CallerMemberName]String propertyName = "")
        {
            Boolean isValid = errorMessages == null || errorMessages.Count > 0;

            if (!isValid)
            {
                _validationErrors[propertyName] = errorMessages;
                RaiseErrorsChanged(propertyName);
            }
            else if (_validationErrors.ContainsKey(propertyName))
            {
                _validationErrors.Remove(propertyName);
                RaiseErrorsChanged(propertyName);
            }
        }

		protected void SetValidationResults(Boolean isValid, ICollection<String> errorMessages = null, [CallerMemberName]String propertyName = "")
        {
            if (!isValid)
            {
                _validationErrors[propertyName] = errorMessages;
                RaiseErrorsChanged(propertyName);
            }
            else if (_validationErrors.ContainsKey(propertyName))
            {
                _validationErrors.Remove(propertyName);
                RaiseErrorsChanged(propertyName);
            }
        }

		private void RaiseErrorsChanged(String propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        #endregion

        #region INotifyDataErrorInfo

        public Boolean HasErrors => _validationErrors.Count > 0;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(String propertyName)
        {
            if (String.IsNullOrEmpty(propertyName) || !_validationErrors.ContainsKey(propertyName))
                return null;
            return _validationErrors[propertyName];
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]String propertyName = "")
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

        #region RunICommandAsync

        /// <summary>
        /// Runs a command if the updating flag is not set.
        /// If the flag is true (indicating the function is already running) then the action is not run.
        /// If the flag is false (indicating no running function) then the action is run.
        /// Once the action is finished if it was run, then the flag is reset to false
        /// </summary>
        /// <param name="updatingFlag">The boolean property flag defining if the command is already running</param>
        /// <param name="action">The action to run if the command is not already running</param>
        /// <returns></returns>
        protected async Task RunCommandAsync(Expression<Func<Boolean>> updatingFlag, Func<Task> action)
        {
            // Lock to ensure single access to check
            lock (_propertyValueCheckLock)
            {
                // Check if the flag property is true (meaning the function is already running)
                if (updatingFlag.GetPropertyValue())
                    return;

                // Set the property flag to true to indicate we are running
                updatingFlag.SetPropertyValue(true);
            }

            try
            {
                // Run the passed in action
                await action();
            }
            finally
            {
                // Set the property flag back to false now it's finished
                updatingFlag.SetPropertyValue(false);
            }
        }

        protected async Task<T> RunCommandAsync<T>(Expression<Func<Boolean>> updatingFlag, Func<Task<T>> action, T defaultValue = default(T))
        {
            // Lock to ensure single access to check
            lock (_propertyValueCheckLock)
            {
                // Check if the flag property is true (meaning the function is already running)
                if (updatingFlag.GetPropertyValue())
                    return defaultValue;

                // Set the property flag to true to indicate we are running
                updatingFlag.SetPropertyValue(true);
            }

            try
            {
                // Run the passed in action
                return await action();
            }
            finally
            {
                // Set the property flag back to false now it's finished
                updatingFlag.SetPropertyValue(false);
            }
        }

        #endregion

    }

    //public abstract class BaseViewModel<TViewModelParameter> : BaseViewModel
    //{
    //    public abstract void Initialize(TViewModelParameter parameter);
    //}
}
