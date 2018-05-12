using System;
using System.Collections.Generic;
using System.Text;

namespace TinkloProblemos.API.Contracts.InternetUser
{
    public class InternetUserPage
    {
        public int Total { get; set; }
        public IEnumerable<InternetUserDto> Data { get; set; }
    }
}
