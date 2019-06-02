using ClientApp.Other;
using System;
using System.Threading.Tasks;
using ClientApp.ViewModels.Base;
using System.Windows.Input;
using System.Threading;
using ClientApp.DataSuppliers;
using ClientApp.DataSuppliers.Data;
using ClientApp.ServiceProxies;

namespace ClientApp.ViewModels.ChatPage
{
	internal sealed class ChatListItemViewModel : BaseViewModel, IDisposable
    {
        private EChatType _type;
        private Boolean _isPersonal;
        private String _name;
        private String _message;
        private String _messageAuthor;
        private String _initials;
        private String _profilePictureRgb;
        private Boolean _newContentAvailable;
        private Int32 _newMessagesCount;
        private Boolean _isChatOpened;

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

        public String Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public String Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        public String MessageAuthor
        {
            get => _messageAuthor;
            set
            {
                _messageAuthor = value + ":";
                OnPropertyChanged();
            }
        }

        public String Initials
        {
            get => _initials;
            set
            {
                _initials = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The RGB values (in hex) for the background color of the profile picture
        /// For example FF00FF for Red and Blue mixed
        /// </summary>
        public String ProfilePictureRgb
        {
            get => _profilePictureRgb;
            set
            {
                _profilePictureRgb = value;
                OnPropertyChanged();
            }
        }

        public Boolean NewContentAvailable
        {
            get => _newContentAvailable;
            set
            {
                _newContentAvailable = value;
                OnPropertyChanged();
            }
        }

        public Int32 NewMessagesCount
        {
            get => _newMessagesCount;
            set
            {
                _newMessagesCount = value;
                OnPropertyChanged();
            }
        }

        public Boolean IsChatOpened
        {
            get => _isChatOpened;
            set
            {
                _isChatOpened = value;
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


        private COpenChatCommand _openChatCommandClass;

		private COpenChatCommand OpenChatCommandClass => _openChatCommandClass ?? (_openChatCommandClass = 
            new COpenChatCommand(() => { NewContentAvailable = false; NewMessagesCount = 0; IsChatOpened = true; }, () => !IsChatOpened));
        private ICommand _openChatCommand;
        public ICommand OpenChatCommand => _openChatCommand ?? (_openChatCommand = OpenChatCommandClass);


        public ChatListItemViewModel(CChatData chatDto) : this()
        {
            Name = chatDto.Title;
            Initials = Name.Substring(0, 2);
            ProfilePictureRgb = Name.FromLogin().ToColorCode();
            Id = chatDto.Id;
            IsPersonal = chatDto.IsPersonal;
            Type = chatDto.Type;

            NewMessagesCount = _chatsSupplier.GetUnreadMessagesCount(STokenProvider.Id, Id).Result;
            NewContentAvailable = NewMessagesCount > 0;
            ////Message = _chatsSupplier.GetLastMessage(TokenProvider.Id, Id).GetAwaiter().GetResult().Message;

            ////var res = _chatsSupplier.GetLastMessage(TokenProvider.Id, Id);
            ////var resu = res.GetAwaiter();
            ////var terst = resu.IsCompleted;
            ////var result = resu.GetResult();

            ////Message = result == null ? String.Empty : result.Message;

            //String result = String.Empty;
            //try
            //{
            //    result = _chatsSupplier.GetLastMessage(TokenProvider.Id, Id).GetAwaiter().GetResult().Message;
            //}
            //catch (NullReferenceException ex)
            //{
            //    // Check ex.CancellationToken.IsCancellationRequested here.
            //    Console.WriteLine(ex.Message + ex.StackTrace);
            //    Message = result;
            //    return;
            //}
            //Message = result;

            //var awaiter = _chatsSupplier.GetLastMessage(TokenProvider.Id, Id).GetAwaiter();
            //if (awaiter.IsCompleted == false)
            //    Message = String.Empty;
            //else
            //{
            //    Message = awaiter.GetResult().Message;
            //}
            var lastMessage = _chatsSupplier.GetLastMessage(STokenProvider.Id, Id).GetAwaiter().GetResult();
            Message = lastMessage.Message;
            MessageAuthor = lastMessage.IsSentByMe ? "You" : lastMessage.Login;
            RunNewMessagesActivity();
        }

        public ChatListItemViewModel() { }

        #region NewMessagesActivity

        private readonly IChatsSupplier _chatsSupplier = CChatsSupplier.Create();

        private readonly TimeSpan _timeout = TimeSpan.FromSeconds(15);
        private readonly TimeSpan _timeoutWhenIsOpened = TimeSpan.FromSeconds(4);
        private readonly CancellationTokenSource _tokenSource = new CancellationTokenSource();
        private void RunNewMessagesActivity()
        {
            Console.WriteLine($@"Start activity {nameof(RunNewMessagesActivity)} for chat {Id}");

            Task.Factory.StartNew(async () => await NewMessagesActivity(_tokenSource.Token),
                _tokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);

            Console.WriteLine($@"End activity {nameof(RunNewMessagesActivity)} for chat {Id}");
        }

        private async Task UpdateNewMessages(CancellationToken token)
        {
            Console.WriteLine($@"{nameof(UpdateNewMessages)} called by chat {Id}");
            //token.ThrowIfCancellationRequested();
            try
            {
                NewMessagesCount = await _chatsSupplier.GetUnreadMessagesCount(STokenProvider.Id, Id);
                NewContentAvailable = NewMessagesCount > 0;              
                var result = await _chatsSupplier.GetLastMessage(STokenProvider.Id, Id);
                Message = result.Message;
                MessageAuthor = result.IsSentByMe ? "You" : result.Login;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        private async Task NewMessagesActivity(CancellationToken token)
        {
            token.Register(() => Console.WriteLine(@"On CancellationToken canceled"));
            token.ThrowIfCancellationRequested();

            try
            {
                while (true)
                {
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine(@"Break");
                        break;
                    }

                    //await Task.Delay(_timeout, token);
                    //await UpdateNewMessages(token);
                    await Task.Delay(IsChatOpened ? _timeoutWhenIsOpened : _timeout, token).ContinueWith(task => UpdateNewMessages(token), token);

                    Console.WriteLine(@"Updated");
                    //await UpdateNewMessagesCount(token);
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine($@"{nameof(NewMessagesActivity)} canceled.");
            }
        }

        #endregion

        public void Dispose()
        {
            Console.WriteLine(@"Cancel");
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}
