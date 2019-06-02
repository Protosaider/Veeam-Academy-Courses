using System;
using System.Collections.Generic;
using ClientApp.DataSuppliers;
using ClientApp.Other;
using ClientApp.ViewModels.Base;

namespace ClientApp.ViewModels.LogInPage
{
    public class CLogInCommand : CBaseCommand
    {             
        private readonly IAuthSupplier _authSupplier;
        private readonly Func<Boolean> _getValidationResult;
        private readonly Action<ICollection<String>> _returnValidationResult;

        protected sealed override Boolean CanExecute<T>(Object parameter)
        {
            return _getValidationResult();
        }

        protected sealed override void Execute<T>(Object parameter)
        {
            String login = (String)parameter;

            ////TryGetUser
            //CTokenDto user = _authSupplier.LogIn(login);
            //Boolean hasGetUser = user != null;

            ////validate TryGetUser result
            //_returnValidationResult(!hasGetUser ? new List<String> { "Login is invalid" } : null);

            //if (!hasGetUser)
            //    return;

            //TokenProvider.Id = user.Id;
            CViewModelLocator.Instance.ApplicationViewModel.GoToPage(EApplicationPage.Chat);
        }

        public CLogInCommand(IAuthSupplier authSupplier, Func<Boolean> getValidationResult, Action<ICollection<String>> returnValidationResult)
        {
            _authSupplier = authSupplier ?? throw new ArgumentNullException(nameof(authSupplier));
            _getValidationResult = getValidationResult ?? throw new ArgumentNullException(nameof(getValidationResult));
            _returnValidationResult = returnValidationResult ?? throw new ArgumentNullException(nameof(returnValidationResult));
        }
    }
}