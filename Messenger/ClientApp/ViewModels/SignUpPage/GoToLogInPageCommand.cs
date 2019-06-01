using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientApp.Other;

namespace ClientApp.ViewModels.SignUpPage
{
    public sealed class CGoToLogInPageCommand : CBaseCommand
    {
        protected override Boolean CanExecute<T>(Object parameter) => true;

        protected override void Execute<T>(Object parameter)
        {
            CViewModelLocator.Instance.ApplicationViewModel.GoToPage(EApplicationPage.LogIn);
        }
    }
}
