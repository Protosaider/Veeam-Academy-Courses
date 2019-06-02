using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClientApp.DataSuppliers.Data;

namespace ClientApp.DataSuppliers
{
	internal interface IChatSupplier
    {
        IReadOnlyCollection<CMessageData> GetAllMessages(Guid userId, Guid chatId, Int32 limit, Int32 offset);
        Task<IReadOnlyCollection<CMessageData>> GetNewMessages(Guid userId, Guid chatId, DateTimeOffset lastRequestDate, Int64 lastUsn, Int32 limit, Int32 offset);
        CMessageData SendMessage(String messageText, DateTimeOffset dispatchDate, Int32 type, String attachedContent, Guid chatId, Guid senderId);
        //Task<Boolean> ReadMessages(Guid userId, Guid chatId, List<Guid> readMessages);
        Task<Boolean> ReadMessages(Guid userId, List<Guid> readMessages);
        IReadOnlyCollection<CParticipantData> GetChatParticipants(Guid userId, Guid chatId);
    }
}
