using System;
using System.Collections.Generic;
using Info;

namespace DataStorage.DataProviders
{
    public interface ICChatInfoDataProvider
    {
        IList<CChatInfo> GetChatsByParticipantId(Guid userId);
        Int32 GetUnreadMessagesCount(Guid userId, Guid chatId);
        IList<CChatInfo> GetChatsByOwnerId(Guid userId);
        CChatInfo GetChatById(Guid chatId);
        CChatInfo GetDialog(Guid userId, Guid participantId);
        CChatInfo CreateChat(CChatInfo chatInfo);
    }
}