﻿using System.ComponentModel.DataAnnotations;

namespace TinkloProblemos.API.Contracts.User
{
    public class Login
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
