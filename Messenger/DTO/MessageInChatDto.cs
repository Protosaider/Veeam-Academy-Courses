using System;

namespace DTO
{
    public sealed class CMessageInChatDto
    {
        public Guid Id { get; }
        public Guid MessageId { get; }
        public Guid ChatId { get; }
        public Guid FromUserId { get; }
        public Guid ToUserId { get; }
        public Boolean IsRead { get; }

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