using ClientApp.DataSuppliers;
using ClientApp.ServiceProxies;
using ClientApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ClientApp.DataSuppliers.Data;
using ClientApp.Other;

namespace ClientApp.ViewModels.ChatPage
{
	internal sealed class ChatCreateViewModel : BaseViewModel
    {
        private ObservableCollection<ChatCreateListItemViewModel> _items;
        public ObservableCollection<ChatCreateListItemViewModel> FilteredItems
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        private readonly HashSet<Guid> _hasDialogContactsId;

        private String _displayTitle;
        public String DisplayTitle
        {
            get => _displayTitle;
            set
            {
                _displayTitle = value;
                OnPropertyChanged();
            }
        }

        private Boolean _isPersonal;
        public Boolean IsPersonal
        {
            get => _isPersonal;
            set
            {
                _isPersonal = value;
                OnPropertyChanged();
            }
        }

        private readonly List<Guid> _selectedItemsIdList = new List<Guid>();

        public Boolean HasSelectedItems => _selectedItemsIdList.Count > 0;
        private Boolean _hasDialog;
        public Boolean HasDialog
        {
            get => _hasDialog;
            private set
            {
                _hasDialog = value;
                OnPropertyChanged();
            }
        }

        private void OnSelection(Guid contactId, Boolean isSelected)
        {
            if (isSelected)
                _selectedItemsIdList.Add(contactId);
            else
                _selectedItemsIdList.Remove(contactId);

            OnPropertyChanged(nameof(HasSelectedItems));

            HasDialog = _selectedItemsIdList.Count == 1 && _hasDialogContactsId.Contains(_selectedItemsIdList.Single());
        }

        private readonly IContactsSupplier _contactsSupplier;
        private readonly IChatsSupplier _chatsSupplier;

        public ChatCreateViewModel()
		{
			_contactsSupplier = CContactsSupplier.Create();
			_chatsSupplier = CChatsSupplier.Create();

            #region MyRegion
            //FilteredItems = new ObservableCollection<ChatCreateListItemViewModel>
            //{
            //    new ChatCreateListItemViewModel(new CContactData(default(Guid), default(Guid), default(Guid),
            //        false, "Test")),                                 
            //    new ChatCreateListItemViewModel(new CContactData(default(Guid), default(Guid), default(Guid),
            //        false, "Test")),                                 
            //    new ChatCreateListItemViewModel(new CContactData(default(Guid), default(Guid), default(Guid),
            //        false, "Test")),                                 
            //    new ChatCreateListItemViewModel(new CContactData(default(Guid), default(Guid), default(Guid),
            //        false, "Test"))
            //}; 
            #endregion

            FilteredItems = new ObservableCollection<ChatCreateListItemViewModel>(_contactsSupplier.GetContacts(STokenProvider.Id).Select(x => new ChatCreateListItemViewModel(x)));
            foreach (var item in FilteredItems)
            {
                item.OnSelection = OnSelection;
            }
            _hasDialogContactsId = new HashSet<Guid>(_contactsSupplier.GetHasDialogContactsId(STokenProvider.Id));
        }


        private async void CreateChat(Object obj)
        {
            //create array of chat participants
            var participantsId = FilteredItems.Where(x => x.IsSelected).Select(x => x.ContactId).ToList();

            //TODO Get not spare names for chats earlier (before creation)?
            //TODO Or check if there are chat with same name - if not than create

            //send to server
            Boolean result = await _chatsSupplier.CreateChat(
                new CChatData(default(Guid), DisplayTitle, participantsId.Count > 1 ? EChatType.Group : EChatType.Common, IsPersonal), 
                STokenProvider.Id, participantsId);

            if (!result) //error occured
                return;
        }

        private CRelayCommand _createChatCommandClass;

		private CRelayCommand CreateChatCommandClass => _createChatCommandClass ??
                                                      (_createChatCommandClass =
                                                          new CRelayCommand(CreateChat,
                                                              (obj) => !HasDialog && !HasErrors && HasSelectedItems));
        private ICommand _createChatCommand;
        public ICommand CreateChatCommand => _createChatCommand ?? (_createChatCommand = CreateChatCommandClass);

        private CCloseCreateChatCommand _closeCreateChatCommandClass;
		private CCloseCreateChatCommand CloseCreateChatCommandClass => _closeCreateChatCommandClass ?? (_closeCreateChatCommandClass = new CCloseCreateChatCommand());
        private ICommand _closeCreateChatCommand;
        public ICommand CloseCreateChatCommand => _closeCreateChatCommand ?? (_closeCreateChatCommand = CloseCreateChatCommandClass);

    }
}
