using ClientApp.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
