using System;
using System.Collections.Generic;

namespace DTO
{
    public class CReadMessagesDto
    {
        //public Guid UserId { get; set; }
        //public Guid ChatId { get; set; }
        //public List<Guid> ReadMessages { get; set; }

        //public CReadMessagesDto(Guid userId, Guid chatId, List<Guid> readMessages)
        //{
        //    UserId = userId;
        //    ChatId = chatId;
        //    ReadMessages = readMessages;
        //}

        public Guid UserId { get; set; }
        public List<Guid> ReadMessages { get; set; }
        public CReadMessagesDto(Guid userId, List<Guid> readMessages)
        {
            UserId = userId;
            ReadMessages = readMessages;
        }
    }
}