using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClientApp.DataSuppliers;
using ClientApp.Other;
using ClientApp.ServiceProxies;
using DTO;

namespace ClientApp.ViewModels.LogInPage
{
    public sealed class CLogInCommandAsync : CBaseAsyncCommand
    {             
        private readonly IAuthSupplier _authSupplier;
        private readonly Func<Boolean> _getValidationResult;
        private readonly Action<ICollection<String>> _returnValidationResult;

        protected sealed override Boolean CanExecute<T>(T parameter)
        {
            return _getValidationResult();
        }

        protected sealed override async Task ExecuteAsync(Object parameter)
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
            CTokenDto result = await _authSupplier.LogInAsync(login, password);
            CTokenDto user = result;
            Boolean hasGetUser = user != null;

            //validate TryGetUser result
            _returnValidationResult(!hasGetUser ? new List<String> { "Login or password is invalid" } : null);

            if (!hasGetUser)
                return;

            STokenProvider.OnAuthCompleted(user.Id);

            //await Task.Delay(1_000);

            CViewModelLocator.Instance.ApplicationViewModel.GoToPage(EApplicationPage.Chat);

        }

        public CLogInCommandAsync(IAuthSupplier authSupplier, Func<Boolean> getValidationResult, Action<ICollection<String>> returnValidationResult)
        {
            _authSupplier = authSupplier ?? throw new ArgumentNullException(nameof(authSupplier));
            _getValidationResult = getValidationResult ?? throw new ArgumentNullException(nameof(getValidationResult));
            _returnValidationResult = returnValidationResult ?? throw new ArgumentNullException(nameof(returnValidationResult));
        }
    }
}