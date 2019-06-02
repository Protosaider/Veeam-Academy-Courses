using ClientApp.Other;
using System;
using ClientApp.ViewModels.Base;

namespace ClientApp.ViewModels.ContactAdd
{
	internal sealed class CCloseAddContactCommand : CBaseCommand
    {
        protected override Boolean CanExecute<T>(Object parameter)
        {
            return true;
        }

        protected override void Execute<T>(Object parameter)
        {
            CViewModelLocator.Instance.ApplicationViewModel.GoToControl(SideMenuContent.Contacts);
        }
    }
}
