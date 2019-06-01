using System;

namespace ClientApp.DataSuppliers.Data
{
	internal class CMessageData
    {
        public Guid Id { get; }
        public DateTimeOffset DispatchDate { get; }
        public String Message { get; }
        public Boolean IsSentByMe { get; }
        public Boolean IsRead { get; }
        public String Login { get; }
        public Int64 Usn { get; }

        public CMessageData(Guid id, DateTimeOffset dispatchDate, String message, Boolean isSentByMe, Boolean isRead, String login, Int64 usn)
        {
            Id = id;
            DispatchDate = dispatchDate;
            Message = message;
            IsSentByMe = isSentByMe;
            IsRead = isRead;
            Login = login;
            Usn = usn;
        }

        public static readonly CMessageData Null = new CMessageData(default(Guid), default(DateTimeOffset),
            String.Empty, default(Boolean), default(Boolean), String.Empty, default(Int64));

    }
}