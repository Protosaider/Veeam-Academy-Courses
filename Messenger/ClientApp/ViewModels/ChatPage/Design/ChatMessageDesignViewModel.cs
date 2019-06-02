using System;

namespace ClientApp.ViewModels.ChatPage.Design
{
	internal sealed class ChatMessageDesignViewModel : ChatMessageViewModel
    {
        public static ChatMessageDesignViewModel Instance => new ChatMessageDesignViewModel();

        public ChatMessageDesignViewModel()
        {
            Initials = "LM";
            SenderName = "Luke";
            Message = "Text";
            ProfilePictureRgb = "3099c5";
            IsSentByMe = false;
            DispatchDate = DateTimeOffset.UtcNow;
            ReadDate = DateTimeOffset.UtcNow.Subtract(TimeSpan.FromDays(1.3));
        }

    }
}
