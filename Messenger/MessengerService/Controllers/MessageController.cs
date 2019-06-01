using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataStorage.DataProviders;
using Info;

namespace MessengerService.Controllers
{
    public class MessageController : ApiController
    {
        //public IHttpActionResult GetAllMessagesFromChat(Guid chatId)
        //{
        //    Console.WriteLine($"Starting processing incoming query {nameof(GetAllMessagesFromChat)} with parameter {nameof(chatId)}={chatId}");
        //    return Ok(CMessageInfoDataProvider.GetAllMessagesFromChat(chatId));
        //}

        //public IHttpActionResult PostMessage([FromBody]CMessageDto message)
        //{
        //    CMessageInfoDataProvider.CreateMessage(message);
        //    return Ok();
        //}

        //public IHttpActionResult PostCreateMessageInChat([FromBody]CMessageInChatDTO messageInChat)
        //{
        //    return Ok(CMessageInChatInfoDataProvider.CreateMessageInChat(messageInChat));
        //}
    }
}
