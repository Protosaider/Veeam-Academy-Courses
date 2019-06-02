using System;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public sealed class CNewMessageDto
    {
        public String MessageText { get; }
        public DateTimeOffset DispatchDate { get; }
        public Int32 Type { get; }
        public String ContentUri { get; }
        [Required]
        public Guid ChatId { get; }
        [Required]
        public Guid SenderId { get; }

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