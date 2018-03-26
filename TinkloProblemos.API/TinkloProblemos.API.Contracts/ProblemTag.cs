using System;
using System.Collections.Generic;

namespace TinkloProblemos.API.Database
{
    public partial class ProblemTag
    {
        public int TagId { get; set; }
        public int ProblemId { get; set; }

        public Problem Problem { get; set; }
        public Tag Tag { get; set; }
    }
}
