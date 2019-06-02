using System;
using System.Collections.Generic;

namespace DTO
{
    public sealed class CNewChatDto
    {
        public String Title { get; }
        public Boolean IsPersonal { get; }
        public Int32 Type { get; }
        public Guid CreatorId { get; }
        public List<Guid> ParticipantsId { get; }

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
