using System;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public sealed class CSignUpDto
    {
        [Required]
        public CCredentialsDto Credentials { get; }
        public String Avatar { get; }

        public CSignUpDto(CCredentialsDto credentials, String avatar)
        {
            Credentials = credentials;
            Avatar = avatar;
        }
    }
}