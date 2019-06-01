using System;

namespace DTO
{
    public class CContactDto
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public Guid UserId { get; set; }
        public Boolean IsBlocked { get; set; }
        public CUserDto UserData { get; set; }

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
