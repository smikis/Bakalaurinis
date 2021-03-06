﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TinkloProblemos.API.Contracts.Models
{
    public class TokenResult
    {
        public string Token { get; set; }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public IList<string> Roles { get; set; }
    }
}
