using DataStorage.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Common.ServiceLocator;
using DataStorage;
using DTO;
using Info;
using log4net;
using MessengerService.Other;

namespace MessengerService.Controllers
{
    public sealed class ChatsController : ApiController
    {
        private static readonly ILog s_log = SLogger.GetLogger();
        private readonly ICChatInfoDataProvider _chatDataProvider;
        private readonly ICChatsParticipantInfoDataProvider _chatsParticipantDataProvider;
        private readonly ICMessageInfoDataProvider _messageDataProvider;
        private readonly ICUserInfoDataProvider _userDataProvider;

        //public ChatsController(ICChatInfoDataProvider chatDataProvider)
        //{
        //    _chatDataProvider = chatDataProvider ?? throw new ArgumentNullException(nameof(chatDataProvider));
        //}

        public ChatsController()
        {
            var container = SServiceLocator.CreateContainer();
            ConfigureContainer(ref container);
            _chatDataProvider = container.Resolve<ICChatInfoDataProvider>();
            _chatsParticipantDataProvider = container.Resolve<ICChatsParticipantInfoDataProvider>();
            _messageDataProvider = container.Resolve<ICMessageInfoDataProvider>();
            _userDataProvider = container.Resolve<ICUserInfoDataProvider>();
        }

        private static void ConfigureContainer(ref CContainer container)
        {
            container.Register<ICChatInfoDataProvider, CChatInfoDataProvider>(ELifeCycle.Transient);
            container.Register<ICChatsParticipantInfoDataProvider, CChatsParticipantInfoDataProvider>(ELifeCycle.Transient);
            container.Register<ICMessageInfoDataProvider, CMessageInfoDataProvider>(ELifeCycle.Transient);
            container.Register<ICUserInfoDataProvider, CUserInfoDataProvider>(ELifeCycle.Transient);
            container.Register<CDataStorageSettings>(ELifeCycle.Transient);
        }

        [Route("api/{participantId}/chats")]
        [ResponseType(typeof(IEnumerable<CChatDto>))]
        public IHttpActionResult GetChats([FromUri]Guid participantId)
        {
            s_log.LogInfo($"{System.Reflection.MethodBase.GetCurrentMethod()}({participantId}) is called");

            if (participantId == Guid.Empty)
            {
                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()}({participantId})", new ArgumentNullException(nameof(participantId), "Incoming data is null"));
                ModelState.AddModelError($"{nameof(participantId)}", "Incoming data is null");
                return BadRequest(ModelState);
            }

            var chatInfos = _chatDataProvider.GetChatsByParticipantId(participantId);

            if (chatInfos == null)
            {
                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()}({participantId})", new Exception("Failed to get all chats"));
                return NotFound();
            }

            return Ok(chatInfos.Select(x => new CChatDto(x.Id, x.Title, x.OwnerId, x.IsPersonal, x.Type)));
        }

        [Route("api/{userId}/chats/{participantId}")]
        [ResponseType(typeof(CChatDto))]
        public IHttpActionResult GetDialog([FromUri]Guid userId, [FromUri]Guid participantId)
        {
            s_log.LogInfo($"{System.Reflection.MethodBase.GetCurrentMethod()}({userId}, {participantId}) is called");

            if (participantId == Guid.Empty)
            {
                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()}({userId}, {participantId})", new ArgumentNullException(nameof(participantId), "Incoming data is null"));
                ModelState.AddModelError($"{nameof(participantId)}", "Incoming data is null");
                return BadRequest(ModelState);
            }

            if (userId == Guid.Empty)
            {
                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()}({userId}, {participantId})", new ArgumentNullException(nameof(participantId), "Incoming data is null"));
                ModelState.AddModelError($"{nameof(userId)}", "Incoming data is null");
                return BadRequest(ModelState);
            }

            var chat = _chatDataProvider.GetDialog(userId, participantId);

            if (chat == null)
            {
                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()}({userId}, {participantId})", new Exception("Failed to get all chats"));
                return NotFound();
            }

            return Ok(new CChatDto(chat.Id, chat.Title, chat.OwnerId, chat.IsPersonal, chat.Type));
        }

        //todo From every chat at one time (return IEnumerable(Guid, Int32))
        [Route("api/{userId}/chats/{chatId}/messages/unread/count")]
        [ResponseType(typeof(IEnumerable<CChatDto>))]
        public IHttpActionResult GetUnreadMessagesCount([FromUri]Guid userId, [FromUri]Guid chatId)
        {
            s_log.LogInfo($"{System.Reflection.MethodBase.GetCurrentMethod()}({userId}, {chatId}) is called");

            Boolean hasError = false;

            if (userId == default(Guid))
            {
                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()}({userId}, {chatId})", new ArgumentNullException(nameof(userId), "Incoming data is null"));
                ModelState.AddModelError($"{nameof(userId)}", "Incoming data is null");
				hasError = true;
			}
            else if (chatId == default(Guid))
            {
                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()}({userId}, {chatId})", new ArgumentNullException(nameof(chatId), "Incoming data is null"));
                ModelState.AddModelError($"{nameof(chatId)}", "Incoming data is null");
				hasError = true;
            }

            if (hasError)
                return BadRequest(ModelState);

            Int32 messagesCount = _chatDataProvider.GetUnreadMessagesCount(userId, chatId);

            return Ok(messagesCount);
        }

        [Route("api/{userId}/chats/{chatId}/messages/last")]
        [ResponseType(typeof(IEnumerable<CMessageInfo>))]
        public IHttpActionResult GetLastMessage([FromUri]Guid userId, [FromUri]Guid chatId)
        {
            s_log.LogInfo($"{System.Reflection.MethodBase.GetCurrentMethod()}({chatId}) is called");

            if (chatId == Guid.Empty)
            {
                ModelState.AddModelError($"{nameof(chatId)}", "Incoming data is null");
                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()}({chatId})", new ArgumentNullException(nameof(chatId), "Incoming data is null"));
                return BadRequest(ModelState);
            }

            CMessageInfo lastMsg = _messageDataProvider.GetLastMessageFromChat(chatId);

            if (lastMsg == null)
            {
                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()}({chatId})", new Exception("Failed to get last message"));
                return NotFound();
            }

            return Ok(new CMessageDto(lastMsg.Id, lastMsg.DispatchDate, lastMsg.MessageText, lastMsg.Type, lastMsg.ContentUri, lastMsg.FromUserId == userId, lastMsg.IsRead, lastMsg.Login, lastMsg.Usn));
        }

        [Route("api/chats/{chatId}/participants")]
        [ResponseType(typeof(IEnumerable<CTokenDto>))]
        public IHttpActionResult GetChatParticipants([FromUri]Guid chatId)
        {
            s_log.LogInfo($"{System.Reflection.MethodBase.GetCurrentMethod()}({chatId}) is called");

            if (chatId == Guid.Empty)
            {
                ModelState.AddModelError($"{nameof(chatId)}", "Incoming data is null");
                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()}({chatId})", new ArgumentNullException(nameof(chatId), "Incoming data is null"));
                return BadRequest(ModelState);
            }

            var userInfos = _userDataProvider.GetAllChatParticipantsByChatId(chatId);

            if (userInfos == null)
            {
                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()}({chatId})", new Exception("Failed to get all chat participants"));
                return NotFound();
            }

            return Ok(userInfos.Select(x => new CTokenDto(x.Id)));
        }

        [Route("api/chats/{chatId}/participants/activity")]
        [ResponseType(typeof(IEnumerable<CActivityStatusDto>))]
        public IHttpActionResult GetChatParticipantsActivityStatus([FromUri]Guid chatId)
        {
            s_log.LogInfo($"{System.Reflection.MethodBase.GetCurrentMethod()}({chatId}) is called");

            if (chatId == Guid.Empty)
            {
                ModelState.AddModelError($"{nameof(chatId)}", "Incoming data is null");
                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()}({chatId})", new ArgumentNullException(nameof(chatId), "Incoming data is null"));
                return BadRequest(ModelState);
            }

            var userInfos = _userDataProvider.GetAllChatParticipantsByChatId(chatId);

            if (userInfos == null)
            {
                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()}({chatId})", new Exception("Failed to get all chat participants"));
                return NotFound();
            }

            return Ok(userInfos.Select(x => new CActivityStatusDto(x.Id, x.ActivityStatus)));
        }

        [Route("api/chats/new")]
        [ResponseType(typeof(Boolean))]
        [ValidateModel]
        public IHttpActionResult PostChat([FromBody]CNewChatDto newChat)
        {
            s_log.LogInfo($"{System.Reflection.MethodBase.GetCurrentMethod()}({newChat}) is called");

            if (newChat == null)
            {
                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()}({(CNewChatDto)null})", new ArgumentNullException(nameof(newChat), "Incoming data is null"));
                ModelState.AddModelError($"{nameof(newChat)}", "Incoming data is null");
                return BadRequest(ModelState);
            }

            var chatInfo = _chatDataProvider.CreateChat(new CChatInfo(default(Guid), newChat.Title, newChat.CreatorId, newChat.IsPersonal, newChat.Type));

            if (chatInfo == null)
            {
                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()}({newChat})", new Exception("Failed to create chat"));
                return InternalServerError();
            }

            var result =
                _chatsParticipantDataProvider.CreateChatParticipant(
                    new CChatsParticipantInfo(default(Guid), chatInfo.Id, newChat.CreatorId));

            if (result == 0)
            {
                s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()}({newChat})", new Exception("Failed to create chat"));
                return InternalServerError();
            }

            foreach (var participantId in newChat.ParticipantsId)
            {
                result =
                    _chatsParticipantDataProvider.CreateChatParticipant(
                        new CChatsParticipantInfo(default(Guid), chatInfo.Id, participantId));

                if (result == 0)
                {
                    s_log.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()}({newChat})", new Exception("Failed to create chat"));
                    return InternalServerError();
                }
            }

            return Ok(true);
        }
    }
}
