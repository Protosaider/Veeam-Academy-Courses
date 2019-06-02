using System;
using ClientApp.Other;
using ClientApp.ViewModels.Base;

namespace ClientApp.ViewModels.SignUpPage
{
	internal sealed class CGoToLogInPageCommand : CBaseCommand
    {
        protected override Boolean CanExecute<T>(Object parameter) => true;

        protected override void Execute<T>(Object parameter)
        {
            CViewModelLocator.Instance.ApplicationViewModel.GoToPage(EApplicationPage.LogIn);
        }
    }
}
