using System;

namespace DTO
{
    public sealed class CActivityStatusDto
    {
        public Guid UserId { get; }
        public Int32 ActivityStatus { get; }

        public CActivityStatusDto(Guid userId, Int32 activityStatus)
        {
            UserId = userId;
            ActivityStatus = activityStatus;
        }
    }
}
