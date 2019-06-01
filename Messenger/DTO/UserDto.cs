using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class CUserDto
    {
        public String Login { get; set; }
        public DateTimeOffset LastActiveTime { get; set; }
        public Int32 ActivityStatus { get; set; }

        public CUserDto(String login, DateTimeOffset lastActiveTime, Int32 activityStatus)
        {
            Login = login;
            LastActiveTime = lastActiveTime;
            ActivityStatus = activityStatus;
        }
    }
}
