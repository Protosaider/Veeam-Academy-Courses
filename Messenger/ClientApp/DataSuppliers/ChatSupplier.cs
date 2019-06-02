using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ClientApp.DataSuppliers.Data;
using ClientApp.Other;
using ClientApp.ServiceProxies;
using DTO;
using log4net;

namespace ClientApp.DataSuppliers
{
	internal sealed class CChatSupplier : IChatSupplier, IDisposable
    {
        private readonly CChatServiceProxy _service;
        private readonly ILog _logger = SLogger.GetLogger();

        private CChatSupplier()
        {
            _service = new CChatServiceProxy();
        }

		internal static CChatSupplier Create()
        {
            try
            {
                return new CChatSupplier();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        #region IChatSupplier

        public IReadOnlyCollection<CMessageData> GetAllMessages(Guid userId, Guid chatId, Int32 limit, Int32 offset)
        {
            _logger.LogInfo($"Supplier method '{nameof(GetAllMessages)}({userId}, {chatId}, {limit}, {offset})' is called");

			return _service.GetAllMessages(userId, chatId, limit, offset).Select(messageDto => new CMessageData(messageDto.Id, messageDto.DispatchDate, messageDto.MessageText, messageDto.IsSentByRequestingUser, messageDto.IsRead, messageDto.Login, messageDto.Usn)).ToList();
        }

        public Task<IReadOnlyCollection<CMessageData>> GetNewMessages(Guid userId, Guid chatId, DateTimeOffset lastRequestDate, Int64 usn, Int32 limit, Int32 offset)
        {        
            _logger.LogInfo($"Supplier method '{nameof(GetNewMessages)}({userId}, {chatId}, {lastRequestDate}, {limit}, {offset})' is called");
            Console.WriteLine(@"GetNewMessages called");
            return Task.Run<IReadOnlyCollection<CMessageData>>(() =>
			{
				return _service.GetNewMessages(userId, chatId, lastRequestDate, usn, limit, offset).Select(messageDto => new CMessageData(messageDto.Id, messageDto.DispatchDate, messageDto.MessageText, messageDto.IsSentByRequestingUser, messageDto.IsRead, messageDto.Login, messageDto.Usn)).ToList();
			});
        }

        public CMessageData SendMessage(String messageText, DateTimeOffset dispatchDate, Int32 type, String attachedContent, Guid chatId, Guid senderId)
        {
            _logger.LogInfo($"Supplier method '{nameof(SendMessage)}({messageText}, {type}, {attachedContent}, {chatId}, {senderId})' is called");

            var message = new CNewMessageDto(messageText, dispatchDate, type, attachedContent, chatId, senderId);

            var result = _service.SendMessage(message);

            return result == null ? null : new CMessageData(result.Id, dispatchDate, messageText, true, true, result.SenderLogin, result.Usn);
		}

        //public Task<Boolean> ReadMessages(Guid userId, Guid chatId, List<Guid> readMessages)
        public Task<Boolean> ReadMessages(Guid userId, List<Guid> readMessages)
        {
            //_logger.LogInfo($"Supplier method '{nameof(ReadMessages)}({userId}, {chatId}, {readMessages})' is called");
            _logger.LogInfo($"Supplier method '{nameof(ReadMessages)}({userId}, {readMessages})' is called");
            return Task.Run<Boolean>(() =>
            {
                //var readMessagesDto = new CReadMessagesDto(userId, chatId, readMessages);
                var readMessagesDto = new CReadMessagesDto(userId, readMessages);
                return _service.ReadMessages(readMessagesDto);
            });
        }

        public IReadOnlyCollection<CParticipantData> GetChatParticipants(Guid userId, Guid chatId)
        {
            _logger.LogInfo($"Supplier method '{nameof(GetChatParticipants)}({userId}, {chatId})' is called");

			return _service.GetChatParticipants(userId, chatId).Select(userDto => new CParticipantData(userDto.Login, userDto.ActivityStatus)).ToList();
        }

        #endregion

        #region IDisposable

        public void Dispose() => _service?.Dispose();

        #endregion

    }
}
