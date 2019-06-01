using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ClientApp.DataSuppliers;
using ClientApp.Other;
using ClientApp.ServiceProxies;
using ClientApp.ViewModels.Base;
using ClientApp.ViewModels.ChatPage;
using DTO;

namespace ClientApp.ViewModels.Contact
{
    public sealed class ContactListViewModel : BaseViewModel
    {
        private ObservableCollection<ContactListItemViewModel> _items;
        private ObservableCollection<ContactListItemViewModel> _filteredItems;
        private String _lastSearchText;
        private String _searchText;
        private Boolean _searchIsOpen;
        private Boolean _filterIsOpen;


        public ObservableCollection<ContactListItemViewModel> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();

                // Update filtered list to match
                FilteredItems = new ObservableCollection<ContactListItemViewModel>(_items);
            }
        }

        public ObservableCollection<ContactListItemViewModel> FilteredItems
        {
            get => _filteredItems;
            set
            {
                _filteredItems = value;
                OnPropertyChanged();
            }
        }

        private ContactListItemViewModel _selectedValue;
        public ContactListItemViewModel SelectedValue
        {
            get => _selectedValue;
            set
            {
                if (_selectedValue == value)
                    return;

                if (_selectedValue != null)
                    _selectedValue.IsChatOpened = false;

                _selectedValue = value;
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

                // If the search text is empty...
                if (String.IsNullOrEmpty(SearchText))
                    // Search to restore messages
                    Search(null);
            }
        }

        public Boolean SearchIsOpen
        {
            get => _searchIsOpen;
            set
            {
                if (_searchIsOpen == value)
                    return;
                _searchIsOpen = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsMainButtonsBlocked));

                if (!_searchIsOpen)
                    SearchText = String.Empty;
            }
        }

        public Boolean FilterIsOpen
        {
            get => _filterIsOpen;
            set
            {
                if (_filterIsOpen == value)
                    return;
                _filterIsOpen = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsMainButtonsBlocked));
            }
        }

        public Boolean IsMainButtonsBlocked => FilterIsOpen || SearchIsOpen;

        private readonly Dictionary<EListSortOrder, Comparer<ContactListItemViewModel>> _comparerDict = new Dictionary<EListSortOrder, Comparer<ContactListItemViewModel>>
        {
            [EListSortOrder.ByStringAsc] = Comparer<ContactListItemViewModel>.Create(new Comparison<ContactListItemViewModel>((l, r) => l.Name.CompareTo(r.Name))),
            [EListSortOrder.ByStringDesc] = Comparer<ContactListItemViewModel>.Create(new Comparison<ContactListItemViewModel>((l, r) => -1 * l.Name.CompareTo(r.Name))),
            [EListSortOrder.ByDateAsc] = Comparer<ContactListItemViewModel>.Create(new Comparison<ContactListItemViewModel>((l, r) => l.LastActiveTime.CompareTo(r.LastActiveTime))),
            [EListSortOrder.ByDateDesc] = Comparer<ContactListItemViewModel>.Create(new Comparison<ContactListItemViewModel>((l, r) => -1 * l.LastActiveTime.CompareTo(r.LastActiveTime))),
        };

        public EListSortOrder SortOrder { get; set; } = EListSortOrder.None;

        public ICommand ChangeSortOrderCommand { get; set; }
        public ICommand OpenFilterCommand { get; set; }
        public ICommand CloseFilterCommand { get; set; }


        public ICommand SearchCommand { get; set; }
        public ICommand OpenSearchCommand { get; set; }
        public ICommand CloseSearchCommand { get; set; }
        public ICommand ClearSearchCommand { get; set; }

        private ICommand _openAddContactCommand;
        public ICommand OpenAddContactCommand => _openAddContactCommand ?? (_openAddContactCommand = OpenAddContactCommandClass);
        private COpenAddContactCommand _openAddContactCommandClass;
        public COpenAddContactCommand OpenAddContactCommandClass => _openAddContactCommandClass ?? (_openAddContactCommandClass = new COpenAddContactCommand());

        private readonly IContactsSupplier _contactsSupplier;


        public ContactListViewModel()
        {
            #region MyRegion
            /*
                Items = new ObservableCollection<ContactListItemViewModel>()
                {
                    new ContactListItemViewModel()
                    {
                        Initials = "AB",
                        Name = "Arthur",
                        LastActiveTime = "Break Those Bones Whose Sinews Gave It Motion",
                        ProfilePictureRGB = "3099c5",
                    },
                    new ContactListItemViewModel()
                    {
                        Initials = "AB",
                        Name = "Arthur",
                        LastActiveTime = "Break Those Bones Whose Sinews Gave It Motion",
                        ProfilePictureRGB = "3099c5",
                    },
                    new ContactListItemViewModel()
                    {
                        Initials = "AB",
                        Name = "Arthur",
                        LastActiveTime = "Last seen 23.04.19",
                        ProfilePictureRGB = "3099c5",
                    },
                    new ContactListItemViewModel()
                    {
                        Initials = "AB",
                        Name = "Arthur",
                        LastActiveTime = "online",
                        ProfilePictureRGB = "3099c5",
                    },
                    new ContactListItemViewModel()
                    {
                        Initials = "AB",
                        Name = "Arthur",
                        LastActiveTime = "online",
                        ProfilePictureRGB = "3099c5",
                    },
                    new ContactListItemViewModel()
                    {
                        Initials = "AB",
                        Name = "Arthur",
                        LastActiveTime = "online",
                        ProfilePictureRGB = "3099c5",
                    },
                    new ContactListItemViewModel()
                    {
                        Initials = "AB",
                        Name = "Arthur",
                        LastActiveTime = "online",
                        ProfilePictureRGB = "3099c5",
                    },
                    new ContactListItemViewModel()
                    {
                        Initials = "AB",
                        Name = "Arthur",
                        LastActiveTime = "online",
                        ProfilePictureRGB = "3099c5",
                    },
                };
                */ 
            #endregion

            _contactsSupplier = new CContactsSupplier();

            var lastActiveDates = _contactsSupplier.GetContactsLastActiveDate(STokenProvider.Id);

            Items = new ObservableCollection<ContactListItemViewModel>(_contactsSupplier.GetContacts(ServiceProxies.STokenProvider.Id).Select(x => new ContactListItemViewModel(x)));

            foreach (var contactListItemViewModel in Items)
            {
                //contactListItemViewModel.LastActiveTime = lastActiveDates[]
            }

            SearchCommand = new CRelayCommand(Search);
            OpenSearchCommand = new CRelayCommand(OpenSearch);
            CloseSearchCommand = new CRelayCommand(CloseSearch);
            ClearSearchCommand = new CRelayCommand(ClearSearch);

            OpenFilterCommand = new CRelayCommand(OpenFilter);
            CloseFilterCommand = new CRelayCommand(CloseFilter);
            ChangeSortOrderCommand = new CRelayCommand((obj) =>
            {
                SortOrder = (EListSortOrder)obj;
                Filter(null);
            });
        }

        #region Filtering

        private void OpenFilter(Object obj) => FilterIsOpen = true;
        private void CloseFilter(Object obj) => FilterIsOpen = false;

        private void Filter(Object obj)
        {
            // If we have no search text, or no items
            if (String.IsNullOrEmpty(SearchText) || Items == null || Items.Count <= 0)
            {
                if (Items == null)
                    FilteredItems =
                        new ObservableCollection<ContactListItemViewModel>(Enumerable.Empty<ContactListItemViewModel>());
                else
                {
                    if (SortOrder == EListSortOrder.None)
                        FilteredItems = new ObservableCollection<ContactListItemViewModel>(Items);
                    else
                    {
                        FilteredItems =
                            new ObservableCollection<ContactListItemViewModel>(Items.OrderBy(x => x,
                                _comparerDict[SortOrder]));
                    }
                }

                _lastSearchText = SearchText;
                return;
            }

            _lastSearchText = SearchText.ToLower();

            // Find all items that contain the given text
            FilteredItems = new ObservableCollection<ContactListItemViewModel>(Items
                .Where(item => item.Name.ToLower().Contains(_lastSearchText))
                .OrderBy(x => x, _comparerDict[SortOrder]));
        }

        #endregion


        private void ClearSearch(Object obj)
        {
            if (String.IsNullOrEmpty(SearchText))
                SearchIsOpen = false;
            else
                SearchText = String.Empty;
        }

        private void OpenSearch(Object obj) => SearchIsOpen = true;
        private void CloseSearch(Object obj) => SearchIsOpen = false;

        private void Search(Object obj)
        {
            // Make sure we don't re-search the same text
            if ((String.IsNullOrEmpty(_lastSearchText) && String.IsNullOrEmpty(SearchText)) ||
                String.Equals(_lastSearchText, SearchText))
                return;

            Filter(obj);
        }

    }
}