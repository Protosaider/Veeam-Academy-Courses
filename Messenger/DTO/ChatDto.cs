using System;

namespace DTO
{
    public sealed class CChatDto
    {
        public Guid Id { get; }
        public String Title { get; }
        public Guid OwnerId { get; }
        public Boolean IsPersonal { get; }
        public Int32 Type { get; }

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
