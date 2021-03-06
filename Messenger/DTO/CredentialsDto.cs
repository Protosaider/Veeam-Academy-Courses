﻿using System;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public sealed class CCredentialsDto
    {
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Login should contain at least 3 character and not more then 20")]
        public String Login { get; }
        [Required]
        [StringLength(24, MinimumLength = 3, ErrorMessage = "Password should contain at least 3 character and not more then 24")]
        public String Password { get; }

        public CCredentialsDto(String login, String password)
        {
            Login = login;
            Password = password;
        }

        //TODO it's necessary for [FromUri]
        public CCredentialsDto() { }
    }
}