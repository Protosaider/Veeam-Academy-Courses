using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ClientApp.DataSuppliers;
using ClientApp.Other;
using ClientApp.ViewModels.Base;

namespace ClientApp.ViewModels.ChatPage
{
	internal sealed class ChatListViewModel : BaseViewModel
    {
        private ObservableCollection<ChatListItemViewModel> _items;
        private ObservableCollection<ChatListItemViewModel> _filteredItems;

        private String _lastSearchText;
        private String _searchText;
        private Boolean _searchIsOpen;

        private Boolean _filterIsOpen;

        private ChatListItemViewModel _selectedValue;
        public ChatListItemViewModel SelectedValue
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

        public ObservableCollection<ChatListItemViewModel> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
                // Update filtered list to match
                FilteredItems = new ObservableCollection<ChatListItemViewModel>(_items);
            }
        }

        public ObservableCollection<ChatListItemViewModel> FilteredItems
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

        private readonly Dictionary<EListSortOrder, Comparer<ChatListItemViewModel>> _comparerDict = new Dictionary<EListSortOrder, Comparer<ChatListItemViewModel>>
        {
            [EListSortOrder.ByStringAsc] = Comparer<ChatListItemViewModel>.Create(new Comparison<ChatListItemViewModel>((l, r) => String.Compare(l.Name, r.Name, StringComparison.InvariantCultureIgnoreCase))),
            [EListSortOrder.ByStringDesc] = Comparer<ChatListItemViewModel>.Create(new Comparison<ChatListItemViewModel>((l, r) => -1 * String.Compare(l.Name, r.Name, StringComparison.InvariantCultureIgnoreCase))),
            [EListSortOrder.ByNumberAsc] = Comparer<ChatListItemViewModel>.Create(new Comparison<ChatListItemViewModel>((l, r) => l.NewMessagesCount.CompareTo(r.NewMessagesCount))),
            [EListSortOrder.ByNumberDesc] = Comparer<ChatListItemViewModel>.Create(new Comparison<ChatListItemViewModel>((l, r) => -1 * l.NewMessagesCount.CompareTo(r.NewMessagesCount))),
        };

        public EListSortOrder SortOrder { get; set; } = EListSortOrder.None;

        public ICommand ChangeSortOrderCommand { get; }
        public ICommand OpenFilterCommand { get; }
        public ICommand CloseFilterCommand { get; }



        public ICommand SearchCommand { get; }
        public ICommand OpenSearchCommand { get; }
        public ICommand CloseSearchCommand { get; }
        public ICommand ClearSearchCommand { get; }


        private COpenCreateChatCommand _openCreateChatCommandClass;
        private COpenCreateChatCommand OpenCreateChatCommandClass => _openCreateChatCommandClass ??
            (_openCreateChatCommandClass = new COpenCreateChatCommand());

        private ICommand _openCreateChatCommand;
        public ICommand OpenCreateChatCommand => _openCreateChatCommand ?? (_openCreateChatCommand = OpenCreateChatCommandClass);


        private readonly IChatsSupplier _chatsSupplier;


        public ChatListViewModel()
        {
            #region MyRegion
            /*
                  Items = new ObservableCollection<ChatListItemViewModel>()
                  {
                      new ChatListItemViewModel()
                      {
                          Initials = "AB",
                          Name = "Arthur",
                          Message = "Break Those Bones Whose Sinews Gave It Motion",
                          ProfilePictureRGB = "3099c5",
                          Type = Other.EChatType.Common,
                      },
                      new ChatListItemViewModel()
                      {
                          Initials = "AB",
                          Name = "Black",
                          Message = "Break Those",
                          ProfilePictureRGB = "3099c5",
                          Type = Other.EChatType.Group,
                      },
                      new ChatListItemViewModel()
                      {
                          Initials = "AB",
                          Name = "Silver",
                          Message = "Bones Whose Sinews",
                          ProfilePictureRGB = "3099c5",
                          Type = Other.EChatType.Protected,
                      },
                      new ChatListItemViewModel()
                      {
                          Initials = "AB",
                          Name = "Ufa",
                          Message = "Gave It Motion",
                          ProfilePictureRGB = "3099c5",
                          Type = Other.EChatType.Common,
                      },
                      new ChatListItemViewModel()
                      {
                          Initials = "AB",
                          Name = "Artemis",
                          Message = "Break Those Bones Whose Sinews Gave It Motion",
                          ProfilePictureRGB = "3099c5",
                          Type = Other.EChatType.Common,
                      },
                      new ChatListItemViewModel()
                      {
                          Initials = "AB",
                          Name = "Art hur",
                          Message = "Break Those Bones Whose Sinews Gave It Motion",
                          ProfilePictureRGB = "3099c5",
                          Type = Other.EChatType.Common,
                      },
                      new ChatListItemViewModel()
                      {
                          Initials = "AB",
                          Name = "Art hurricane",
                          Message = "Break Those Bones Whose Sinews Gave It Motion",
                          ProfilePictureRGB = "3099c5",
                          Type = Other.EChatType.Common,
                      },
                      new ChatListItemViewModel()
                      {
                          Initials = "AB",
                          Name = "Arthur",
                          Message = "Break Those Bones Whose Sinews Gave It Motion",
                          ProfilePictureRGB = "3099c5",
                          Type = Other.EChatType.Common,
                      },
                      new ChatListItemViewModel()
                      {
                          Initials = "AB",
                          Name = "Arthur",
                          Message = "Break Those Bones Whose Sinews Gave It Motion",
                          ProfilePictureRGB = "3099c5",
                          Type = Other.EChatType.Common,
                      },
                      new ChatListItemViewModel()
                      {
                          Initials = "AB",
                          Name = "Arthur",
                          Message = "Break Those Bones Whose Sinews Gave It Motion",
                          ProfilePictureRGB = "3099c5",
                          Type = Other.EChatType.Common,
                      },
                      new ChatListItemViewModel()
                      {
                          Initials = "AB",
                          Name = "Arthur",
                          Message = "Break Those Bones Whose Sinews Gave It Motion",
                          ProfilePictureRGB = "3099c5",
                          Type = Other.EChatType.Common,
                      },
                      new ChatListItemViewModel()
                      {
                          Initials = "AB",
                          Name = "Arthur",
                          Message = "Break Those Bones Whose Sinews Gave It Motion",
                          ProfilePictureRGB = "3099c5",
                          Type = Other.EChatType.Common,
                      },
                      new ChatListItemViewModel()
                      {
                          Initials = "AB",
                          Name = "Arthur",
                          Message = "Break Those Bones Whose Sinews Gave It Motion",
                          ProfilePictureRGB = "3099c5",
                          Type = Other.EChatType.Common,
                      },
                      new ChatListItemViewModel()
                      {
                          Initials = "AB",
                          Name = "Arthur",
                          Message = "Break Those Bones Whose Sinews Gave It Motion",
                          ProfilePictureRGB = "3099c5",
                          Type = Other.EChatType.Common,
                      },
                  };
                  */ 
            #endregion

            _chatsSupplier = CChatsSupplier.Create();

            Items = new ObservableCollection<ChatListItemViewModel>(_chatsSupplier
                .GetChats(ServiceProxies.STokenProvider.Id).Select(x => new ChatListItemViewModel(x)));

            SearchCommand = new CRelayCommand(Search);
            OpenSearchCommand = new CRelayCommand(OpenSearch);
            CloseSearchCommand = new CRelayCommand(CloseSearch);
            ClearSearchCommand = new CRelayCommand(ClearSearch);

            OpenFilterCommand = new CRelayCommand(OpenFilter);
            CloseFilterCommand = new CRelayCommand(CloseFilter);
            ChangeSortOrderCommand = new CRelayCommand((obj) =>
            {
                SortOrder = (EListSortOrder)obj;
                Filter();
            });
        }

        #region Filtering

        private void OpenFilter(Object obj) => FilterIsOpen = true;
        private void CloseFilter(Object obj) => FilterIsOpen = false;

        private void Filter()
        {
            // If we have no search text, or no items
            if (String.IsNullOrEmpty(SearchText) || Items == null || Items.Count <= 0)
            {
                if (Items == null)
                    FilteredItems =
                        new ObservableCollection<ChatListItemViewModel>(Enumerable.Empty<ChatListItemViewModel>());
                else
                {
                    if (SortOrder == EListSortOrder.None)
                        FilteredItems = new ObservableCollection<ChatListItemViewModel>(Items);
                    else
                    {
                        FilteredItems =
                            new ObservableCollection<ChatListItemViewModel>(Items.OrderBy(x => x,
                                _comparerDict[SortOrder]));
                    }
                }

                _lastSearchText = SearchText;
                return;
            }

            _lastSearchText = SearchText.ToLower();

            // Find all items that contain the given text
            FilteredItems = new ObservableCollection<ChatListItemViewModel>(Items
                .Where(item => item.Name.ToLower().Contains(_lastSearchText))
                .OrderBy(x => x, _comparerDict[SortOrder]));
        }

        #endregion

        #region Searching

        private void ClearSearch(Object obj)
        {
            // If there is no search text...
            if (String.IsNullOrEmpty(SearchText))
                // Close search dialog
                SearchIsOpen = false;
            // Otherwise...
            else
                // Clear the text
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

            Filter();

            //// Find all items that contain the given text
            //FilteredItems = new ObservableCollection<ChatListItemViewModel>(
            //    Items.Where(item => item.Name.ToLower().Contains(SearchText)));

            //// Set last search text
            //_lastSearchText = SearchText;
        } 
        
        #endregion

    }
}
