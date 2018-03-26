using System;
using System.Collections.Generic;

namespace TinkloProblemos.API.Database
{
    public partial class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public int ProblemId { get; set; }

        public Problem Problem { get; set; }
        public User User { get; set; }
    }
}
