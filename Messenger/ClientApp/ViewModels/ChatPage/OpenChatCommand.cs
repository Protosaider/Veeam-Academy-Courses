using ClientApp.Other;
using System;
using ClientApp.DataSuppliers.Data;
using ClientApp.ViewModels.Base;

namespace ClientApp.ViewModels.ChatPage
{
	internal sealed class COpenChatCommand : CBaseCommand
    {
        private readonly Action _onExecute;
        private readonly Func<Boolean> _onCanExecute;

        protected override Boolean CanExecute<T>(Object parameter)
        {
            //if (parameter == null)
            //    return false;
            //var args = (OpenChatCommandArgs)parameter;
            //return args.IsSelected;
            return _onCanExecute();
        }

        public COpenChatCommand(Action onExecute, Func<Boolean> onCanExecute)
        {
            _onExecute = onExecute ?? throw new ArgumentNullException();
            _onCanExecute = onCanExecute ?? throw new ArgumentNullException();
        }

        protected override void Execute<T>(Object parameter)
        {
            if (parameter == null)
                return;

            var chatData = (CChatData)parameter;
            //var chatData = ((OpenChatCommandArgs)parameter).ChatData;

            _onExecute.Invoke();

            CViewModelLocator.Instance.ApplicationViewModel.GoToPage(EApplicationPage.Chat, new ChatViewModel(chatData));
        }
    }
}