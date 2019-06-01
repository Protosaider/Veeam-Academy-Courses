using ClientApp.ViewModels.ChatPage;
using DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClientApp.DataSuppliers.Data;

namespace ClientApp.DataSuppliers
{
	internal interface IChatsSupplier
    {
        IReadOnlyCollection<CChatData> GetChats(Guid participantId);
        Task<Int32> GetUnreadMessagesCount(Guid userId, Guid chatId);
        Task<CMessageData> GetLastMessage(Guid userId, Guid chatId);
        Task<CChatData> GetDialog(Guid userId, Guid participantId);
        Task<Boolean> CreateChat(CChatData chatData, Guid creatorId, List<Guid> participantsId);
    }
}