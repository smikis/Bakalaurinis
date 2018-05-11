using System;
using System.Collections.Generic;
using System.Text;

namespace TinkloProblemos.API.Contracts.User
{
    public class UsersPage
    {
        public int Total { get; set; }
        public IEnumerable<GetUser> Data { get; set; }
    }
}
