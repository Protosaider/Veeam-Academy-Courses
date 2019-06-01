using System;

namespace Info
{
    public sealed class CContactInfo
    {
        public Guid Id { get; }
        public Guid OwnerId { get; }
        public Guid UserId { get; }
        public Boolean IsBlocked { get; }

        public CContactInfo(Guid id, Guid ownerId, Guid userId, Boolean isBlocked)
        {
            Id = id;
            OwnerId = ownerId;
            UserId = userId;
            IsBlocked = isBlocked;
        }
    }
}
