using System;
using System.Collections.ObjectModel;

namespace ClientApp.ViewModels.ChatPage.Design
{
	internal class ChatDesignViewModel : ChatViewModel
    {
        public static ChatDesignViewModel Instance => new ChatDesignViewModel();
     
        public ChatDesignViewModel()
        {
            DisplayTitle = "Chat Title";

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
                    SenderName = "Luke",
                    Initials = "LM",
                    Message = "Let me know when you manage to spin me right round baby right now",
                    ProfilePictureRgb = "3099c5",
                    DispatchDate = DateTimeOffset.UtcNow,
                    ReadDate = DateTimeOffset.UtcNow.Subtract(TimeSpan.FromDays(1.3)),
                    IsSentByMe = true,
                },
                new ChatMessageViewModel()
                {
                    SenderName = "Parnell",
                    Initials = "PL",
                    Message = "The song from Alan Wake is...\r\nCarnival of rust!",
                    ProfilePictureRgb = "3099c5",
                    DispatchDate = DateTimeOffset.UtcNow,
                    IsSentByMe = false,
                },
            };
        }
    }
}
