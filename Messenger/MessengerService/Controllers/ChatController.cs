using DataStorage.DataProviders;
using DTO;
using Info;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Common.ServiceLocator;
using DataStorage;
using MessengerService.Other;

namespace MessengerService.Controllers
{
    public sealed class ChatController : ApiController
    {
        private static readonly ILog s_log = SLogger.GetLogger();
        private readonly ICMessageInChatInfoDataProvider _messageInChatDataProvider;
        private readonly ICMessageInfoDataProvider _messageDataProvider;
        private readonly ICUserInfoDataProvider _userDataProvider;

        public ChatController()
        {
            var container = SServiceLocator.CreateContainer();
            ConfigureContainer(ref container);
            _messageInChatDataProvider = container.Resolve<ICMessageInChatInfoDataProvider>();
            _messageDataProvider = container.Resolve<ICMessageInfoDataProvider>();
            _userDataProvider = container.Resolve<ICUserInfoDataProvider>();
        }

        private static void ConfigureContainer(ref CContainer container)
        {
            container.Register<ICMessageInChatInfoDataProvider, CMessageInChatInfoDataProvider>(ELifeCycle.Transient);
            container.Register<ICMessageInfoDataProvider, CMessageInfoDataProvider>(ELifeCycle.Transient);
            container.Register<ICUserInfoDataProvider, CUserInfoDataProvider>(ELifeCycle.Transient);
            container.Register<CDataStorageSettings>(ELifeCycle.Transient);
        }

        //[Route("api/chats/{chatId}/messages?limit={limit}&offset={offset}")]
        //[Route("api/chats/{chatId}/messages{limit}{offset}")]
        [Route("api/{userId}/chats/{chatId}/messages")]
        [ResponseType(typeof(IEnumerable<CMessageDto>))]
        public IHttpActionResult GetAllMessages([FromUri]Guid userId, [FromUri]Guid chatId, [FromUri]Int32 limit = 50, [FromUri]Int32 offset = 0)
        {
            s_log.LogInfo($"{System.Reflection.MethodBase.GetCurrentMethod()}({chatId}) is called");

            if (chatId == Guid.Empty)
            {
                ModelState.AddModelError($"{nameof(chatId)}", "Incoming data is null");
                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()}({chatId})", new ArgumentNullException(nameof(chatId), "Incoming data is null"));
                return BadRequest(ModelState);
            }

            var messageInfos = _messageDataProvider.GetAllMessagesFromChat(userId, chatId, limit, offset);

            if (messageInfos == null)
            {
                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()}({chatId})", new Exception("Failed to get all messages from chat"));
                return NotFound();
            }

            return Ok(messageInfos.Select(x => new CMessageDto(x.Id, x.DispatchDate, x.MessageText, x.Type, x.ContentUri, x.FromUserId == userId, x.IsRead, x.Login, x.Usn)));
        }

        //[Route("api/chats/{chatId}/messages{lastRequestDate}{limit}{offset}")]
        //[Route("api/chats/{chatId}/messages?lastRequestDate={lastRequestDate}&limit={limit}&offset={offset}")]
        [Route("api/{userId}/chats/{chatId}/messages")]
        [ResponseType(typeof(List<CMessageDto>))]
        public IHttpActionResult GetNewMessages([FromUri]Guid userId, [FromUri]Guid chatId, [FromUri]DateTimeOffset lastRequestDate, [FromUri]Int64 lastUsn, [FromUri]Int32 limit = 50, [FromUri]Int32 offset = 0)
        {
            s_log.LogInfo($"{System.Reflection.MethodBase.GetCurrentMethod()}({chatId}) is called");

            if (chatId == Guid.Empty)
            {
                ModelState.AddModelError($"{nameof(chatId)}", "Incoming data is null");
                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()}({chatId})", new ArgumentNullException(nameof(chatId), "Incoming data is null"));
                return BadRequest(ModelState);
            }

            //Console.WriteLine(lastRequestDate.Offset);
            //Console.WriteLine(lastRequestDate.Date);
            //Console.WriteLine(lastRequestDate.UtcDateTime);
            //Console.WriteLine(lastRequestDate.LocalDateTime);

            var messageInfos = _messageDataProvider.GetNewMessagesFromChat(userId, chatId, lastRequestDate, limit, offset, lastUsn);

            if (messageInfos == null)
            {
                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()}({chatId})", new Exception("Failed to get all messages from chat"));
                return NotFound();
            }

            return Ok(messageInfos.Select(x => new CMessageDto(x.Id, x.DispatchDate, x.MessageText, x.Type, x.ContentUri, x.FromUserId == userId, x.IsRead, x.Login, x.Usn)).ToList());
        }


        //[Route("api/{userId}/chats/{chatId}/messages")]
        //[ResponseType(typeof(IEnumerable<CMessageDto>))]
        //public IHttpActionResult GetNewMessages([FromUri]Guid userId, [FromUri]Guid chatId, [FromUri]DateTime lastRequestDateTime, [FromUri]TimeSpan lastRequestOffset, [FromUri]Int32 limit = 50, [FromUri]Int32 offset = 0)
        //{
        //    s_log.LogInfo($"{System.Reflection.MethodBase.GetCurrentMethod().ToString()}({chatId}) is called");

        //    if (chatId == Guid.Empty)
        //    {
        //        ModelState.AddModelError($"{nameof(chatId)}", "Incoming data is null");
        //        s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod().ToString()}({chatId})", new ArgumentNullException(nameof(chatId), "Incoming data is null"));
        //        return BadRequest(ModelState);
        //    }

        //    var lastRequestDate = new DateTimeOffset(lastRequestDateTime, lastRequestOffset);

        //    Console.WriteLine(lastRequestDate.Offset);
        //    Console.WriteLine(lastRequestDate.Date);
        //    Console.WriteLine(lastRequestDate.UtcDateTime);
        //    Console.WriteLine(lastRequestDate.LocalDateTime);

        //    var messageInfos = CMessageInfoDataProvider.GetNewMessagesFromChat(chatId, lastRequestDate, limit, offset);

        //    if (messageInfos == null)
        //    {
        //        s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod().ToString()}({chatId})", new Exception("Failed to get all messages from chat"));
        //        return NotFound();
        //    }

        //    return Ok(messageInfos.Select(x => new CMessageDto(x.Id, x.DispatchDate, x.MessageText, x.Type, x.ContentUri, x.FromUserId == userId, x.IsRead)));
        //}

        //[HttpPost]
        [Route("api/chats/messages/new")]
        [ResponseType(typeof(CMessagePostedDto))]
        [ValidateModel]
        public IHttpActionResult PostMessage([FromBody]CNewMessageDto message)
        {
            if (message == null)
            {
                ModelState.AddModelError($"{nameof(message)}", "Incoming data is null");
                return BadRequest(ModelState);
            }

            var messageInfoToPost = new CMessageInfo(Guid.Empty, message.DispatchDate, message.MessageText, message.Type, message.ContentUri, message.SenderId, true, String.Empty, default(Int64));

            var messageInfoRetrieved = _messageDataProvider.CreateMessage(messageInfoToPost);

            if (messageInfoRetrieved == null || messageInfoRetrieved.Id == Guid.Empty)
            {
                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()}({message})", new Exception("Failed to post message: can't get message info"));
                return InternalServerError();
            }

            var chatParticipants = _userDataProvider.GetAllChatParticipantsByChatId(message.ChatId);
            if (chatParticipants == null || chatParticipants.Count == 0)
            {
                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()}({message})", new Exception("Failed to post message: can't get chat participants"));
                return InternalServerError();
            }

			CUserInfo senderInfo = null;
            //get all participants
            foreach (var user in chatParticipants)
            {
                //TODO А если сами себе пересылаем сообщение?
                //if (user.Id == message.SenderId)
                //    continue;
                if (user.Id == message.SenderId)
                    senderInfo = user;

                var messageInChatInfo = new CMessageInChatInfo(
                    Guid.Empty,
                    messageInfoRetrieved.Id,
                    message.ChatId,
                    message.SenderId,
                    user.Id,
                    user.Id == message.SenderId
                    );

                var rowsAffected = _messageInChatDataProvider.CreateMessageInChat(messageInChatInfo);

                if (rowsAffected == 0)
                {
                    s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()}({message})", new Exception("Failed to post message: can't create message in chat"));
                    return InternalServerError();
                }
            }

            return Ok(new CMessagePostedDto(messageInfoRetrieved.Id, messageInfoRetrieved.DispatchDate, senderInfo != null ? senderInfo.Login : String.Empty, messageInfoRetrieved.Usn));
        }

        [Route("api/chats/messages/read")]
        [ResponseType(typeof(Boolean))]
        [ValidateModel]
        public IHttpActionResult PostReadMessages([FromBody]CReadMessagesDto readMessages)
        {
            if (readMessages == null)
            {
                ModelState.AddModelError($"{nameof(readMessages)}", "Incoming data is null");
                return BadRequest(ModelState);
            }

            foreach (var messageId in readMessages.ReadMessages)
            {
                var messageInChatInfo = new CMessageInChatInfo(
                    default(Guid),
                    messageId,
                    default(Guid),
                    default(Guid),
                    readMessages.UserId,
                    true
                    );

                var rowsAffected = _messageInChatDataProvider.UpdateReadMessageInChat(messageInChatInfo);

                if (rowsAffected == 0)
                {
                    s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()}({readMessages})", new Exception("Failed to update message in chat"));
                    return InternalServerError();
                }
            }

            return Ok(true);
        }

        [Route("api/{userId}/chats/{chatId}/participants")]
        [ResponseType(typeof(IEnumerable<CMessageDto>))]
        public IHttpActionResult GetChatParticipants([FromUri]Guid userId, [FromUri]Guid chatId)
        {
            s_log.LogInfo($"{nameof(GetChatParticipants)}({userId}, {chatId}) is called");

            if (chatId == Guid.Empty)
            {
                ModelState.AddModelError($"{nameof(chatId)}", "Incoming data is null");
                s_log.LogError($"{nameof(GetChatParticipants)}({userId}, {chatId})", new ArgumentNullException(nameof(chatId), "Incoming data is null"));
                return BadRequest(ModelState);
            }

            var participantsInfos = _userDataProvider.GetAllChatParticipantsByChatId(chatId);

            if (participantsInfos == null)
            {
                s_log.LogError($"{nameof(GetChatParticipants)}({userId}, {chatId})", new Exception("Failed to get chat participants"));
                return NotFound();
            }

            return Ok(participantsInfos.Where(x => x.Id != userId)
                .Select(x => new CUserDto(x.Login, x.LastActiveDate, x.ActivityStatus)));
        }

    }
}
