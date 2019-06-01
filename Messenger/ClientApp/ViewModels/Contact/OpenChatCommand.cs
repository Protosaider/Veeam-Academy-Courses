using ClientApp.DataSuppliers;
using ClientApp.Other;
using ClientApp.ViewModels.ChatPage;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using ClientApp.DataSuppliers.Data;
using ClientApp.ServiceProxies;

namespace ClientApp.ViewModels.Contact
{
    public sealed class COpenChatCommand : CBaseCommand
    {
        private readonly Action _onExecute;
        private readonly Func<Boolean> _onCanExecute;

        private readonly IChatsSupplier _chatsSupplier = CChatsSupplier.Create();
        protected override Boolean CanExecute<T>(Object parameter)
        {
            //throw new NotImplementedException();
            return _onCanExecute();
        }

        public COpenChatCommand(Action onExecute, Func<Boolean> onCanExecute)
        {
            _onExecute = onExecute ?? throw new ArgumentNullException();
            _onCanExecute = onCanExecute ?? throw new ArgumentNullException();
        }

        //protected override async void Execute<T>(Object parameter)
        //{
        //    Console.WriteLine(@"Pressed Contact");
        //}

        protected override async void Execute<T>(Object parameter)
        {
            if (parameter == null)
                return;

            Guid userId = STokenProvider.Id;
            Guid participantId = (Guid)parameter;

            CChatData result;
            try
            {
                //Try to find existing chat
                result = await _chatsSupplier.GetDialog(userId, participantId);
            }
            catch (NullReferenceException)
            {
                result = null;
            }

            //result = await _chatsSupplier.GetDialog(userId, participantId);
            //if (result == CChatData.Null)

            _onExecute();

            //if there are - open
            if (result == null)
            {
                ////if not - create empty
                ////TODO after first message has been sending - create chat on server
                //ViewModelLocator.Instance.ApplicationViewModel.GoToPage(EApplicationPage.Chat, new ChatViewModel(Guid.Empty));
            }
            else
            {
                //else - just open
                CViewModelLocator.Instance.ApplicationViewModel.GoToPage(EApplicationPage.Chat, new ChatViewModel(result));
            }
        }
    }
}