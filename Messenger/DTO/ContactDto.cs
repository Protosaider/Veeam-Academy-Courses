using System;

namespace DTO
{
    public sealed class CContactDto
    {
        public Guid Id { get; }
        public Guid OwnerId { get; }
        public Guid UserId { get; }
        public Boolean IsBlocked { get; }
        public CUserDto UserData { get; }

        public CContactDto(Guid id, Guid ownerId, Guid userId, Boolean isBlocked, CUserDto userData)
        {
            Id = id;
            OwnerId = ownerId;
            UserId = userId;
            IsBlocked = isBlocked;
            UserData = userData;
        }
    }
}
