using System;

namespace DTO
{
    public sealed class CLastActiveDateDto
    {
        public Guid UserId { get; }
        public DateTimeOffset LastActiveDate { get; }

        public CLastActiveDateDto(Guid userId, DateTimeOffset lastActiveDate)
        {
            UserId = userId;
            LastActiveDate = lastActiveDate;
        }

        public CLastActiveDateDto() { }
    }
}
