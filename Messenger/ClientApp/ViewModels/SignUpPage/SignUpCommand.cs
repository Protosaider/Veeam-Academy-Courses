using System;
using System.Collections.Generic;
using ClientApp.DataSuppliers;
using ClientApp.Other;
using ClientApp.ViewModels.Base;
using DTO;

namespace ClientApp.ViewModels.SignUpPage
{
	internal sealed class CSignUpCommand : CBaseCommand
    {
        private readonly IAuthSupplier _authSupplier;
        private readonly Func<Boolean> _getValidationResult;
        private readonly Action<ICollection<String>> _returnValidationResult;


        public CSignUpCommand(IAuthSupplier authSupplier, Func<Boolean> getValidationResult, Action<ICollection<String>> returnValidationResult)
        {
            _authSupplier = authSupplier ?? throw new ArgumentNullException(nameof(authSupplier));
            _getValidationResult = getValidationResult ?? throw new ArgumentNullException(nameof(getValidationResult));
            _returnValidationResult = returnValidationResult ?? throw new ArgumentNullException(nameof(returnValidationResult));
        }

        protected override Boolean CanExecute<T>(Object parameter)
        {
            return _getValidationResult();
        }

        protected override void Execute<T>(Object parameter)
        {
            //TODO Добавить логгер
            if (parameter == null)
            {
                _returnValidationResult(new List<String> { "Null arguments" });
                return;
            }

            List<String> values = parameter as List<String>;
            if (values == null)
            {
                _returnValidationResult(new List<String> { "Null arguments" });
                return;
            }

            String login = values[0];
            String password = values[1];

            //TryGetUser
            CTokenDto user = _authSupplier.LogIn(login, password);
            Boolean hasGetUser = user != null;

            //validate TryGetUser result
            _returnValidationResult(!hasGetUser ? new List<String>() {"This login is occupied already"} : null);

            if (!hasGetUser)
                return;

            String result = _authSupplier.SignUp(login, password, null);

            if (String.IsNullOrEmpty(result))
                return;

            CViewModelLocator.Instance.ApplicationViewModel.GoToPage(EApplicationPage.Chat);
        }
    }
}