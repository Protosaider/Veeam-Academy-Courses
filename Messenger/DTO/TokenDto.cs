using System;

namespace DTO
{
    public sealed class CTokenDto
    {
        public Guid Id { get; }

        public CTokenDto(Guid id)
        {
            Id = id;
        }
    }
}
