using ClientApp.Other;
using System;

namespace ClientApp.ViewModels.ChatPage
{
    public sealed class CCloseCreateChatCommand : CBaseCommand
    {
        protected override Boolean CanExecute<T>(Object parameter)
        {
            return true;
        }

        protected override void Execute<T>(Object parameter)
        {
            CViewModelLocator.Instance.ApplicationViewModel.GoToControl(SideMenuContent.Chats);
        }
    }
}