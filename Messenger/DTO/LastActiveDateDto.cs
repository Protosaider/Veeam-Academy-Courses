using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class CLastActiveDateDto
    {
        public Guid UserId { get; set; }
        public DateTimeOffset LastActiveDate { get; set; }

        public CLastActiveDateDto(Guid userId, DateTimeOffset lastActiveDate)
        {
            UserId = userId;
            LastActiveDate = lastActiveDate;
        }

        public CLastActiveDateDto() { }
    }
}
