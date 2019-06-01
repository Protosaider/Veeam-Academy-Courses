using System;
using Info;

namespace DataStorage.DataProviders
{
    public interface ICMessageInChatInfoDataProvider
    {
        Int32 CreateMessageInChat(CMessageInChatInfo messageInChat);
        Int32 UpdateReadMessageInChat(CMessageInChatInfo messageInChat);
    }
}