using System;
using System.Threading.Tasks;
using ClientApp.DataSuppliers.Data;
using ClientApp.Other;
using ClientApp.ViewModels.Base;

namespace ClientApp.ViewModels.ChatPage
{
	internal class ChatMessageViewModel : BaseViewModel
    {
        public Guid Id { get; }
        public Int64 Usn { get; }
        private DateTimeOffset _dispatchDate;
        private DateTimeOffset _convertedDispatchDate;
        public DateTimeOffset DispatchDate
        {
            get => _convertedDispatchDate;
            set
            {
                _dispatchDate = value;
                _convertedDispatchDate = _dispatchDate.ToLocalTime();
                OnPropertyChanged();
            }
        }
        public String Message { get; set; }
        public Boolean IsSentByMe { get; set; }
        /// <summary>
        /// A flag indicating if this item was added since the first main list of items was created
        /// Used as a flag for animating in
        /// </summary>
        public Boolean IsRead { get; set; }

        public DateTimeOffset ReadDate { get; set; }

        public String SenderName { get; set; }
        public String Initials { get; set; }
        public String ProfilePictureRgb { get; set; }

        public Boolean IsSelected { get; set; }

        public ChatMessageViewModel(CMessageData messageDto)
        {
            Id = messageDto.Id;
            Usn = messageDto.Usn;
            Message = messageDto.Message;
            DispatchDate = messageDto.DispatchDate;
            IsRead = messageDto.IsRead;
            IsSentByMe = messageDto.IsSentByMe;
            SenderName = messageDto.Login;
            Initials = SenderName.Substring(0, 2);
            ProfilePictureRgb = SenderName.FromLogin().ToColorCode();
        }

        public ChatMessageViewModel() { }

        private readonly TimeSpan _readMessageDelay = TimeSpan.FromSeconds(1);

        public async Task ReadMessage()
        {
            if (IsRead)
            {
                await Task.Delay(_readMessageDelay).ContinueWith((task => IsRead = false));
            }
        }

    }
}
