using ClientApp.Validators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ClientApp.DataSuppliers;
using ClientApp.ViewModels.Base;
using System.Threading;
using System.Windows;
using ClientApp.DataSuppliers.Data;
using ClientApp.Other;
using ClientApp.ServiceProxies;
using ClientApp.ViewModels.Contact;

namespace ClientApp.ViewModels.ChatPage
{
    //public class ChatViewModel : BaseViewModel<Guid>
    public class ChatViewModel : BaseViewModel, IDisposable
    {
        //public Int32 MAX_MESSAGE_LENGTH = 2028;
        //public Int32 AVG_MESSAGE_LENGTH = 256;

        private String _displayTitle;
        private EChatType _type;
        private Boolean _isPersonal;

        private String _pendingMessageText;
        private String _pendingContent;

        private ObservableCollection<ChatMessageViewModel> _items;
        private ObservableCollection<ChatMessageViewModel> _filteredItems;
        private String _lastSearchText;
        private String _searchText;
        private Boolean _searchIsOpen;

        public Guid Id { get; }

        public Boolean IsPersonal
        {
            get => _isPersonal;
            set
            {
                _isPersonal = value;
                OnPropertyChanged();
            }
        }

        public EChatType Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged();
            }
        }

        public String DisplayTitle
        {
            get => _displayTitle;
            set
            {
                _displayTitle = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasTitle));
            }
        }

        public Boolean HasTitle => String.IsNullOrEmpty(_displayTitle);

        private DateTimeOffset _lastDateMessagesRequested = default(DateTimeOffset);
        private Int64 _lastUSN;

        private readonly CMessageValidator _messageValidator;
        private readonly IChatSupplier _chatSupplier;

        /// <summary>
        /// The chat thread items for the list
        /// NOTE: Do not call Items.Add to add messages to this list
        ///       as it will make the FilteredItems out of sync
        /// </summary>
        /// 
        public ObservableCollection<ChatMessageViewModel> Items
        {
            get => _items ?? (_items = new ObservableCollection<ChatMessageViewModel>());
            set
            {
                if (_items == value)
                    return;
                _items = value;
                OnPropertyChanged();

                // Update filtered list to match
                FilteredItems = new ObservableCollection<ChatMessageViewModel>(_items);
            }
        }


        public ObservableCollection<ChatMessageViewModel> FilteredItems
        {
            get => _filteredItems;
            set
            {
                _filteredItems = value;
                OnPropertyChanged();
            }
        }

        public String PendingMessageText
        {
            get => _pendingMessageText;
            set
            {
                _pendingMessageText = value;
                ValidateProperty(_messageValidator.Validate, _pendingMessageText);
                OnPropertyChanged();
            }
        }

        public String PendingContent
        {
            get => _pendingContent;
            set
            {
                _pendingContent = value;
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

                if (!_searchIsOpen)
                    SearchText = String.Empty;
            }
        }

        #region Sending

        private CSendCommand _sendCommandClass;
        public CSendCommand SendCommandClass => _sendCommandClass ?? (_sendCommandClass = new CSendCommand(_chatSupplier,
            () => !HasErrors, Sent));

        private ICommand _sendCommand;
        public ICommand SendCommand => _sendCommand ?? (_sendCommand = SendCommandClass); 

        #endregion

        #region Searching

        public ICommand SearchCommand { get; set; }
        public ICommand OpenSearchCommand { get; set; }
        public ICommand CloseSearchCommand { get; set; }
        public ICommand ClearSearchCommand { get; set; }

        #endregion

        
        public Boolean ParticipantsIsOpen
        {
            get => _searchIsOpen;
            set
            {
                if (_searchIsOpen == value)
                    return;
                _searchIsOpen = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenParticipantsCommand { get; set; }
        public ICommand CloseParticipantsCommand { get; set; }

        private ObservableCollection<ChatParticipantListItemViewModel> _participants;
        public ObservableCollection<ChatParticipantListItemViewModel> Participants
        {
            get => _participants ?? (_participants = new ObservableCollection<ChatParticipantListItemViewModel>());
            set
            {
                if (_participants == value)
                    return;
                _participants = value;
                OnPropertyChanged();
            }
        }

        private ChatParticipantListItemViewModel _selectedParticipant;
        public ChatParticipantListItemViewModel SelectedParticipant
        {
            get => _selectedParticipant;
            set
            {
                _selectedParticipant = value;
                OnPropertyChanged();
            }
        }


        public ChatViewModel(CChatData selectedChat) : this()
        {
            Id = selectedChat.Id;
            DisplayTitle = selectedChat.Title;
            Type = selectedChat.Type;
            IsPersonal = selectedChat.IsPersonal;

            if (Id != default(Guid))
            {
                _lastDateMessagesRequested = DateTimeOffset.Now;
                Items = new ObservableCollection<ChatMessageViewModel>(_chatSupplier
                    .GetAllMessages(STokenProvider.Id, Id, 50, 0).Select(x => new ChatMessageViewModel(x)));

                if (Items.Count > 0)
                {
                    _lastUSN = Items[Items.Count - 1].USN;

                    var messagesToRead = Items.Where(x => !x.IsRead).ToList();

                    if (messagesToRead.Count > 0)
                    {
                        for (var i = 0; i < messagesToRead.Count; i++)
                        {
#pragma warning disable 4014
							messagesToRead[i].ReadMessage();
#pragma warning restore 4014
						}
                        _chatSupplier.ReadMessages(STokenProvider.Id, messagesToRead.Select(x => x.Id).ToList());
                    }
                }

                RunNewMessagesActivity();
            }

            //var result = _chatSupplier.GetNewMessages(TokenProvider.Id, Id, _lastDateMessagesRequested, 50, 0);
        }

        public ChatViewModel()
        {
            #region MyRegion

            /*
    Items = new ObservableCollection<ChatMessageViewModel>()
    {
        new ChatMessageViewModel()
        {
            Message = "TestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTest",
            IsRead = true
        },
        new ChatMessageViewModel()
        {
            Message = "Test",
            IsRead = true,
            IsSentByMe = true,
        },
        new ChatMessageViewModel()
        {
            Message = "Test",
            IsRead = true
        },
        new ChatMessageViewModel()
        {
            Message = "Test",
            IsRead = true
        },
    };
    */

            #endregion

            _messageValidator = new CMessageValidator();
            _chatSupplier = CChatSupplier.Create();

            ValidateProperty(_messageValidator.Validate, _pendingMessageText, nameof(PendingMessageText));

            SearchCommand = new CRelayCommand(Search);
            OpenSearchCommand = new CRelayCommand(OpenSearch);
            CloseSearchCommand = new CRelayCommand(CloseSearch);
            ClearSearchCommand = new CRelayCommand(ClearSearch);

            OpenParticipantsCommand = new CRelayCommand((obj) =>
                {
                    if (Id == default(Guid))
                        return;
                    if (ParticipantsIsOpen)
                    {
                        ParticipantsIsOpen = false;
                        return;
                    }
                    Participants = new ObservableCollection<ChatParticipantListItemViewModel>(_chatSupplier
                        .GetChatParticipants(STokenProvider.Id, Id)
                        .Select(x => new ChatParticipantListItemViewModel(x)));
                    ParticipantsIsOpen = true;
                }
            //, 
            //(obj) => Id != default(Guid)
            );

            CloseParticipantsCommand = new CRelayCommand((obj) => ParticipantsIsOpen = false);
        }

        #region Sending
        //Used in ChatPage.xaml.cs to handle 'Enter' input
        public void Send() =>
            SendCommand.Execute(new CCreateMessageData(Id, PendingMessageText, IsPersonal ? 1 : 0, PendingContent));

        private void Sent(CMessageData messageData)
        {
            var messageViewModel = new ChatMessageViewModel(messageData);
            Items.Add(messageViewModel);      
            if (String.IsNullOrEmpty(SearchText) || messageData.Message.ToLower().Contains(SearchText))               
                FilteredItems.Add(messageViewModel);

            PendingMessageText = String.Empty;
            PendingContent = String.Empty;
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

            // If we have no search text, or no items
            if (String.IsNullOrEmpty(SearchText) || Items == null || Items.Count <= 0)
            {
                // Make filtered list the same
                FilteredItems = new ObservableCollection<ChatMessageViewModel>(Items ?? Enumerable.Empty<ChatMessageViewModel>());

                _lastSearchText = SearchText;
                return;
            }

            // Find all items that contain the given text
            FilteredItems = new ObservableCollection<ChatMessageViewModel>(
                Items.Where(item => item.Message.ToLower().Contains(SearchText)));

            // Set last search text
            _lastSearchText = SearchText;
        }

        #endregion

        #region NewMessagesActivity

        private readonly TimeSpan _timeout = TimeSpan.FromMilliseconds(500);
        private readonly CancellationTokenSource _tokenSource = new CancellationTokenSource();


        private void RunNewMessagesActivity()
        {
            Console.WriteLine($@"Start activity {nameof(RunNewMessagesActivity)} for chat {Id}");

            Task.Factory.StartNew(() => NewMessagesActivity(_tokenSource.Token),
                _tokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        private void NewMessagesActivity(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested && !token.WaitHandle.WaitOne(_timeout))
                {
                    Console.WriteLine(@"RUN");
                    //await UpdateNewMessages(token);
                    //await Task.Delay(_timeout, token);

                    ////var task = UpdateNewMessages(token);
                    ////task.Start();
                    ////task.Wait(token);

                    UpdateNewMessages(token).Wait(token);
                    //Console.WriteLine("RUN BETWEEN");
                    //token.WaitHandle.WaitOne(_timeout);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        //private async Task NewMessagesActivity(CancellationToken token)
        //{
        //    token.Register(() => Console.WriteLine(@"On CancellationToken canceled"));
        //    //token.ThrowIfCancellationRequested();

        //    try
        //    {
        //        while (true)
        //        {
        //            if (token.IsCancellationRequested)
        //            {
        //                Console.WriteLine(@"Break");
        //                break;
        //            }

        //            await Task.Delay(_timeout, token);
        //            await UpdateNewMessages(token);
        //            //await Task.Delay(_timeout, token).ContinueWith(task => UpdateNewMessages(token), token);

        //            Console.WriteLine(@"Updated");
        //        }
        //    }
        //    catch (OperationCanceledException)
        //    {
        //        Console.WriteLine($@"{nameof(NewMessagesActivity)} canceled.");
        //    }
        //}

#pragma warning disable 1998
		private async Task UpdateNewMessages(CancellationToken token)
#pragma warning restore 1998
		{
            Console.WriteLine(@"INSIDE UPDATE NEW MESSAGES");
            try
            {
                var result = _chatSupplier.GetNewMessages(STokenProvider.Id, Id, _lastDateMessagesRequested, _lastUSN, 50, 0)
                    .Result;

                if (result.Count > 0)
                {                  
                    var isSearching = SearchIsOpen && !String.IsNullOrEmpty(SearchText);
                    Int32 lastIndex = 0, lastIndexFiltered = 0;

                    Int32 elementsCounter = 0; //Only for USN
                    foreach (var item in result)
                    {
                        elementsCounter++;
                        if (elementsCounter == result.Count)
                            _lastUSN = item.Usn;

                        var chatViewModel = new ChatMessageViewModel(item);
                        Int32 count = _items.Count, index = lastIndex;
                        Boolean isFound = false;
                        for (var i = index; i < count; i++)
                        {
                            //if (_items[i].DispatchDate > item.DispatchDate)
                            if (_items[i].USN > item.Usn)
                            {
                                index = i;
                                isFound = true;
                                lastIndex = i;
                                break;
                            }
                        }

                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            if (!isFound)
                                Items.Add(chatViewModel);
                            else
                                Items.Insert(index, chatViewModel);
                        });

                        if (!isSearching)
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                if (!isFound)
                                    FilteredItems.Add(chatViewModel);
                                else
                                    FilteredItems.Insert(index, chatViewModel);
                            });
                        }
                        else
                        {
                            if (item.Message.Contains(SearchText))
                            {
                                Int32 countFiltered = _filteredItems.Count, indexFiltered = lastIndexFiltered;
                                Boolean isFoundFiltered = false;

                                for (var i = indexFiltered; i < countFiltered; i++)
                                {
                                    //if (_filteredItems[i].DispatchDate > item.DispatchDate)
                                    if (_filteredItems[i].USN > item.Usn)
                                    {
                                        indexFiltered = i;
                                        isFoundFiltered = true;
                                        lastIndexFiltered = i;
                                        break;
                                    }
                                }

                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    if (!isFoundFiltered)
                                        FilteredItems.Add(chatViewModel);
                                    else
                                        FilteredItems.Insert(indexFiltered, chatViewModel);
                                });
                            }
                        }

                        //await chatViewModel.ReadMessage(); //switches property inside ViewModel for UI - maybe unnecessary
                    }

                    var isRead = _chatSupplier.ReadMessages(STokenProvider.Id, result.Select(x => x.Id).ToList());
                }

                _lastDateMessagesRequested = DateTimeOffset.Now;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                Console.WriteLine(@"ENDED UPDATE NEW MESSAGES");
            }
        }




        //private void RunNewMessagesActivity()
        //{
        //    Console.WriteLine($@"Start activity {nameof(RunNewMessagesActivity)} for chat {Id}");

        //    Task.Factory.StartNew(async () => await NewMessagesActivity(_tokenSource.Token),
        //        _tokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        //}

        //private async Task NewMessagesActivity(CancellationToken token)
        //{
        //    token.Register(() => Console.WriteLine(@"On CancellationToken canceled"));
        //    //token.ThrowIfCancellationRequested();

        //    try
        //    {
        //        while (true)
        //        {
        //            if (token.IsCancellationRequested)
        //            {
        //                Console.WriteLine(@"Break");
        //                break;
        //            }

        //            //await Task.Delay(_timeout, token);
        //            //await UpdateNewMessages(token);
        //            await Task.Delay(_timeout, token).ContinueWith(task => UpdateNewMessages(token), token);

        //            Console.WriteLine(@"Updated");
        //        }
        //    }
        //    catch (OperationCanceledException)
        //    {
        //        Console.WriteLine($@"{nameof(NewMessagesActivity)} canceled.");
        //    }
        //}

        //private async Task UpdateNewMessages(CancellationToken token)
        //{
        //    Console.WriteLine("INSIDE UPDATE NEW MESSAGES");
        //    //token.ThrowIfCancellationRequested();
        //    try
        //    {
        //        var result = await _chatSupplier.GetNewMessages(TokenProvider.Id, Id, _lastDateMessagesRequested, 50, 0);
        //        if (result.Count > 0)
        //        {
        //            var isSearching = SearchIsOpen && !String.IsNullOrEmpty(SearchText);

        //            Int32 lastIndex = 0;
        //            Int32 lastIndexFiltered = 0;

        //            foreach (var item in result)
        //            {
        //                var chatViewModel = new ChatMessageViewModel(item);
        //                ////Items.Add(chatViewModel);
        //                Int32 count = _items.Count;
        //                Int32 index = lastIndex;
        //                Boolean isFound = false;
        //                for (var i = index; i < count; i++)
        //                {
        //                    if (_items[i].DispatchDate > item.DispatchDate)
        //                    {
        //                        index = i;
        //                        isFound = true;
        //                        lastIndex = i;
        //                        break;
        //                    }
        //                }
        //                //lastIndex = index;
        //                Application.Current.Dispatcher.Invoke(() =>
        //                {
        //                    if (!isFound)
        //                        Items.Add(chatViewModel);
        //                    else
        //                        Items.Insert(index, chatViewModel);
        //                });
        //                //Application.Current.Dispatcher.Invoke(() => Items.Add(chatViewModel));

        //                if (!isSearching)
        //                {
        //                    //Application.Current.Dispatcher.Invoke(() => FilteredItems.Add(chatViewModel));
        //                    Application.Current.Dispatcher.Invoke(() =>
        //                    {
        //                        if (!isFound)
        //                            FilteredItems.Add(chatViewModel);
        //                        else
        //                            FilteredItems.Insert(index, chatViewModel);
        //                    });
        //                    Console.WriteLine("Added item filtered");
        //                }
        //                else
        //                {
        //                    if (item.Message.Contains(SearchText))
        //                    {

        //                        Int32 countFiltered = _filteredItems.Count;
        //                        Int32 indexFiltered = lastIndexFiltered;
        //                        Boolean isFoundFiltered = false;

        //                        for (var i = indexFiltered; i < countFiltered; i++)
        //                        {
        //                            if (_filteredItems[i].DispatchDate > item.DispatchDate)
        //                            {
        //                                indexFiltered = i;
        //                                isFoundFiltered = true;
        //                                lastIndexFiltered = i;
        //                                break;
        //                            }
        //                        }
        //                        //lastIndexFiltered = indexFiltered;

        //                        //Application.Current.Dispatcher.Invoke(() => FilteredItems.Add(chatViewModel));
        //                        Application.Current.Dispatcher.Invoke(() =>
        //                        {
        //                            if (!isFoundFiltered)
        //                                FilteredItems.Add(chatViewModel);
        //                            else
        //                                FilteredItems.Insert(indexFiltered, chatViewModel);
        //                        });
        //                        Console.WriteLine("Added item FILTER");
        //                    }
        //                }
        //                await chatViewModel.ReadMessage();
        //                Console.WriteLine("call read message await");
        //            }

        //            //var isRead = await _chatSupplier.ReadMessages(TokenProvider.Id, Id, result.Select(x => x.Id).ToList());
        //            var isRead = await _chatSupplier.ReadMessages(TokenProvider.Id, result.Select(x => x.Id).ToList());
        //            Console.WriteLine("call read all messages await");
        //        }

        //        _lastDateMessagesRequested = DateTimeOffset.Now;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        throw;
        //    }

        //}


        #endregion

        public void Dispose()
        {
            Console.WriteLine($@"Activity {nameof(RunNewMessagesActivity)} for chat {Id} has been canceled");
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}
