using System;
using System.Collections.Generic;
using System.Text;

namespace TinkloProblemos.API.Contracts.Comment
{
    public class CreateComment
    {
        public string Text { get; set; }
        public int UserId { get; set; }
        public int ProblemId { get; set; }
    }
}
