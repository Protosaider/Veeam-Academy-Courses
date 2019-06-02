using System;
using System.Collections.Generic;
using System.Windows.Input;
using ClientApp.DataSuppliers;
using ClientApp.Validators;
using ClientApp.ViewModels.Base;

namespace ClientApp.ViewModels.SignUpPage
{
	internal sealed class CSignUpViewModel : BaseViewModel
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

        public String LogInText => "Go to log in page";

        private readonly CAuthSupplier _authSupplier;
        private readonly CLoginValidator _loginValidator;


        private void HandleTryGetUser(ICollection<String> validationErrors) =>
            SetValidationResults(validationErrors != null, validationErrors, nameof(Login));


        private CSignUpCommand _signUpCommandClass;
		private CSignUpCommand SignUpCommandClass => _signUpCommandClass ?? (_signUpCommandClass =
                                                        new CSignUpCommand(_authSupplier, () => !HasErrors,
                                                            HandleTryGetUser));
        private ICommand _signUpCommand;
        public ICommand SignUpCommand => _signUpCommand ?? (_signUpCommand = SignUpCommandClass);


        private CGoToLogInPageCommand _goToLogInPageCommandClass;
		private CGoToLogInPageCommand GoToLogInPageCommandClass =>
            _goToLogInPageCommandClass ?? (_goToLogInPageCommandClass = new CGoToLogInPageCommand());
        private ICommand _goToLogInPageCommand;
        public ICommand GoToLogInPageCommand => _goToLogInPageCommand ?? (_goToLogInPageCommand = GoToLogInPageCommandClass);


        public CSignUpViewModel()
        {
            _authSupplier = CAuthSupplier.Create();
            _loginValidator = new CLoginValidator();

            //validate on creation
            ValidateProperty(_loginValidator.ValidateSpelling, _login, nameof(Login));
        }
    }
}
