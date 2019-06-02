using System;

namespace ClientApp.DataSuppliers.Data
{
	internal sealed class CContactData
    {
        public Guid Id { get; }
        public Guid OwnerId { get; }
        public Guid UserId { get; }
        public Boolean IsBlocked { get; }
        public String Login { get; }
        public DateTimeOffset LastActiveDate { get; }
        public Int32 ActivityStatus { get; }

        public CContactData(Guid id, Guid ownerId, Guid userId, Boolean isBlocked, String login, DateTimeOffset lastActiveDate, Int32 activityStatus)
        {
            Id = id;
            OwnerId = ownerId;
            UserId = userId;
            IsBlocked = isBlocked;
            Login = login;
            LastActiveDate = lastActiveDate;
            ActivityStatus = activityStatus;
        }
    }
}
