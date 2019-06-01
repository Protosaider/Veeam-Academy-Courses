using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class CNewChatDto
    {
        public String Title { get; set; }
        public Boolean IsPersonal { get; set; }
        public Int32 Type { get; set; }
        public Guid CreatorId { get; set; }
        public List<Guid> ParticipantsId { get; set; }

        public CNewChatDto(String title, Boolean isPersonal, Int32 type, Guid creatorId, List<Guid> participantsId)
        {
            Title = title;
            IsPersonal = isPersonal;
            Type = type;
            CreatorId = creatorId;
            ParticipantsId = participantsId;
        }
    }
}
