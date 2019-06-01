using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class CMessagePostedDto
    {
        public Guid Id { get; set; }
        public DateTimeOffset DispatchDate { get; set; }
        public String SenderLogin { get; set; }
        public Int64 USN { get; set; }

        public CMessagePostedDto(Guid id, DateTimeOffset dispatchDate, String senderLogin, Int64 usn)
        {
            Id = id;
            DispatchDate = dispatchDate;
            SenderLogin = senderLogin;
            USN = usn;
        }
    }
}
