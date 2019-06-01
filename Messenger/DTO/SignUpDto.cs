using System;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class CSignUpDto
    {
        [Required]
        public CCredentialsDto Credentials { get; set; }
        public String Avatar { get; set; }

        public CSignUpDto(CCredentialsDto credentials, String avatar)
        {
            Credentials = credentials;
            Avatar = avatar;
        }
    }
}