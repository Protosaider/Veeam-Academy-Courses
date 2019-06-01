using System;

namespace Info
{
    public sealed class CUserInfo
    {
        public Guid Id { get; }
        public String Login { get; }
        public String Password { get; }
        public DateTimeOffset LastActiveDate { get; }
        public Int32 ActivityStatus { get; } //byte?
        public String Avatar { get; }

        public CUserInfo(Guid id, String login, String password, DateTimeOffset lastActiveDate, Int32 activityStatus, String avatar)
        {
            Id = id;
            Login = login;
            Password = password;
            LastActiveDate = lastActiveDate;
            ActivityStatus = activityStatus;
            Avatar = avatar;
        }
    }
}
