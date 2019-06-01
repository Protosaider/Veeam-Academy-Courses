using System;

namespace Info
{
    public sealed class CChatInfo
    {
        public Guid Id { get; }
        public String Title { get; }
        public Guid OwnerId { get; }
        public Boolean IsPersonal { get; }
        public Int32 Type { get; }
        

        public CChatInfo(Guid id, String title, Guid ownerId, Boolean isPersonal, Int32 type)
        {
            Id = id;
            Title = title;
            OwnerId = ownerId;
            IsPersonal = isPersonal;
            Type = type;
        }
    }
}
