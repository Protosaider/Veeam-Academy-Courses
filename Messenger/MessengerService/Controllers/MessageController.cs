using System.Web.Http;

namespace MessengerService.Controllers
{
    public sealed class MessageController : ApiController
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
