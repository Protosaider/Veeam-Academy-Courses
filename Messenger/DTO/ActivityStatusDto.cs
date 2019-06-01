using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class CActivityStatusDto
    {
        public Guid UserId { get; set; }
        public Int32 ActivityStatus { get; set; }

        public CActivityStatusDto(Guid userId, Int32 activityStatus)
        {
            UserId = userId;
            ActivityStatus = activityStatus;
        }
    }
}
