using System;

namespace DTO
{
    public sealed class CMessagePostedDto
    {
        public Guid Id { get; }
        public DateTimeOffset DispatchDate { get; }
        public String SenderLogin { get; }
        public Int64 Usn { get; }

        public CMessagePostedDto(Guid id, DateTimeOffset dispatchDate, String senderLogin, Int64 usn)
        {
            Id = id;
            DispatchDate = dispatchDate;
            SenderLogin = senderLogin;
            Usn = usn;
        }
    }
}
