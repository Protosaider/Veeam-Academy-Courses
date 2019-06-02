using System;

namespace DTO
{
    public sealed class CUserDto
    {
        public String Login { get; }
        public DateTimeOffset LastActiveTime { get; }
        public Int32 ActivityStatus { get; }

        public CUserDto(String login, DateTimeOffset lastActiveTime, Int32 activityStatus)
        {
            Login = login;
            LastActiveTime = lastActiveTime;
            ActivityStatus = activityStatus;
        }
    }
}
