using System;
using System.Collections.Generic;
using System.Text;

namespace TinkloProblemos.API.Contracts.Comment
{
    public class GetComment
    {
        public int Id { get; set; }
        public string Text { get; set; }     
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
