using System;

namespace DTO
{
    public sealed class CMessageDto
    {
        public Guid Id { get; }
        public DateTimeOffset DispatchDate { get; }
        public String MessageText { get; }
        public Int32 Type { get; }
        public String ContentUri { get; }
        public Boolean IsSentByRequestingUser { get; }
        public Boolean IsRead { get; }
        public String Login { get; }
        public Int64 Usn { get; }

        public CMessageDto(Guid id, DateTimeOffset dispatchDate, String messageText, Int32 type, String contentUri, Boolean isSentByRequestingUser, Boolean isRead, String login, Int64 usn)
        {
            Id = id;
            DispatchDate = dispatchDate;
            MessageText = messageText;
            Type = type;
            ContentUri = contentUri;
            IsSentByRequestingUser = isSentByRequestingUser;
            IsRead = isRead;
            Login = login;
            Usn = usn;
        }
    }
}