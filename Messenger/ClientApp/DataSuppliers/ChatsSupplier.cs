using ClientApp.ServiceProxies;
using DTO;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientApp.DataSuppliers.Data;
using ClientApp.Other;

namespace ClientApp.DataSuppliers
{
	internal sealed class CChatsSupplier : IChatsSupplier
    {
        private readonly CChatsServiceProxy _service;
        private readonly ILog _logger = SLogger.GetLogger();

		private CChatsSupplier()
        {
            _service = new CChatsServiceProxy();
        }

		internal static CChatsSupplier Create()
        {
            try
            {
                return new CChatsSupplier();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IReadOnlyCollection<CChatData> GetChats(Guid participantId)
        {
            _logger.LogInfo($"Supplier method '{nameof(GetChats)}({participantId})' is called");

			return _service.GetChats(participantId).Select(chatDto => new CChatData(chatDto.Id, chatDto.Title, (EChatType)chatDto.Type, chatDto.IsPersonal)).ToList();
        }

        public Task<Int32> GetUnreadMessagesCount(Guid userId, Guid chatId)
        {
            _logger.LogInfo($"Supplier method '{nameof(GetUnreadMessagesCount)}({userId}, {chatId})' is called");

            return Task.Run<Int32>(() => _service.GetUnreadMessagesCount(userId, chatId));
        }

        public Task<CMessageData> GetLastMessage(Guid userId, Guid chatId)
        {
            _logger.LogInfo($"Supplier method '{nameof(GetLastMessage)}({chatId})' is called");

            return Task.Run<CMessageData>(() =>
            {
                //TODO May return null - Use NullObjectPattern?
                var result = _service.GetLastMessage(userId, chatId);
                return result == null ? CMessageData.Null : new CMessageData(result.Id, result.DispatchDate, result.MessageText, result.IsSentByRequestingUser, result.IsRead, result.Login, result.Usn);
			});
        }
        //TODO Create method to get item depended on chat type
        public Task<CChatData> GetDialog(Guid userId, Guid participantId)
        {
            _logger.LogInfo($"Supplier method '{nameof(GetDialog)}({userId}, {participantId})' is called");

            return Task.Run<CChatData>(() =>
            {
                var result = _service.GetDialog(userId, participantId);
                //if (result == null)
                //    return CChatData.Null;
                return new CChatData(result.Id, result.Title, (EChatType)result.Type, result.IsPersonal);
            });
        }

        public Task<Boolean> CreateChat(CChatData chatData, Guid creatorId, List<Guid> participantsId)
        {
            _logger.LogInfo($"Supplier method '{nameof(CreateChat)}({chatData}, {creatorId}, {participantsId})' is called");

            CNewChatDto chatDto = new CNewChatDto(chatData.Title, chatData.IsPersonal, (Int32)chatData.Type, creatorId, participantsId);
            return Task.Run<Boolean>(() => _service.CreateChat(chatDto));

        }
    }
}
