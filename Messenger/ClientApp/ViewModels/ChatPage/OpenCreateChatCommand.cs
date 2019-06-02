using ClientApp.Other;
using System;
using ClientApp.ViewModels.Base;

namespace ClientApp.ViewModels.ChatPage
{
	internal sealed class COpenCreateChatCommand : CBaseCommand
    {
        protected override Boolean CanExecute<T>(Object parameter)
        {
            //throw new NotImplementedException();
            return true;
        }

        protected override void Execute<T>(Object parameter)
        {
            CViewModelLocator.Instance.ApplicationViewModel.GoToControl(SideMenuContent.AddChat, new ChatCreateViewModel());
        }
    }
}
