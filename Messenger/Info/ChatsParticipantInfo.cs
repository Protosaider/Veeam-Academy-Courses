using System;

namespace Info
{
    public sealed class CChatsParticipantInfo
    {
        public Guid Id { get; }
        public Guid ChatId { get; }
        public Guid UserId { get; }

        public CChatsParticipantInfo(Guid id, Guid chatId, Guid userId)
        {
            Id = id;
            ChatId = chatId;
            UserId = userId;
        }
    }
}