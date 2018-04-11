using System;
using System.Collections.Generic;
using System.Text;

namespace TinkloProblemos.API.Contracts.User
{
    public class GetUser
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
