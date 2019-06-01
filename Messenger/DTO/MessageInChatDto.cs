using System;

namespace DTO
{
    public class CMessageInChatDto
    {
        public Guid Id { get; set; }
        public Guid MessageId { get; set; }
        public Guid ChatId { get; set; }
        public Guid FromUserId { get; set; }
        public Guid ToUserId { get; set; }
        public Boolean IsRead { get; set; }

        public CMessageInChatDto(Guid id, Guid messageId, Guid chatId, Guid fromUserId, Guid toUserId, Boolean isRead)
        {
            Id = id;
            MessageId = messageId;
            ChatId = chatId;
            FromUserId = fromUserId;
            ToUserId = toUserId;
            IsRead = isRead;
        }
    }
}