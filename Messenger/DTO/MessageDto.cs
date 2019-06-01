using System;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class CMessageDto
    {
        public Guid Id { get; set; }
        public DateTimeOffset DispatchDate { get; set; }
        public String MessageText { get; set; }
        public Int32 Type { get; set; }
        public String ContentUri { get; set; }
        public Boolean IsSentByRequestingUser { get; set; }
        public Boolean IsRead { get; set; }
        public String Login { get; set; }
        public Int64 USN { get; set; }

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
            USN = usn;
        }
    }
}