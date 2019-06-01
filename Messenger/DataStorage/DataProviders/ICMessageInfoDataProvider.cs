using System;
using System.Collections.Generic;
using Info;

namespace DataStorage.DataProviders
{
    public interface ICMessageInfoDataProvider
    {
        IList<CMessageInfo> GetAllMessagesFromChat(Guid userId, Guid chatId, Int32 limit, Int32 offset);
        CMessageInfo CreateMessage(CMessageInfo message);
        CMessageInfo GetLastMessageFromChat(Guid chatId);
        IList<CMessageInfo> GetNewMessagesFromChat(Guid userId, Guid chatId, DateTimeOffset lastRequestDate, Int32 limit, Int32 offset, Int64 usn);
    }
}