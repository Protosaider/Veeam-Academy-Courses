using System;

namespace DTO
{
    public class CTokenDto
    {
        public Guid Id { get; set; }

        public CTokenDto(Guid id)
        {
            Id = id;
        }
    }
}
