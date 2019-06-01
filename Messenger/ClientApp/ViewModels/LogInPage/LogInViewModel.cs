using System;
using System.Collections.Generic;
using System.Windows.Input;
using ClientApp.DataSuppliers;
using ClientApp.Validators;
using ClientApp.ViewModels.Base;

namespace ClientApp.ViewModels.LogInPage
{
    public sealed class CLogInViewModel : BaseViewModel
    {
        private String _login;
        public String Login
        {
            get => _login;
            set
            {
                if (String.Equals(_login, value))
                    return;

                _login = value;
                ValidateProperty(_loginValidator.ValidateSpelling, _login);
                OnPropertyChanged();
            }
        }

        private String _password;
        public String Password
        {
            get => _password;
            set
            {
                if (String.Equals(_password, value))
                    return;

                _password = value;
                ValidateProperty(_loginValidator.ValidateSpelling, _password);
                OnPropertyChanged();
            }
        }


        public String SignUpText => "Maybe you just aren't registered yet?";


        private readonly IAuthSupplier _authSupplier;
        private readonly CLoginValidator _loginValidator;


        private void HandleTryGetUser(ICollection<String> validationErrors) =>
            SetValidationResults(validationErrors != null, validationErrors, nameof(Login));


        //private CLogInCommand _logInCommandClass;
        //public CLogInCommand LogInCommandClass => _logInCommandClass ?? (_logInCommandClass =
        //    new CLogInCommand(_authSupplier, () => !HasErrors, HandleTryGetUser));


        //private ICommand _logInCommand;
        //public ICommand LogInCommand => _logInCommand ?? (_logInCommand = LogInCommandClass);


        private CLogInCommandAsync _logInCommandAsyncClass;
        public CLogInCommandAsync LogInCommandAsyncClass => _logInCommandAsyncClass ?? (_logInCommandAsyncClass =
            new CLogInCommandAsync(_authSupplier, () => !HasErrors, HandleTryGetUser));
        private ICommand _logInCommand;
        public ICommand LogInCommand => _logInCommand ?? (_logInCommand = LogInCommandAsyncClass);


        private CGoToSignUpPageCommand _goToSignUpPageCommandClass;
        public CGoToSignUpPageCommand GoToSignUpPageCommandClass =>
            _goToSignUpPageCommandClass ?? (_goToSignUpPageCommandClass = new CGoToSignUpPageCommand());

        private ICommand _goToSignUpPageCommand;
        public ICommand GoToSignUpPageCommand =>
            _goToSignUpPageCommand ?? (_goToSignUpPageCommand = GoToSignUpPageCommandClass);


        public CLogInViewModel()
        {
            _authSupplier = new CAuthSupplier();
            _loginValidator = new CLoginValidator();

            //validate on creation
            ValidateProperty(_loginValidator.ValidateSpelling, _login, nameof(Login));
        }
    }
}
