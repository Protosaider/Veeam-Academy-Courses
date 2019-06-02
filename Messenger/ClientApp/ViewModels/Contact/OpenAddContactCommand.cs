using ClientApp.Other;
using System;
using ClientApp.ViewModels.Base;

namespace ClientApp.ViewModels.Contact
{
	internal sealed class COpenAddContactCommand : CBaseCommand
    {
        protected override Boolean CanExecute<T>(Object parameter)
        {
            return true;
        }

        protected override void Execute<T>(Object parameter)
        {
            CViewModelLocator.Instance.ApplicationViewModel.GoToControl(SideMenuContent.AddContact);
        }
    }
}
