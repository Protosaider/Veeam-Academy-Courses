using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ClientApp.DataSuppliers;
using ClientApp.ServiceProxies;
using ClientApp.ViewModels.Base;

namespace ClientApp.ViewModels.ContactAdd
{
	internal sealed class ContactAddListViewModel : BaseViewModel
    {
        private ObservableCollection<ContactAddListItemViewModel> _filteredItems;
        private String _lastSearchText;
        private String _searchText;

        public ObservableCollection<ContactAddListItemViewModel> FilteredItems
        {
            get => _filteredItems;
            set
            {
                _filteredItems = value;
                OnPropertyChanged();
            }
        }

        public String SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText == value)
                    return;
                _searchText = value;
                OnPropertyChanged();

                if (String.IsNullOrEmpty(SearchText))
                    Search(null);
            }
        }

        public ICommand SearchCommand { get; }
        public ICommand ClearSearchCommand { get; }

        private ICommand _addContactCommand;
        public ICommand AddContactCommand => _addContactCommand ?? (_addContactCommand = AddContactCommandClass);
        private CAddContactCommand _addContactCommandClass;
		private CAddContactCommand AddContactCommandClass => _addContactCommandClass ?? (_addContactCommandClass = new CAddContactCommand());

        private ICommand _closeAddContactCommand;
        public ICommand CloseAddContactCommand => _closeAddContactCommand ?? (_closeAddContactCommand = CloseAddContactCommandClass);
        private CCloseAddContactCommand _closeAddContactCommandClass;
		private CCloseAddContactCommand CloseAddContactCommandClass => _closeAddContactCommandClass ?? (_closeAddContactCommandClass = new CCloseAddContactCommand());

        private readonly IContactsSupplier _contactsSupplier;

        public ContactAddListViewModel()
        {
            #region MyRegion
            //FilteredItems = new ObservableCollection<ContactAddListItemViewModel>()
            //{
            //    new ContactAddListItemViewModel()
            //    {
            //        Initials = "AB",
            //        Name = "Arthur",
            //        LastActiveTime = "Break Those Bones Whose Sinews Gave It Motion",
            //        ProfilePictureRgb = "3099c5",
            //    },
            //    new ContactAddListItemViewModel()
            //    {
            //        Initials = "AB",
            //        Name = "Arthur",
            //        LastActiveTime = "Break Those Bones Whose Sinews Gave It Motion",
            //        ProfilePictureRgb = "3099c5",
            //    },
            //    new ContactAddListItemViewModel()
            //    {
            //        Initials = "AB",
            //        Name = "Arthur",
            //        LastActiveTime = "Last seen 23.04.19",
            //        ProfilePictureRgb = "3099c5",
            //    },                                          
            //}; 
            #endregion

            _contactsSupplier = CContactsSupplier.Create();

            //FilteredItems = new ObservableCollection<ContactAddListItemViewModel>(_contactsSupplier.GetContacts(TokenProvider.Id).Select(x => new ContactAddListItemViewModel(x)));
            FilteredItems = new ObservableCollection<ContactAddListItemViewModel>();

            SearchCommand = new CRelayCommand(Search);
            ClearSearchCommand = new CRelayCommand(ClearSearch);
        }

        private void ClearSearch(Object obj) => SearchText = String.Empty;

        private void Search(Object obj)
        {
            if ((String.IsNullOrEmpty(_lastSearchText) && String.IsNullOrEmpty(SearchText)) ||
                String.Equals(_lastSearchText, SearchText))
                return;

            // If we have no search text
            if (String.IsNullOrEmpty(SearchText))
            {
                // Make filtered list the same
                FilteredItems = new ObservableCollection<ContactAddListItemViewModel>(Enumerable.Empty<ContactAddListItemViewModel>());
                _lastSearchText = SearchText;
                return;
            }

            _lastSearchText = SearchText.ToLower();
            // Find all items that contain the given text
            FilteredItems = new ObservableCollection<ContactAddListItemViewModel>(_contactsSupplier.FindContacts(STokenProvider.Id, _lastSearchText).Select(x => new ContactAddListItemViewModel(x)));
        }

    }
}