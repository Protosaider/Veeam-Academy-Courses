using System;

namespace DTO
{
    public class CChatDto
    {
        public Guid Id { get; set; }
        public String Title { get; set; }
        public Guid OwnerId { get; set; }
        public Boolean IsPersonal { get; set; }
        public Int32 Type { get; set; }

        public CChatDto(Guid id, String title, Guid ownerId, Boolean isPersonal, Int32 type)
        {
            Id = id;
            Title = title;
            OwnerId = ownerId;
            IsPersonal = isPersonal;
            Type = type;
        }
    }
}
