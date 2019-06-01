using System;
using ClientApp.Other;

namespace ClientApp.ViewModels.LogInPage
{
    public sealed class CGoToSignUpPageCommand : CBaseCommand
    {
        protected override Boolean CanExecute<T>(Object parameter) => true;

        protected override void Execute<T>(Object parameter)
        {
            CViewModelLocator.Instance.ApplicationViewModel.GoToPage(EApplicationPage.SignUp);
        }
    }
}