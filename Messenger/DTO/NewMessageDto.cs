using System;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class CNewMessageDto
    {
        public String MessageText { get; set; }
        public DateTimeOffset DispatchDate { get; set; }
        public Int32 Type { get; set; }
        public String ContentUri { get; set; }
        [Required]
        public Guid ChatId { get; set; }
        [Required]
        public Guid SenderId { get; set; }

        public CNewMessageDto(String messageText, DateTimeOffset dispatchDate, Int32 type, String contentUri, Guid chatId, Guid senderId)
        {
            MessageText = messageText;
            DispatchDate = dispatchDate;
            Type = type;
            ContentUri = contentUri;
            ChatId = chatId;
            SenderId = senderId;
        }
    }
}